using Projeto.Infra.Data.Context;
using Projeto.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        private DbContextTransaction transaction;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public IEstoqueRepository EstoqueRepository
        {
            get { return new EstoqueRepository(context); }
        }

        public IProdutoRepository ProdutoRepository
        {
            get { return new ProdutoRepository(context); }
        }
    }
}
