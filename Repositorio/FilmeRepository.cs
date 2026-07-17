using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Domain.Models;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FuscaFilmes.Repo;

public class FilmeRepository(Context _context) : IFilmeRepository
{
    public Context Context = _context;

    public async Task<List<Filme>> GetFilmesAsync()
    {
        return await Context.Filmes
                .Include(x => x.Diretores)
                .OrderByDescending(x => x.Ano)
                .OrderByDescending(X => X.Titulo)
                .ToListAsync();
    }

    public async Task<List<Filme>> GetFilmeByIdAsync(int id)
    {
        return await Context.Filmes
                .Where(x => x.Id == id)
                .Include(x => x.Diretores)
                .ToListAsync();
    }

    public async Task<List<Filme>> GetFilmeEFFunctionByTituloAsync(string titulo)
    {
        return await Context.Filmes
                .Where(x => EF.Functions.Like(x.Titulo, $"%{titulo}%"))
                .Include(x => x.Diretores)
                .ToListAsync();
    }

    public async Task<List<Filme>> GetFilmesContainsByTituloAsync(string titulo)
    {
        return await Context.Filmes
            .Where(x => x.Titulo.Contains(titulo))
            .Include(x => x.Diretores)
            .ToListAsync();
    }

    public async Task AddAsync(Filme filme)
    {
        await Context.Filmes.AddAsync(filme);
    }

    public async Task UpdateAsync(FilmeUpdate filmeUpdate)
    {
        var filme = await Context.Filmes.FindAsync(filmeUpdate.Id);

        if (filme != null)
        {
            filme.Titulo = filmeUpdate.Titulo;
            filme.Ano = filmeUpdate.Ano;

            Context.Filmes.Update(filme);
        }
    }

    public async Task DeleteAsync(int filmeId)
    {
        await Context.Filmes
            .Where(x => x.Id == filmeId)
            .ExecuteDeleteAsync<Filme>();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await Context.SaveChangesAsync()) > 0;
    }
}
