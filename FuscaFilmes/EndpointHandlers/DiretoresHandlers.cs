using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;
using System.Threading.Tasks;

namespace FuscaFilmes.EndpointHandlers;

public class DiretoresHandlers
{
    public static async Task<List<Diretor>> GetDiretoresAsync(IDirectorRepository directorRepository)
    {
        return await directorRepository.GetDiretoresAsync();
    }

    public static async Task<Diretor> GetDiretorByNameAsync(IDirectorRepository directorRepository, string name)
    {
        return await directorRepository.GetDiretorByNameAsync(name);
    }

    public static async Task<List<Diretor>> GetDiretoreByIdAsync(IDirectorRepository directorRepository, int id) 
    {
        return await directorRepository.GetDiretoreByIdAsync(id);
    }

    public static async Task AddDiretorAsync(IDirectorRepository directorRepository, Diretor diretor)
    {
        await directorRepository.AddAsync(diretor);
        await directorRepository.SaveChangesAsync();
    }

    public static async Task UpdateDiretorAsync(IDirectorRepository directorRepository, Diretor diretorNovo)
    {
        await directorRepository.UpdateAsync(diretorNovo);
        await directorRepository.SaveChangesAsync();
    }

    public static async Task DeleteDiretorAsync(IDirectorRepository directorRepository, int diretorId)
    {
        await directorRepository.DeleteAsync(diretorId);
        await directorRepository.SaveChangesAsync();
    }
}
