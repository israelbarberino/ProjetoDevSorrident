using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DevSorrident.Models
{
    public class ModelAtendimento
    {
        public string codAten { get; set; }

        [DisplayName("Data do Atendimento")]
        public string dataAten { get; set; }

        [DisplayName("Hora do Atendimento")]
        public string horaDen { get; set; }

        [DisplayName("Nome do Paciente")]
        public string codPac { get; set; }

        [DisplayName("Nome do Dentista")]
        public string codDen { get; set; }

        public string confAtendimento { get; set; }
    }
}