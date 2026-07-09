using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Domain.Models;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo;

public class FilmeRepository(Context _context) : IFilmeRepository
{
    public Context Context = _context;

    public List<Filme> GetFilmes()
    {
        return Context.Filmes
                .Include(x => x.Diretor)
                .OrderByDescending(x => x.Ano)
                .OrderByDescending(X => X.Titulo)
                .ToList();
    }

    public List<Filme> GetFilmeById(int id)
    {
        return Context.Filmes
                .Where(x => x.Id == id)
                .Include(x => x.Diretor)
                .ToList();
    }

    public List<Filme> GetFilmeEFFunctionByTitulo(string titulo)
    {
        return Context.Filmes
                .Where(x => EF.Functions.Like(x.Titulo, $"%{titulo}%"))
                .Include(x => x.Diretor)
                .ToList();
    }

    public List<Filme> GetFilmesContainsByTitulo(string titulo)
    {
        return Context.Filmes
            .Where(x => x.Titulo.Contains(titulo))
            .Include(x => x.Diretor)
            .ToList();
    }

    public void Add(Filme filme)
    {
        Context.Filmes.Add(filme);
    }

    public void Update(FilmeUpdate filmeUpdate)
    {
        var filme = Context.Filmes.Find(filmeUpdate.Id);

        if (filme != null)
        {
            filme.Titulo = filmeUpdate.Titulo;
            filme.Ano = filmeUpdate.Ano;

            Context.Filmes.Update(filme);
        }
    }

    public void Delete(int filmeId)
    {
        Context.Filmes
            .Where(x => x.Id == filmeId)
            .ExecuteDelete<Filme>();
    }

    public bool SaveChanges()
    {
        return Context.SaveChanges() > 0;
    }
}
