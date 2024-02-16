using Persistence;
using Tichu;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ITichuFactory factory = new TichuFactory();
ITichuRepository DBRepository = new TichuRepositoryMySQL(factory);
ITichuRepository memoryRepository = new TichuRepositoryInMemory();

builder.Services.AddControllers();
builder.Services.AddSingleton<List<ITichuRepository>>([DBRepository, memoryRepository]);
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
