using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DevSorrident.Models
{
    public class ModelDentista
    {
        [DisplayName("Código do Dentista")]
        public string codDen { get; set; }

        [DisplayName("Nome do Doutor(a)")]
        public string nomeDen { get; set; }

        [DisplayName("Especialidade")]
        public string codEsp { get; set; }

        [DisplayName("Especialidade")]
        public string especialidade { get; set; }
    }
}