using Projeto.Entidades;
using Projeto.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Infra.Data.Context
{
    //Regra 1) Herdar DbContext..
    public class DataContext : DbContext
    {
        //Regra 2) Criar um construtor que envie para o DbContext/
        //o endereço da connectionstring..
        public DataContext() : base(ConfigurationManager.ConnectionStrings["aula"].ConnectionString)
        {

        }

        //Regra 3) Sobrescrever o método..
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //convenções do EntityFramework..
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //incluir cada classe de mapeamento..
            modelBuilder.Configurations.Add(new EstoqueMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
        }
        //Regra 4) Declarar um DbSet para cada entidade
        public DbSet<Estoque> Estoque { get; set; } //LAMBDA..
        public DbSet<Produto> Produto { get; set; } //LAMBDA..
    }
}
