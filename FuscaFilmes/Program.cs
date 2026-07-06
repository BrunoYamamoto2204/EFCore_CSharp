using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;
using FuscaFilmes.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options => options
    .UseSqlite(builder.Configuration["ConnectionStrings:FuscaFilmesStr"])
    .LogTo(Console.WriteLine, LogLevel.Information)
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

// ------------------------------ FILME - GET ------------------------------ 
app.MapGet("/filmes", (Context context) =>
{
    return context.Filmes
        .Include(x => x.Diretor)
        .OrderByDescending(x => x.Ano)
        .ThenByDescending(x => x.Titulo)
        .ToList();
});

// ------------------------------ FILME - GET (ID) ------------------------------ 
app.MapGet("/filmes/{id}", (int id, Context context) =>
{
    return context.Filmes
        .Where(x => x.Id == id)
        .Include(x => x.Diretor)
        .ToList();
});

// ------------------------------ FILME - BYNAME (EF) ------------------------------ 
app.MapGet("filmesEFFunctions/filmes/byName/{titulo}", (string titulo, Context context) =>
{
    return context.Filmes
         .Where(x => EF.Functions.Like(x.Titulo, $"%{titulo}%"))
         .Include(x => x.Diretor)
         .ToList();
});

// ------------------------------ FILME - BYNAME (LINQ) ------------------------------ 
app.MapGet("filmesLinQ/filmes/byName/{titulo}", (string titulo, Context context) =>
{
    return context.Filmes
        .Where(x => x.Titulo.Contains(titulo))
        .Include(x => x.Diretor)
        .ToList();
});

// ------------------------------ FILME - POST ------------------------------ 
app.MapPost("filmes", (Context context, Filme filme) =>
{
    context.Filmes.Add(filme);
    context.SaveChanges();
}); 

// ------------------------------ FILME - PATCH (SaveChanges) ------------------------------ 
app.MapPatch("/filmesUpdate", (Context context, FilmeUpdate filmeUpdate) =>
{
    var filme = context.Filmes.Find(filmeUpdate.Id);

    if (filme == null)
        return Results.NotFound("Filme não encontrado");

    filme.Titulo = filmeUpdate.Titulo;
    filme.Ano = filmeUpdate.Ano;

    context.Filmes.Update(filme);
    context.SaveChanges();

    return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com sucesso!");
});

// ------------------------------ FILME - PATCH (ExecuteUpdate) ------------------------------ 
app.MapPatch("/filmesExecuteUpdate", (Context context, FilmeUpdate filmeUpdate) =>
{
    var linhasAfetadas = context.Filmes
        .Where(x => x.Id == filmeUpdate.Id)
        .ExecuteUpdate(setter => setter
            .SetProperty(f => f.Titulo, filmeUpdate.Titulo)
            .SetProperty(f => f.Ano, filmeUpdate.Ano)
        );

    if (linhasAfetadas > 0)
    {
        return Results.Ok($"você teve um total de {linhasAfetadas} Linha(s) afetada(s)");
    }
    else
    {
        return Results.NoContent();
    }
});

// ------------------------------ FILME - DELETE ------------------------------ 
app.MapDelete("/filmes/{filmeId}", (Context context, int filmeId) =>
{
    context.Filmes
        .Where(x => x.Id == filmeId)
        .ExecuteDelete<Filme>();
});



// ------------------------------ DIRETOR - GET ------------------------------ 
app.MapGet("/diretores", (Context context) =>
{
    return context.Diretores.Include(x => x.Filmes).ToList();
});

// ------------------------------ DIRETOR - GET (ID) ------------------------------ 
app.MapGet("/diretores/agregacao/{name}", (string name, Context context) =>
{
    return context.Diretores
        .Include(x => x.Filmes)
        .Where(x => x.Name.Contains(name))
        .Select(x => x.Name)
        .FirstOrDefault()
        ?? "Maria";
});

// ------------------------------ DIRETOR - GET (ID) ------------------------------ 
app.MapGet("/diretores/where/{id}", (int id, Context context) =>
{
    return context.Diretores
        .Include(x => x.Filmes)
        .Where(x => x.Id == id)
        .ToList();
});

// ------------------------------ DIRETOR - POST ------------------------------ 
app.MapPost("/diretores", (Context context, Diretor diretor) =>
{
    context.Diretores.Add(diretor);
    context.SaveChanges();
});

// ------------------------------ DIRETOR - PUT ------------------------------ 
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

// ------------------------------ DIRETOR - DELETE ------------------------------ 
app.MapDelete("/diretores/{diretorId}", (Context context, int diretorId) =>
{
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null) context.Diretores.Remove(diretor);

    context.SaveChanges();
});

app.Run();