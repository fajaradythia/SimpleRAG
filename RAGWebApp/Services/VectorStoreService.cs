namespace RAGWebApp.Services
{
    public class VectorStoreService
    {
        public List<(string Text, float[] Vector)> Store = new();

        public void Add(string text, float[] vector)
        {
            Store.Add((text, vector));
        }

        public List<string> Search(float[] query, int topK = 3)
        {
            return Store
                .Select(x => new
                {
                    x.Text,
                    Score = CosineSimilarity(query, x.Vector)
                })
                .OrderByDescending(x => x.Score)
                .Take(topK)
                .Select(x => x.Text)
                .ToList();
        }

        private float CosineSimilarity(float[] a, float[] b)
        {
            float dot = 0, magA = 0, magB = 0;

            for (int i = 0; i < a.Length; i++)
            {
                dot += a[i] * b[i];
                magA += a[i] * a[i];
                magB += b[i] * b[i];
            }

            return dot / (MathF.Sqrt(magA) * MathF.Sqrt(magB));
        }
    }
}
