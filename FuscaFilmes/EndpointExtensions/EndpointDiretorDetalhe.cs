using FuscaFilmes.API.EndpointHandlers;

namespace FuscaFilmes.API.EndpointExtensions
{
    public static class EndpointDiretorDetalhe
    {
        public static void DiretorDetalheEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/diretoresDetalhe/", DiretorDetalheHandler.GetDiretorDetalhes);
            app.MapPost("/diretoresDetalhe/", DiretorDetalheHandler.AddDiretorDetalhe);
            app.MapPut("/diretoresDetalhe/{id}", DiretorDetalheHandler.UpdateDiretorDetalhe);
            app.MapDelete("/diretoresDetalhe/{id}", DiretorDetalheHandler.DeleteDiretorDetalhe);
        }
    }
}
