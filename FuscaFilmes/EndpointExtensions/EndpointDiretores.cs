using FuscaFilmes.EndpointHandlers;

namespace FuscaFilmes.EndpointHandlers;

public static class EndpointDiretores
{
    // Extensão do app
    public static void DiretoresEndopoints(this IEndpointRouteBuilder app)
    {
        // ------------------------------ DIRETOR - GET ------------------------------ 
        app.MapGet("/diretores", DiretoresHandlers.GetDiretores);

        // ------------------------------ DIRETOR - GET (NAME) ------------------------------ 
        app.MapGet("/diretores/agregacao/{name}", DiretoresHandlers.GetDiretorByName);

        // ------------------------------ DIRETOR - GET (ID) ------------------------------ 
        app.MapGet("/diretores/where/{id}", DiretoresHandlers.GetDiretoreById);

        // ------------------------------ DIRETOR - POST ------------------------------ 
        app.MapPost("/diretores", DiretoresHandlers.AddDiretor);

        // ------------------------------ DIRETOR - PUT ------------------------------ 
        app.MapPut("/diretores", DiretoresHandlers.UpdateDiretor);

        // ------------------------------ DIRETOR - DELETE ------------------------------ 
        app.MapDelete("/diretores/{diretorId}", DiretoresHandlers.DeleteDiretor);
    }
}
