namespace CrudApi.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}