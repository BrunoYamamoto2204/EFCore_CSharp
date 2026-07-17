using FuscaFilmes.EndpointHandlers;

namespace FuscaFilmes.EndpointHandlers;

public static class EndpointDiretores
{
    // Extensão do app
    public static void DiretoresEndopoints(this IEndpointRouteBuilder app)
    {
        // ------------------------------ DIRETOR - GET ------------------------------ 
        app.MapGet("/diretores", DiretoresHandlers.GetDiretoresAsync);

        // ------------------------------ DIRETOR - GET (NAME) ------------------------------ 
        app.MapGet("/diretores/agregacao/{name}", DiretoresHandlers.GetDiretorByNameAsync);

        // ------------------------------ DIRETOR - GET (ID) ------------------------------ 
        app.MapGet("/diretores/where/{id}", DiretoresHandlers.GetDiretoreByIdAsync);

        // ------------------------------ DIRETOR - POST ------------------------------ 
        app.MapPost("/diretores", DiretoresHandlers.AddDiretorAsync);

        // ------------------------------ DIRETOR - PUT ------------------------------ 
        app.MapPut("/diretores", DiretoresHandlers.UpdateDiretorAsync);

        // ------------------------------ DIRETOR - DELETE ------------------------------ 
        app.MapDelete("/diretores/{diretorId}", DiretoresHandlers.DeleteDiretorAsync);
    }
}
