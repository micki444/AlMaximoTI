using AlMaximoTI.Models;
using AlMaximoTI.Repositorios.Contrato;
using Microsoft.AspNetCore.Mvc;

namespace AlMaximoTI.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IGenericRepository<Producto> _productoRepository;

        public ProductoController(ILogger<ProductoController> logger,
            IGenericRepository<Producto> productoRepository)
        {
            _logger = logger;
            _productoRepository = productoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertarActualizarProducto([FromBody] Producto modelo)
        {
            bool _resultado = await _productoRepository.Guardar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }
    }
}
