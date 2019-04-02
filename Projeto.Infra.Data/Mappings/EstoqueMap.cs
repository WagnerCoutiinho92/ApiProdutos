using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Infra.Data.Mappings
{
    public class EstoqueMap : EntityTypeConfiguration<Estoque>
    {
        public EstoqueMap()
        {
            //chave primária..
            HasKey(e => e.IdEstoque);
            //demais propriedades..
            Property(e => e.Nome)
            .HasMaxLength(50)
            .IsRequired();
        }
    }
}
