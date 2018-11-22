using System;
using System.Collections.Generic;

namespace InovaPet.Models
{
    public partial class Servico
    {
        public int IdServico { get; set; }
        public double Valor { get; set; }
        public int IdCliente { get; set; }
        public int IdFuncionario { get; set; }
        public int IdPet { get; set; }

        public Cliente IdClienteNavigation { get; set; }
        public Funcionario IdFuncionarioNavigation { get; set; }
        public Pet IdPetNavigation { get; set; }
    }
}
