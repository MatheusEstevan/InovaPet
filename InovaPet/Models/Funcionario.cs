using System;
using System.Collections.Generic;

namespace InovaPet.Models
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Servico = new HashSet<Servico>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Endereco { get; set; }
        public string Cpf { get; set; }

        public ICollection<Servico> Servico { get; set; }
    }
}
