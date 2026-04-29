namespace RAGWebApp.Models.Orchestration
{
    public class McpResponse
    {
        public string Answer { get; set; }
        public List<string> Sources { get; set; } = new();
    }
}
