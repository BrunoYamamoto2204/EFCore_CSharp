using FuscaFilmes.EndpointHandlers;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FuscaFilmes.Repo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options => options
    .UseSqlite(builder.Configuration["ConnectionStrings:FuscaFilmesStr"])
    .LogTo(Console.WriteLine, LogLevel.Information)
);

builder.Services.AddScoped<IDirectorRepository, DiretorRepository>();

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

// Método DiretoresEndopoints() é extensão de app
app.DiretoresEndopoints();  
app.FilmesEndopoints();

app.Run();