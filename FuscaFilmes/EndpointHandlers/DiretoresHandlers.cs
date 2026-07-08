using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;

namespace FuscaFilmes.EndpointHandlers;

public class DiretoresHandlers
{
    public static List<Diretor> GetDiretores(IDirectorRepository directorRepository)
    {
        return directorRepository.GetDiretores();
    }

    public static Diretor GetDiretorByName(IDirectorRepository directorRepository, string name)
    {
        return directorRepository.GetDiretorByName(name);
    }

    public static List<Diretor> GetDiretoreById(IDirectorRepository directorRepository, int id) 
    {
        return directorRepository.GetDiretoreById(id);
    }

    public static void AddDiretor(IDirectorRepository directorRepository, Diretor diretor)
    {   
        directorRepository.Add(diretor);
        directorRepository.SaveChanges();
    }

    public static void UpdateDiretor(IDirectorRepository directorRepository, Diretor diretorNovo)
    {
        directorRepository.Update(diretorNovo);
        directorRepository.SaveChanges();
    }

    public static void DeleteDiretor (IDirectorRepository directorRepository, int diretorId)
    {
        directorRepository.Delete(diretorId);
        directorRepository.SaveChanges();
    }
}
