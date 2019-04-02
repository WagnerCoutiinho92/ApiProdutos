using Projeto.Entidades;
using Projeto.Infra.Data.Context;
using Projeto.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Infra.Data.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly DataContext context;

        public ProdutoRepository(DataContext context) :base(context)
        {
            this.context = context;
        }
    }
}
