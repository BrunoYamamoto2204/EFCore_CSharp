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
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();

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

// Método DiretoresEndopoints() é extensão de app
app.DiretoresEndopoints();  
app.FilmesEndopoints();

app.Run();