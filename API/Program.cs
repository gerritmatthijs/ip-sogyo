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

var app = builder.Build();

app.UseAuthentication();

app.MapControllers();

app.Run();
