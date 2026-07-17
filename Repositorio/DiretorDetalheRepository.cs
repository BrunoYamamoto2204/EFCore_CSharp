using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;


namespace FuscaFilmes.Repo
{
    public class DiretorDetalheRepository(Context _context) : IDiretorDetalheRepository
    {
        public Context Context = _context;

        public void Add(DiretorDetalhe diretorDetalhe)
        {
            Context.DiretorDetalhe.Add(diretorDetalhe);
        }

        public void Delete(int diretorDetalheId)
        {
            Context.DiretorDetalhe
                .Where(x => x.Id == diretorDetalheId)
                .ExecuteDelete<DiretorDetalhe>();
        }

        public List<DiretorDetalhe> Get()
        {
            return Context.DiretorDetalhe
                .Include(x => x.Diretor)
                .ToList();
        }

        public void Update(DiretorDetalhe diretorDetalheNovo)
        {
            Context.DiretorDetalhe
                .Where(x => x.Id == diretorDetalheNovo.Id)
                .ExecuteUpdate<DiretorDetalhe>(x => x
                    .SetProperty(p => p.Biografia, diretorDetalheNovo.Biografia)
                    .SetProperty(p => p.DataNascimento, diretorDetalheNovo.DataNascimento)
                    .SetProperty(p => p.DiretorId, diretorDetalheNovo.DiretorId)
                );
        }

        public bool SaveChanges()
        {
            return Context.SaveChanges() > 0;
        }
    }
}
