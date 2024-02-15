using Persistence;
using Tichu;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ITichuFactory factory = new TichuFactory();

builder.Services.AddControllers();
builder.Services.AddSingleton<ITichuRepository>(new TichuRepositoryMySQL(factory));
builder.Services.AddSingleton<ITichuRepository>(new TichuRepositoryInMemory());
builder.Services.AddSingleton(factory);

// Needed to add info the the session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSession();

app.UseAuthentication();

app.MapControllers();

app.Run();
