using FuscaFilmes.Domain.Entities;
using FuscaFilmes.API.Models;
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.EndpointHandlers;

public class FilmesHandlers
{
    public static List<Filme> GetFilmes(Context context)
    {
        return context.Filmes
            .Include(x => x.Diretor)
            .OrderByDescending(x => x.Ano)
            .ThenByDescending(x => x.Titulo)
            .ToList();
    }

    public static List<Filme> GetFilmeById(int id, Context context)
    {
        return context.Filmes
            .Where(x => x.Id == id)
            .Include(x => x.Diretor)
            .ToList();

    }

    public static List<Filme> GetFilmeEFFunctionByTitulo(string titulo, Context context)
    {
        return context.Filmes
            .Where(x => EF.Functions.Like(x.Titulo, $"%{titulo}%"))
            .Include(x => x.Diretor)
            .ToList();
    }   

    public static List<Filme> GetFilmesContainsByTitulo(string titulo, Context context)
    {
        return context.Filmes
            .Where(x => x.Titulo.Contains(titulo))
            .Include(x => x.Diretor)
            .ToList();
    }

    public static void ExecuteDeleteFilme(Context context, int filmeId)
    {
        context.Filmes
            .Where(x => x.Id == filmeId)
            .ExecuteDelete<Filme>();
    }

    public static IResult UpdateFilme(Context context, FilmeUpdate filmeUpdate)
    {
        var filme = context.Filmes.Find(filmeUpdate.Id);

        if (filme == null)
            return Results.NotFound("Filme não encontrado");

        filme.Titulo = filmeUpdate.Titulo;
        filme.Ano = filmeUpdate.Ano;

        context.Filmes.Update(filme);
        context.SaveChanges();

        return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com sucesso!");
    }

    public static IResult ExecuteUpdateFilme(Context context, FilmeUpdate filmeUpdate)
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
    }

    public static void CreateFilme(Context context, Filme filme)
    {
        context.Filmes.Add(filme);
        context.SaveChanges();
    }
}

