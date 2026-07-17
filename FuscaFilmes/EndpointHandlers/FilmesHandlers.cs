using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Domain.Models;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FuscaFilmes.EndpointHandlers;

public class FilmesHandlers
{
    public static async Task<List<Filme>> GetFilmesAsync(IFilmeRepository filmeRepository)
    {
        return await filmeRepository.GetFilmesAsync();
    }

    public static async Task<List<Filme>> GetFilmeByIdAsync(IFilmeRepository filmeRepository, int id)
    {
        return await filmeRepository.GetFilmeByIdAsync(id);
    }

    public static async Task<List<Filme>> GetFilmeEFFunctionByTituloAsync(IFilmeRepository filmeRepository, string titulo)
    {
        return await filmeRepository.GetFilmeEFFunctionByTituloAsync(titulo);
    }   

    public static async Task<List<Filme>> GetFilmesContainsByTituloAsync(IFilmeRepository filmeRepository, string titulo)
    {
        return await filmeRepository.GetFilmesContainsByTituloAsync(titulo);
    }

    public static async Task AddFilmeAsync(IFilmeRepository filmeRepository, Filme filme)
    {
        await filmeRepository.AddAsync(filme);
        await filmeRepository.SaveChangesAsync();
    }

    public static async Task UpdateFilmeAsync(IFilmeRepository filmeRepository, FilmeUpdate filmeUpdate)
    {
        await filmeRepository.UpdateAsync(filmeUpdate);
        await filmeRepository.SaveChangesAsync();
    }

    public static async Task DeleteFilmeAsync(IFilmeRepository filmeRepository, int filmeId)
    {
        await filmeRepository.DeleteAsync(filmeId);
        await filmeRepository.SaveChangesAsync();
    }
}

