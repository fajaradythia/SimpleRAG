using RAGWebApp.Utils;

namespace RAGWebApp.Services
{
    public class DataSeeder
    {
        private readonly EmbeddingService _embedding;
        private readonly VectorStoreService _store;

        public DataSeeder(EmbeddingService embedding, VectorStoreService store)
        {
            _embedding = embedding;
            _store = store;
        }

        public async Task Run()
        {
            var files = Directory.GetFiles("Data/docs", "*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var text = DocumentLoader.LoadText(file);
                var chunks = TextChunkingHelper.ChunkText(text);

                foreach (var chunk in chunks)
                {
                    var vector = await _embedding.GetEmbedding(chunk);
                    _store.Add(chunk, vector);
                }
            }
        }
    }
}
