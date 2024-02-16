namespace MvcExamenComics.Models
{
    public interface IRepositoryComics
    {
        List<Comic> GetComics();
        Comic FindComic(int idcomic);

        void EliminarComic(int idcomic);
        void ModificarComic
            (int idcomic, string nombre, string imagen, string descripcion);

        void ModificarProcedure
            (int idcomic, string nombre, string imagen, string descripcion);

        void Insertar
            (int idcomic, string nombre, string imagen, string descripcion);

        Comic FindByName(string nombre);

        List<string> GetNombres();
    }
}
