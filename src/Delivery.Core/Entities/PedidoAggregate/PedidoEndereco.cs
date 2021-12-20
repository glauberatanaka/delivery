namespace Delivery.Core.Entities.PedidoAggregate
{
    public class PedidoEndereco
    {
        public string Cep { get; private set; }
        public string Uf { get; private set; }
        public string Localidade { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }

        public PedidoEndereco()
        {
        }

        public PedidoEndereco(string cep)
        {
            Cep = cep;
        }

        public PedidoEndereco(string cep, string uf, string localidade, string numero, string complemento)
        {
            Cep = cep;
            Uf = uf;
            Localidade = localidade;
            Numero = numero;
            Complemento = complemento;
        }

        
    }
}
//{
//  "cep": "01001-000",
//  "logradouro": "Praça da Sé",
//  "complemento": "lado ímpar",
//  "bairro": "Sé",
//  "localidade": "São Paulo",
//  "uf": "SP",
//  "ibge": "3550308",
//  "gia": "1004",
//  "ddd": "11",
//  "siafi": "7107"
//}