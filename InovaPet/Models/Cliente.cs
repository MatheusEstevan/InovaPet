using System;
using System.Collections.Generic;

namespace InovaPet.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pet = new HashSet<Pet>();
            Servico = new HashSet<Servico>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Mensalista { get; set; }
        public string Endereço { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }

        public ICollection<Pet> Pet { get; set; }
        public ICollection<Servico> Servico { get; set; }
    }
}
