using MvcExamenComics.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MvcExamenComics.Repositories
{
    public class RepositoryComicsOracle: IRepositoryComics
    {
        private DataTable tablaComics;
        private OracleConnection cn;
        private OracleCommand com;

        public RepositoryComicsOracle()
        {
            string connectionString =
                @"Data Source=LOCALHOST:1521/XE;Persist Security Info=True;User Id=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            string sql = "SELECT * FROM COMICS";
            OracleDataAdapter ad = new OracleDataAdapter(sql, this.cn);
            this.tablaComics = new DataTable();
            ad.Fill(this.tablaComics);
        }

        public void EliminarComic(int idcomic)
        {
            string sql = "DELETE FROM COMICS WHERE IDCOMIC=:ID";
            OracleParameter pamId = new OracleParameter(":ID", idcomic);
            this.com.Parameters.Add(pamId);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Comic FindComic(int idcomic)
        {
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           where datos.Field<int>("IDCOMIC") == idcomic
                           select datos;
            var row = consulta.First();
            Comic comic = new Comic
            {
                IdComic = row.Field<int>("IDCOMIC"),
                Nombre = row.Field<string>("NOMBRE"),
                Imagen = row.Field<string>("IMAGEN"),
                Descripcion = row.Field<string>("DESCRIPCION")
            };
            return comic;
        }

        public List<Comic> GetComics()
        {
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           select datos;
            List<Comic> comics = new List<Comic>();

            foreach (var row in consulta)
            {
                Comic comic = new Comic
                {
                    IdComic = row.Field<int>("IDCOMIC"),
                    Nombre = row.Field<string>("NOMBRE"),
                    Imagen = row.Field<string>("IMAGEN"),
                    Descripcion = row.Field<string>("DESCRIPCION")
                };
                comics.Add(comic);
            }
            return comics;
        }

        public void Insertar(int idcomic, string nombre, string imagen, string descripcion)
        {
            string sql = "INSERT INTO COMICS VALUES(" +
                ":ID, :NOMBRE, :IMAGEN, :DESCRIPCION)";
            OracleParameter pamId = new OracleParameter(":ID", idcomic);
            this.com.Parameters.Add(pamId);
            OracleParameter pamNombre = new OracleParameter(":NOMBRE", nombre);
            this.com.Parameters.Add(pamNombre);
            OracleParameter pamImagen = new OracleParameter(":IMAGEN", imagen);
            this.com.Parameters.Add(pamImagen);
            OracleParameter pamDesc = new OracleParameter(":DESCRIPCION", descripcion);
            this.com.Parameters.Add(pamDesc);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void ModificarComic(int idcomic, string nombre, string imagen, string descripcion)
        {
            string sql = "UPDATE COMICS SET NOMBRE = :NOMBRE, IMAGEN = :IMAGEN," +
               "DESCRIPCION = :DESCRIPCION WHERE IDCOMIC=:ID";
            
            OracleParameter pamNombre = new OracleParameter(":NOMBRE", nombre);
            this.com.Parameters.Add(pamNombre);
            OracleParameter pamImagen = new OracleParameter(":IMAGEN", imagen);
            this.com.Parameters.Add(pamImagen);
            OracleParameter pamDesc = new OracleParameter(":DESCRIPCION", descripcion);
            this.com.Parameters.Add(pamDesc);
            OracleParameter pamId = new OracleParameter(":ID", idcomic);
            this.com.Parameters.Add(pamId);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
