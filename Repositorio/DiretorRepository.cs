
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
        return Context.Diretores
            .Include(x => x.DiretorDetalhe)
            .Include(x => x.Filmes)
            .ToList();
    }

    public Diretor GetDiretorByName(string name)
    {
        return Context.Diretores
            .FirstOrDefault(x => x.Name.Contains(name))
            ?? new Diretor { Id = 5555, Name = "Marina" };
    }

    public List<Diretor> GetDiretoreById(int id)
    {
        return Context.Diretores
            .Include(x => x.DiretorDetalhe)
            .Include(x => x.Filmes)
            .Where(x => x.Id == id)
            .ToList();
    }

    public void Add(Diretor diretor)
    {
        if (diretor.DiretorFilmes != null && diretor.DiretorFilmes.Any())
        {
            foreach(var diretorFilme in diretor.DiretorFilmes)
            {
                if (diretorFilme.FilmeId > 0) // Adicionando um filme já existente
                {
                    var filmeExistente = Context.Filmes.Any(x => x.Id == diretorFilme.FilmeId);
                    if (filmeExistente)
                    {
                        diretorFilme.Filme = null;
                    }
                }
             }
        }

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
        var diretor = Context.Diretores
            .Include(x => x.DiretorFilmes) // Adiciona a lista de DiretorFilmes
            .FirstOrDefault(x => x.Id == diretorNovo.Id);

        if (diretor != null)
        {
            diretor.Name = diretorNovo.Name;
            if (diretorNovo.DiretorFilmes.Count > 0)
            {
                diretor.DiretorFilmes.Clear();
                foreach (var diretorFilmes in diretorNovo.DiretorFilmes)
                {
                    diretor.DiretorFilmes.Add(diretorFilmes);
                }
            }
        }
    }

    public bool SaveChanges()
    {
        return Context.SaveChanges() > 0;
    }
}
