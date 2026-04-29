using OllamaSharp;
using OllamaSharp.Models.Chat;
using System.Text;

namespace RAGWebApp.Services
{
    public class ChatService
    {
        private readonly OllamaApiClient _client;

        public ChatService()
        {
            _client = new OllamaApiClient(new Uri("http://localhost:11434"))
            {
                SelectedModel = "qwen3:1.7b"
            };
        }

        public async Task<string> Ask(string prompt)
        {
            var sb = new StringBuilder();

            await foreach (var stream in _client.ChatAsync(new ChatRequest
            {
                Messages = new List<Message>
                {
                    new Message { Role = "user", Content = prompt }
                }
            }))
            {
                sb.Append(stream.Message.Content);
            }

            return sb.ToString();
        }
    }
}
