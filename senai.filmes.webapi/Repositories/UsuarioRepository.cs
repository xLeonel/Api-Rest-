using System;
using System.Data.SqlClient;
using senai.filmes.webapi.Domains;
using senai.filmes.webapi.Interfaces;

namespace senai.filmes.webapi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string StringConexao = "Data Source=DEV14\\SQLEXPRESS; initial catalog=Filmes_Tarde; user Id=sa; pwd=sa@132;";
        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "select IdUsuario, Email, Senha, Permissao from Usuarios where Email = @Email and Senha = @Senha";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        UsuarioDomain usuario = new UsuarioDomain();

                        while (reader.Read())
                        {
                            usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            usuario.Email = reader["Email"].ToString();
                            usuario.Senha = reader["Senha"].ToString();
                            usuario.Permissao = reader["Permissao"].ToString();
                        }
                        return usuario;
                    }
                    return null;
                }
            }
        }
    }
}