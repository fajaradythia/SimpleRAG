namespace RAGWebApp.Services
{
    public class RagService
    {
        private readonly EmbeddingService _embedding;
        private readonly VectorStoreService _store;
        private readonly ChatService _chat;

        public RagService(
            EmbeddingService embedding,
            VectorStoreService store,
            ChatService chat)
        {
            _embedding = embedding;
            _store = store;
            _chat = chat;
        }

        public async Task<string> Ask(string question)
        {
            var queryVector = await _embedding.GetEmbedding(question);
            var contexts = _store.Search(queryVector);

            var prompt = $@"
            Gunakan context berikut untuk menjawab:

            {string.Join("\n---\n", contexts)}

            Pertanyaan: {question}
            Jawaban:
            ";

            return await _chat.Ask(prompt);
        }
    }
}
