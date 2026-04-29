using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAGWebApp.Models.Orchestration;
using RAGWebApp.Orchestration;
using RAGWebApp.Services;

namespace RAGWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly McpService _mcp;

    public IndexModel(McpService mcp)
    {
        _mcp = mcp;
    }

    [BindProperty]
    public string Question { get; set; }

    public string Answer { get; set; }

    public async Task OnPost()
    {
        var result = await _mcp.Handle(new McpRequest
        {
            Query = Question,
            UserId = "user-1"
        });

        Answer = result.Answer;
    }
}
