﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InovaPet.Models
{
    public partial class Pet
    {
        public Pet()
        {
            Servico = new HashSet<Servico>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        [DisplayName("Cliente")]
        public int IdCliente { get; set; }
        public string Porte { get; set; }

        public string Peso { get; set; }
        public string Raca { get; set; }
        public string Tipo { get; set; }
        public string Cor { get; set; }
        public int Idade { get; set; }

        [DisplayName("Cliente")]
        public Cliente IdClienteNavigation { get; set; }
        public ICollection<Servico> Servico { get; set; }
    }
}
