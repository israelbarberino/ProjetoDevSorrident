using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DevSorrident.Models
{
    public class ModelPaciente
    {
        public string codPac { get; set; }

        [DisplayName("Nome do Paciente")]
        public string nomePac { get; set; }

        [DisplayName("CPF")]
        public string cpfPac { get; set; }

        [DisplayName("CEP")]
        public string cepPac { get; set; }

        [DisplayName("E-mail")]
        public string emailPac { get; set; }

        [DisplayName("Telefone")]
        public string telefonePac { get; set; }

        [DisplayName("Sexo")]
        public string sexoPac { get; set; }
    }
}