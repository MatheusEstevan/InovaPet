using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InovaPet.Models
{
    public partial class Servico
    {
        [DisplayName("Id do serviço")]
        public int IdServico { get; set; }
        public double Valor { get; set; }

        [DisplayName("Cliente")]
        public int IdCliente { get; set; }

        [DisplayName("Funcionário")]
        public int IdFuncionario { get; set; }
        [DisplayName("Pet")]
        public int IdPet { get; set; }
        [DisplayName("Data do Serviço")]
        public DateTime? DataServico { get; set; }
        public string Titulo { get; set; }

        [DisplayName("Cliente")]
        public Cliente IdClienteNavigation { get; set; }

        [DisplayName("Funcionario")]
        public Funcionario IdFuncionarioNavigation { get; set; }

        [DisplayName("Pet")]
        public Pet IdPetNavigation { get; set; }
    }
}
