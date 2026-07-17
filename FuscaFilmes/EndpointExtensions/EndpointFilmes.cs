using FuscaFilmes.EndpointHandlers;

namespace FuscaFilmes.EndpointHandlers;

public static class EndpointFilmes
{
    public static void FilmesEndopoints(this IEndpointRouteBuilder app)
    {
        // ------------------------------ FILME - GET ------------------------------ 
        app.MapGet("/filmes", FilmesHandlers.GetFilmesAsync);

        // ------------------------------ FILME - GET (ID) ------------------------------ 
        app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmeByIdAsync);

        // ------------------------------ FILME - GET BYNAME (EF) ------------------------------ 
        app.MapGet("filmesEFFunctions/filmes/byName/{titulo}", FilmesHandlers.GetFilmeEFFunctionByTituloAsync);

        // ------------------------------ FILME - GET BYNAME (LINQ) ------------------------------ 
        app.MapGet("filmesLinQ/filmes/byName/{titulo}", FilmesHandlers.GetFilmesContainsByTituloAsync);

        // ------------------------------ FILME - DELETE ------------------------------ 
        app.MapDelete("/filmes/{filmeId}", FilmesHandlers.DeleteFilmeAsync);

        // ------------------------------ FILME - PATCH ------------------------------ 
        app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilmeAsync);

        // ------------------------------ FILME - POST ------------------------------ 
        app.MapPost("filmes", FilmesHandlers.AddFilmeAsync);
    }
}
