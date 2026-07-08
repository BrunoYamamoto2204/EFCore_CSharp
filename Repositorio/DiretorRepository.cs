
using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo;

public class DiretorRepository(Context _context) : IDirectorRepository
{
    public Context Context = _context;

    public List<Diretor> GetDiretores()
    {
        return Context.Diretores.Include(x => x.Filmes).ToList();
    }

    public Diretor GetDiretorByName(string name)
    {
        return Context.Diretores
            .Include(x => x.Name)
            .FirstOrDefault(x => x.Name.Contains(name))
            ?? new Diretor { Id = 5555, Name = "Marina" };
    }

    public List<Diretor> GetDiretoreById(int id)
    {
        return Context.Diretores
            .Include(x => x.Filmes)
            .Where(x => x.Id == id)
            .ToList();
    }

    public void Add(Diretor diretor)
    {
        Context.Diretores.Add(diretor);
    }

    public void Delete(int diretorId)
    {
        var diretor = Context.Diretores.Find(diretorId);
        if (diretor != null) 
            Context.Diretores.Remove(diretor);
    }

    public void Update(Diretor diretorNovo)
    {
        var diretor = Context.Diretores.Find(diretorNovo.Id);

        if (diretor != null)
        {
            diretor.Name = diretorNovo.Name;
            if (diretorNovo.Filmes.Count > 0)
            {
                diretor.Filmes.Clear();
                foreach (var filme in diretorNovo.Filmes)
                {
                    diretor.Filmes.Add(filme);
                }
            }
        }
    }

    public bool SaveChanges()
    {
        return Context.SaveChanges() > 0;
    }
}
