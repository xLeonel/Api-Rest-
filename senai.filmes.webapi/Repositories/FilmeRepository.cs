using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using senai.filmes.webapi.Interfaces;
using senai.Filmes.WebApi.Domains;



namespace senai.filmes.webapi.Repositories
{
    public class FilmeRepository : IFilmesRepository
    {
        private string StringConexao = "Data Source=DEV14\\SQLEXPRESS; initial catalog=Filmes_Tarde; user Id=sa; pwd=sa@132;";

        public List<FilmeDomain> ListarFilmes()
        {
            List<FilmeDomain> filmes = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "select * from Filmes inner join Genero on Genero.IdGenero = Filmes.IdGenero ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    //RDR = RESULTADO DA QUERY 
                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(rdr[0]),
                            Titulo = rdr["Titulo"].ToString()
                        };
                        filme.IdGenero = filme.Genero.IdGenero = Convert.ToInt32(rdr[0]);
                        filme.Genero.Nome = rdr["Nome"].ToString();

                        filmes.Add(filme);
                    }

                }
            }
            return filmes;
        }

        public FilmeDomain ListarFilmesId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "select * from Filmes inner join Genero on Genero.IdGenero = Filmes.IdGenero where IdFilme = @Id ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    SqlDataReader rdr;

                    cmd.Parameters.AddWithValue("@Id",id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FilmeDomain filmeSelecionado = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            Titulo = rdr["Titulo"].ToString()
                        };
                        filmeSelecionado.IdGenero = filmeSelecionado.Genero.IdGenero = Convert.ToInt32(rdr[0]);
                        filmeSelecionado.Genero.Nome = rdr["Nome"].ToString();
                        
                        return filmeSelecionado;
                    }

                    return null;

                }
            }
        }

        public void CadastrarFilme(FilmeDomain filmeJson)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "insert into Filmes values (@Titulo, @IdGenero)";

                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Titulo",filmeJson.Titulo);
                    cmd.Parameters.AddWithValue("@IdGenero",filmeJson.IdGenero);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtulizarFilmeId(int id, FilmeDomain filmeJson)
        {
             using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "Update Filmes set Titulo = @Titulo where IdFilme = @Id ";

                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Titulo", filmeJson.Titulo);
                    cmd.Parameters.AddWithValue("@Id", id);

                   cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletarFilmeId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "delete from Filmes where IdFilme = @Id";

                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Id", id);

                   cmd.ExecuteNonQuery();
                }
            }
        }
    }
}