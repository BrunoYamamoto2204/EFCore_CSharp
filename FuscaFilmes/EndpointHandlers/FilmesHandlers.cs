using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Domain.Models;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.EndpointHandlers;

public class FilmesHandlers
{
    public static List<Filme> GetFilmes(IFilmeRepository filmeRepository)
    {
        return filmeRepository.GetFilmes();
    }

    public static List<Filme> GetFilmeById(IFilmeRepository filmeRepository, int id)
    {
        return filmeRepository.GetFilmeById(id);
    }

    public static List<Filme> GetFilmeEFFunctionByTitulo(IFilmeRepository filmeRepository, string titulo)
    {
        return filmeRepository.GetFilmeEFFunctionByTitulo(titulo);
    }   

    public static List<Filme> GetFilmesContainsByTitulo(IFilmeRepository filmeRepository, string titulo)
    {
        return filmeRepository.GetFilmesContainsByTitulo(titulo);
    }

    public static void AddFilme(IFilmeRepository filmeRepository, Filme filme)
    {
        filmeRepository.Add(filme);
        filmeRepository.SaveChanges();
    }

    public static void UpdateFilme(IFilmeRepository filmeRepository, FilmeUpdate filmeUpdate)
    {
        filmeRepository.Update(filmeUpdate);
        filmeRepository.SaveChanges();
    }

    public static void DeleteFilme(IFilmeRepository filmeRepository, int filmeId)
    {
        filmeRepository.Delete(filmeId);
        filmeRepository.SaveChanges();
    }

    //public static IResult ExecuteUpdateFilme(Context context, FilmeUpdate filmeUpdate)
    //{
    //    var linhasAfetadas = context.Filmes
    //        .Where(x => x.Id == filmeUpdate.Id)
    //        .ExecuteUpdate(setter => setter
    //            .SetProperty(f => f.Titulo, filmeUpdate.Titulo)
    //            .SetProperty(f => f.Ano, filmeUpdate.Ano)
    //        );

    //    if (linhasAfetadas > 0)
    //    {
    //        return Results.Ok($"você teve um total de {linhasAfetadas} Linha(s) afetada(s)");
    //    }
    //    else
    //    {
    //        return Results.NoContent();
    //    }
    //}
}

