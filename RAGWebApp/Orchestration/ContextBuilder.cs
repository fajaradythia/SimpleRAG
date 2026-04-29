using RAGWebApp.Services;

namespace RAGWebApp.Orchestration
{
    public class ContextBuilder
    {
        private readonly EmbeddingService _embedding;
        private readonly VectorStoreService _store;

        public ContextBuilder(
            EmbeddingService embedding,
            VectorStoreService store)
        {
            _embedding = embedding;
            _store = store;
        }

        public async Task<List<string>> Build(string query)
        {
            var vector = await _embedding.GetEmbedding(query);

            var contexts = _store.Search(vector, topK: 3);

            return contexts;
        }
    }
}
