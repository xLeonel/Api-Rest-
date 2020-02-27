using senai.filmes.webapi.Domains;

namespace senai.filmes.webapi.Interfaces
{
    public interface IUsuarioRepository
    {
        UsuarioDomain BuscarPorEmailSenha(string email, string senha);
    }
}