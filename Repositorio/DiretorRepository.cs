
using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FuscaFilmes.Repo;

public class DiretorRepository(Context _context) : IDirectorRepository
{
    public Context Context = _context;

    public async Task<List<Diretor>> GetDiretoresAsync()
    {
        return await Context.Diretores
            .Include(x => x.DiretorDetalhe)
            .Include(x => x.Filmes)
            .ToListAsync();
    }

    public async Task<Diretor> GetDiretorByNameAsync(string name)
    {
        return await Context.Diretores
            .FirstOrDefaultAsync(x => x.Name.Contains(name))
            ?? new Diretor { Id = 5555, Name = "Marina" };
    }

    public async Task<List<Diretor>> GetDiretoreByIdAsync(int id)
    {
        return await Context.Diretores
            .Include(x => x.DiretorDetalhe)
            .Include(x => x.Filmes)
            .Where(x => x.Id == id)
            .ToListAsync();
    }

    public async Task AddAsync(Diretor diretor)
    {
        if (diretor.DiretorFilmes != null && diretor.DiretorFilmes.Any())
        {
            foreach(var diretorFilme in diretor.DiretorFilmes) // Validar se tem algum filme existente ao adicionar o novo diretor
            {
                if (diretorFilme.FilmeId > 0) // Adicionando um filme já existente
                {
                    var filmeExistente = Context.Filmes.Any(x => x.Id == diretorFilme.FilmeId); // Nos filmes existentes, tem algum com o mesmo id no diretorFilme?
                    if (filmeExistente)
                    {
                        diretorFilme.Filme = null;
                    }
                }
             }
        }

        await Context.Diretores.AddAsync(diretor);
    }

    public async Task DeleteAsync(int diretorId)
    {
        var diretor = await Context.Diretores.FindAsync(diretorId);
        if (diretor != null) 
            Context.Diretores.Remove(diretor);
    }

    public async Task UpdateAsync(Diretor diretorNovo)
    {
        var diretor = await Context.Diretores
            .Include(x => x.DiretorFilmes) // Adiciona a lista de DiretorFilmes
            .FirstOrDefaultAsync(x => x.Id == diretorNovo.Id);

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

    public async Task<bool> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync() > 0;
    }
}
