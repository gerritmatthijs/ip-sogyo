using Persistence;
using Tichu;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ITichuFactory factory = new();

builder.Services.AddControllers();
builder.Services.AddSingleton<ITichuRepository>(new TichuRepositoryInMemory(factory));
builder.Services.AddSingleton<ITichuFactory>(factory;);

// Needed to add info the the session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSession();

app.MapControllers();

app.Run();
