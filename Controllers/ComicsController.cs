using Microsoft.AspNetCore.Mvc;
using MvcExamenComics.Models;

namespace MvcExamenComics.Controllers
{
    public class ComicsController : Controller
    {

        private IRepositoryComics repo;

        public ComicsController(IRepositoryComics repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }

        public IActionResult Detalle(int idcomic)
        {
            Comic comic = this.repo.FindComic(idcomic);
            return View(comic);
        }

        public IActionResult Modificar(int idcomic)
        {
            Comic comic = this.repo.FindComic(idcomic);
            return View(comic);
        }

        [HttpPost]
        public IActionResult Modificar(Comic comic)
        {
            this.repo.ModificarComic(comic.IdComic, comic.Nombre, comic.Imagen, comic.Descripcion);
            return RedirectToAction("Index");
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Comic comic)
        {
            this.repo.Insertar(comic.IdComic, comic.Nombre, comic.Imagen, comic.Descripcion);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int idcomic)
        {
            this.repo.EliminarComic(idcomic);
            return RedirectToAction("Index");
        }

        public IActionResult ModificarProcedure(int idcomic)
        {
            Comic comic = this.repo.FindComic(idcomic);
            return View(comic);
        }

        [HttpPost]
        public IActionResult ModificarProcedure(Comic comic)
        {
            this.repo.ModificarProcedure(comic.IdComic, comic.Nombre, comic.Imagen, comic.Descripcion);
            return RedirectToAction("Index");
        }

        public IActionResult Buscador()
        {
            ViewData["COMICS"] = this.repo.GetNombres();
            return View();
        }

        [HttpPost]
        public IActionResult Buscador(string nombre)
        {
            ViewData["COMICS"] = this.repo.GetNombres();
            Comic comic = this.repo.FindByName(nombre);
            return View(comic);
        }

        public IActionResult Borrar(int idcomic)
        {
            Comic comic = this.repo.FindComic(idcomic);
            return View(comic);
        }

        [HttpPost]
        public IActionResult Borrar(int? idcomic)
        {
            if (idcomic != -1)
            {
                this.repo.EliminarComic(idcomic.Value);
            }
            
            return RedirectToAction("Index");   
        }
    }
}
