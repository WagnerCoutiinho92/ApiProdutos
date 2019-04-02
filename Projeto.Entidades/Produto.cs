using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Entidades
{
    public class Produto
    {
        public virtual int IdProduto { get; set; }
        public virtual string Nome { get; set; }
        public virtual decimal Preco { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual int IdEstoque { get; set; }

        #region Associações
        public virtual Estoque Estoque { get; set; }
        #endregion
    }
}
