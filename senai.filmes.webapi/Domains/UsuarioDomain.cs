using System.ComponentModel.DataAnnotations;

namespace senai.filmes.webapi.Domains
{
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "A senha precisa de 3 a 20 caracteres")]
        public string Senha { get; set; }
        public string Permissao { get; set; }
    }
}