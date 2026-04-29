using RAGWebApp.Orchestration;
using RAGWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// ==============================
// Services (Dependency Injection)
// ==============================
builder.Services.AddRazorPages();

builder.Services.AddSingleton<EmbeddingService>();
builder.Services.AddSingleton<VectorStoreService>();
builder.Services.AddSingleton<RagService>();
builder.Services.AddSingleton<ChatService>();
builder.Services.AddSingleton<DataSeeder>();

builder.Services.AddSingleton<ContextBuilder>();
builder.Services.AddSingleton<ToolRegistry>();
builder.Services.AddSingleton<McpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// ==============================
// Seed / Indexing Dokumen (RAG)
// ==============================
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

    Console.WriteLine("Starting document indexing...");

    await seeder.Run();

    Console.WriteLine("Indexing selesai!");
}

app.Run();
