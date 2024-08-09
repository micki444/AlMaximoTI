using AlMaximoTI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AlMaximoTI.Repositorios.Contrato;

namespace AlMaximoTI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<ProductoProveedor> _productoProvedorRepository;
        private readonly IGenericRepository<Producto> _productoRepository;


        public HomeController(ILogger<HomeController> logger, 
            IGenericRepository<ProductoProveedor> productoProvedorRepository,
            IGenericRepository<Producto> productoRepository)
        {
            _logger = logger;
            _productoProvedorRepository = productoProvedorRepository;
            _productoRepository = productoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaProductoProveedor()
        {
            List<ProductoProveedor> _lista = await _productoProvedorRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet]
        public async Task<IActionResult> ListaProducto(string clave, int tipoProducto)
        {
            List<Producto> _lista = await _productoRepository.ObtenerProductos(clave, tipoProducto);
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> guardarEmpleado([FromBody] Empleado modelo)
        {
            bool _resultado = await _empleadoRepository.Guardar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
