using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:FuscaFilmesStr"])
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ------------------------------ GET ------------------------------ 
app.MapGet("/diretores", (Context context) =>
{
    return context.Diretores.Include(x => x.Filmes).ToList();
});

// ------------------------------ POST ------------------------------ 
app.MapPost("/diretores", (Context context, Diretor diretor) =>
{
    context.Diretores.Add(diretor);
    context.SaveChanges();
});

// ------------------------------ PUT ------------------------------ 
app.MapPut("/diretores/{diretorId}", (Context context, int diretorId, Diretor diretorNovo) =>
{
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
    {
        diretor.Name = diretorNovo.Name;
        if (diretorNovo.Filmes.Count > 0)
        {
            diretor.Filmes.Clear();
            foreach (var filme in diretorNovo.Filmes)
            {
                diretor.Filmes.Add(filme);
            }
        }
    }

    context.SaveChanges();
});

// ------------------------------ DELETE ------------------------------ 
app.MapDelete("/diretores/{diretorId}", (Context context, int diretorId) =>
{
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null) context.Diretores.Remove(diretor);

    context.SaveChanges();
});

app.Run();

