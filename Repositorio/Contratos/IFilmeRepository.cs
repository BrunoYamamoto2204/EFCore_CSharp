using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Domain.Models;

namespace FuscaFilmes.Repo.Contratos;

public interface IFilmeRepository
{
    Task<List<Filme>> GetFilmesAsync();

    Task<List<Filme>> GetFilmeByIdAsync(int id);

    Task<List<Filme>> GetFilmeEFFunctionByTituloAsync(string titulo);

    Task<List<Filme>> GetFilmesContainsByTituloAsync(string titulo);

    Task AddAsync(Filme filme);

    Task UpdateAsync(FilmeUpdate filmeUpdate);

    Task DeleteAsync(int filmeId);

    Task<bool> SaveChangesAsync();
}

