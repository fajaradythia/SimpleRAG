using RAGWebApp.Models.Orchestration;
using RAGWebApp.Services;

namespace RAGWebApp.Orchestration
{
    public class McpService
    {
        private readonly ContextBuilder _contextBuilder;
        private readonly ChatService _chat;
        private readonly ToolRegistry _tools;

        public McpService(
            ContextBuilder contextBuilder,
            ChatService chat,
            ToolRegistry tools)
        {
            _contextBuilder = contextBuilder;
            _chat = chat;
            _tools = tools;
        }

        public async Task<McpResponse> Handle(McpRequest request)
        {
            // 1. Routing (simple intent detection)
            if (_tools.IsReportQuery(request.Query))
            {
                // future: bisa call API / DB
            }

            // 2. Build context (RAG)
            var contexts = await _contextBuilder.Build(request.Query);

            // 3. Build prompt (centralized!)
            var prompt = BuildPrompt(request.Query, contexts);

            // 4. Call LLM
            var answer = await _chat.Ask(prompt);

            return new McpResponse
            {
                Answer = answer,
                Sources = contexts
            };
        }

        private string BuildPrompt(string query, List<string> contexts)
        {
            return $@"
            Anda adalah AI assistant. Jawab hanya berdasarkan context berikut.
            Jika tidak ditemukan jawabannya, katakan 'Yo Ndak Tahu, Kok Tanya Saya...'.

            Context:
            {string.Join("\n---\n", contexts)}

            Pertanyaan:
            {query}

            Jawaban:
            ";
        }
    }
}
