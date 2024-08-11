using AlMaximoTI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AlMaximoTI.Repositorios.Contrato;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlMaximoTI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<ProductoProveedor> _productoProvedorRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IGenericRepository<TipoProducto> _tipoProductoRepository;


        public HomeController(ILogger<HomeController> logger, 
            IGenericRepository<ProductoProveedor> productoProvedorRepository,
            IGenericRepository<Producto> productoRepository,
            IGenericRepository<TipoProducto> tipoProductoRepository)
        {
            _logger = logger;
            _productoProvedorRepository = productoProvedorRepository;
            _productoRepository = productoRepository;
            _tipoProductoRepository = tipoProductoRepository;
        }

        public async Task<IActionResult> Index()
        {
           
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaProductoProveedor()
        {
            List<ProductoProveedor> _lista = await _productoProvedorRepository.ObtenerTodos();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string clave, string tipo)
        {
            
            List<Producto> _lista = await _productoRepository.Buscar(clave, tipo);
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet] 
        public async Task<IActionResult> ObtenerTodos()
        {

            List<Producto> _lista = await _productoRepository.ObtenerTodos();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> InsertarActualizarProducto([FromBody] Producto modelo)
        {
            bool _resultado = await _productoRepository.Guardar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });
        }

        [HttpPut]
        public async Task<IActionResult> editarProduto([FromBody] Producto modelo)
        {
            bool _resultado = await _productoRepository.Editar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            bool _resultado = await _productoRepository.Eliminar(idProducto);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTiposProducto()
        {

            List<TipoProducto> _lista = await _tipoProductoRepository.ObtenerTodos();
            return StatusCode(StatusCodes.Status200OK, _lista);
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
