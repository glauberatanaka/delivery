using Ardalis.GuardClauses;

namespace Delivery.Core.Entities.PedidoAggregate
{
    public class PedidoItem : BaseEntity
    {
        public int PedidoId { get; private set; }
        public Pedido Pedido { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        public PedidoItem(int pedidoId, int produtoId, string nome, string descricao, decimal preco, int quantidade)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Quantidade = quantidade;
        }

        public void AddQuantidade(int quantidade)
        {
            Guard.Against.OutOfRange(quantidade, nameof(quantidade), 0, int.MaxValue);

            Quantidade += quantidade;
        }

        public static bool operator ==(PedidoItem lhs, PedidoItem rhs)
        {
            bool status = false;
            if (lhs is null || rhs is null)
            {
                return status;
            }
            if (lhs.Nome == rhs.Nome && lhs.Descricao == rhs.Descricao
               && lhs.Preco == rhs.Preco)
            {
                status = true;
            }
            return status;
        }

        public static bool operator !=(PedidoItem lhs, PedidoItem rhs)
        {
            bool status = false;
            if (lhs is null || rhs is null)
            {
                return status;
            }
            if (lhs.Nome != rhs.Nome || lhs.Descricao != rhs.Descricao ||
               lhs.Preco != rhs.Preco)
            {
                status = true;
            }
            return status;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new System.NotImplementedException();
        }
    }
}