using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;

namespace FuscaFilmes.Repo.Contratos;

public interface IDirectorRepository
{
    List<Diretor> GetDiretores();

    Diretor GetDiretorByName(string name);

    List<Diretor> GetDiretoreById(int id);

    void Add(Diretor diretor);

    void Update(Diretor diretor);

    void Delete(int diretor);

    bool SaveChanges();
}
