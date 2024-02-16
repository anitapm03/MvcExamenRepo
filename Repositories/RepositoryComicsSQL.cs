using MvcExamenComics.Models;
using System.Data;
using System.Data.SqlClient;
namespace MvcExamenComics.Repositories
{
    #region proceduress
    /*CREATE PROCEDURE SP_MODIFICAR_COMIC (
    @IDCOMIC INT,
    @NOMBRE NVARCHAR(150),
    @IMAGEN NVARCHAR(600),
    @DESCRIPCION NVARCHAR(500)
    )
    AS
    BEGIN
    UPDATE COMICS SET NOMBRE = @NOMBRE, 
    IMAGEN = @IMAGEN, DESCRIPCION = @DESCRIPCION
    WHERE IDCOMIC = @IDCOMIC
    END*/
    #endregion
public class RepositoryComicsSQL : IRepositoryComics
{

    private DataTable tablaComics;
    private SqlConnection cn;
    private SqlCommand com;

    public RepositoryComicsSQL()
    {
        string connectionString = "Data Source=DESKTOP-GDCKK71\\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=anita;Password=Cantalojas3;";
        this.cn = new SqlConnection(connectionString);
        this.com = new SqlCommand();
        this.com.Connection = this.cn;
        this.tablaComics = new DataTable();
        string sql = "SELECT * FROM COMICS";
        SqlDataAdapter ad = new SqlDataAdapter(sql, this.cn);
        ad.Fill(this.tablaComics);
    }

    public void EliminarComic(int idcomic)
    {
        string sql = "DELETE FROM COMICS WHERE IDCOMIC=@ID";
        this.com.Parameters.AddWithValue("@ID", idcomic);

        this.com.CommandType = CommandType.Text;
        this.com.CommandText = sql;
        this.cn.Open();
        int af = this.com.ExecuteNonQuery();
        this.cn.Close();
        this.com.Parameters.Clear();
    }

    public List<string> GetNombres()
    {
        var consulta = (from datos in this.tablaComics.AsEnumerable()
                 select datos.Field<string>("NOMBRE")).Distinct();
        List<string> nombres = new List<string>();
        foreach (string n in consulta)
        {
            nombres.Add(n);
        }
        return nombres;
    }

    public Comic FindByName(string nombre)
    {
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           where datos.Field<string>("NOMBRE") == nombre
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
            "@ID, @NOMBRE, @IMAGEN, @DESCRIPCION)";
        this.com.Parameters.AddWithValue("@ID", idcomic);
        this.com.Parameters.AddWithValue("@NOMBRE", nombre);
        this.com.Parameters.AddWithValue("@IMAGEN", imagen);
        this.com.Parameters.AddWithValue("@DESCRIPCION", descripcion);

        this.com.CommandType = CommandType.Text;
        this.com.CommandText = sql;
        this.cn.Open();
        int af = this.com.ExecuteNonQuery();
        this.cn.Close();
        this.com.Parameters.Clear();
    }

    public void ModificarComic(int idcomic, string nombre, string imagen, string descripcion)
    {
        string sql = "UPDATE COMICS SET NOMBRE = @NOMBRE, IMAGEN = @IMAGEN," +
            "DESCRIPCION = @DESCRIPCION WHERE IDCOMIC=@ID";
        this.com.Parameters.AddWithValue("@NOMBRE", nombre);
        this.com.Parameters.AddWithValue("@IMAGEN", imagen);
        this.com.Parameters.AddWithValue("@DESCRIPCION", descripcion);
        this.com.Parameters.AddWithValue("@ID", idcomic);

        this.com.CommandType = CommandType.Text;
        this.com.CommandText = sql;
        this.cn.Open();
        int af = this.com.ExecuteNonQuery();
        this.cn.Close();
        this.com.Parameters.Clear();
    }

    public void ModificarProcedure(int idcomic, string nombre, string imagen, string descripcion)
    {
        this.com.Parameters.AddWithValue("@IDCOMIC", idcomic);
        this.com.Parameters.AddWithValue("@NOMBRE", nombre);
        this.com.Parameters.AddWithValue("@IMAGEN", imagen);
        this.com.Parameters.AddWithValue("@DESCRIPCION", descripcion);

        this.com.CommandType = CommandType.StoredProcedure;
        this.com.CommandText = "SP_MODIFICAR_COMIC";
        this.cn.Open();
        int mod = this.com.ExecuteNonQuery();
        this.cn.Close();
        this.com.Parameters.Clear();
    }
}
}
