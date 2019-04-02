using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Entidades
{
    public class Estoque
    {
        public virtual int IdEstoque { get; set; }
        public virtual string Nome { get; set; }
        #region Associações
        public virtual ICollection<Produto> Produtos { get; set; }
        #endregion
    }
}
