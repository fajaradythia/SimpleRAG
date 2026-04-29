namespace RAGWebApp.Orchestration
{
    public class ToolRegistry
    {
        public bool IsReportQuery(string query)
        {
            return query.ToLower().Contains("laporan");
        }
    }
}
