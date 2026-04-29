using OllamaSharp;

namespace RAGWebApp.Services
{
    public class EmbeddingService
    {
        private readonly OllamaApiClient _client;

        public EmbeddingService()
        {
            _client = new OllamaApiClient(new Uri("http://localhost:11434"))
            {
                SelectedModel = "nomic-embed-text"
            };
        }

        public async Task<float[]> GetEmbedding(string text)
        {
            var result = await _client.EmbedAsync(text);
            return result.Embeddings.First();
        }
    }
}
