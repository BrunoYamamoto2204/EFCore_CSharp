using FuscaFilmes.EndpointHandlers;

namespace FuscaFilmes.EndpointHandlers;

public static class EndpointFilmes
{
    public static void FilmesEndopoints(this IEndpointRouteBuilder app)
    {
        // ------------------------------ FILME - GET ------------------------------ 
        app.MapGet("/filmes", FilmesHandlers.GetFilmes);

        // ------------------------------ FILME - GET (ID) ------------------------------ 
        app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmeById);

        // ------------------------------ FILME - GET BYNAME (EF) ------------------------------ 
        app.MapGet("filmesEFFunctions/filmes/byName/{titulo}", FilmesHandlers.GetFilmeEFFunctionByTitulo);

        // ------------------------------ FILME - GET BYNAME (LINQ) ------------------------------ 
        app.MapGet("filmesLinQ/filmes/byName/{titulo}", FilmesHandlers.GetFilmesContainsByTitulo);

        // ------------------------------ FILME - DELETE ------------------------------ 
        app.MapDelete("/filmes/{filmeId}", FilmesHandlers.ExecuteDeleteFilme);

        // ------------------------------ FILME - PATCH (SaveChanges) ------------------------------ 
        app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilme);

        // ------------------------------ FILME - PATCH (ExecuteUpdate) ------------------------------ 
        app.MapPatch("/filmesExecuteUpdate", FilmesHandlers.ExecuteUpdateFilme);

        // ------------------------------ FILME - POST ------------------------------ 
        app.MapPost("filmes", FilmesHandlers.CreateFilme);
    }
}
