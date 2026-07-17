using FuscaFilmes.Domain.Entities;

namespace FuscaFilmes.Repo.Contratos;

public interface IDirectorRepository
{
    Task<List<Diretor>> GetDiretoresAsync();

    Task<Diretor> GetDiretorByNameAsync(string name);

    Task<List<Diretor>> GetDiretoreByIdAsync(int id);

    Task AddAsync(Diretor diretor);

    Task UpdateAsync(Diretor diretor);

    Task DeleteAsync(int diretor);

    Task<bool> SaveChangesAsync();
}
