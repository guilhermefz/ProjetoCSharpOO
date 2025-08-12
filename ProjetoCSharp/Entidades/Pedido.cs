using ProjetoCSharp.Enums;

namespace ProjetoCSharp.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Criacao { get; set; }
        public StatusPedido Status { get; set; }

        public override string ToString()
        {
            return Id + ", " + Criacao + "," + Status;
        }
    }
}
