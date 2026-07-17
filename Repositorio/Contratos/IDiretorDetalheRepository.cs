using FuscaFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuscaFilmes.Repo.Contratos
{
    public interface IDiretorDetalheRepository
    {   
        List<DiretorDetalhe> Get();

        void Add(DiretorDetalhe diretorDetalhe);

        void Update(DiretorDetalhe diretorDetalheNovo);

        void Delete(int diretorDetalheId);

        bool SaveChanges();
    }
}
