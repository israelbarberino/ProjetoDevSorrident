using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace DevSorrident.Models
{
    public class ModelLogin
    {
        public int codUsu { get; set; }


        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        [StringLength(15, ErrorMessage = "Nome de usuário deve conter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string usuario { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4)]
        [Display(Name = "Senha")]
        public string senha { get; set; }
        

        [Required]
        [Compare(nameof(senha), ErrorMessage = "Atenção! Senhas não conferem.")]
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4)]
        [Display(Name = "Confirmar senha")]
        public string confSenha { get; set; }


        [DisplayName("Defina: 1 - para usuário comum ou 2 - para administrador")]
        public string tipo { get; set; }
    }
}