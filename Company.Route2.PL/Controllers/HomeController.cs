using Company.Route2.PL.ModelViews;
using Company.Route2.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace Company.Route2.PL.Controllers
{
	[Authorize]

	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISingletone _singletone1;
        private readonly ISingletone _singletone2;
        private readonly ITransient _transient1;
        private readonly ITransient _transient2;
        private readonly IScoped _scoped2;
        private readonly IScoped _scoped1;

        public HomeController(ILogger<HomeController> logger,
                              ISingletone singletone1,
                              ISingletone singletone2,
                              ITransient transient1,
                              ITransient transient2,
                              IScoped scoped1,
                              IScoped scoped2
            )
        {
            _logger = logger;
            this._singletone1 = singletone1;
            this._singletone2 = singletone2;
            this._transient1 = transient1;
            this._transient2 = transient2;
            this._scoped2 = scoped2;
            this._scoped1 = scoped1;
        }
        // URL-> ..../Home/DifBetweenThe3DependencyInjectionMethod
        public string DifBetweenThe3DependencyInjectionMethod()
         {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"_singletone1 :{_singletone1.GetGuid()}\n");
            stringBuilder.Append($"_singletone2 :{_singletone2.GetGuid()}\n\n");
            stringBuilder.Append($"_transient1 :{_transient1.GetGuid()}\n");
            stringBuilder.Append($"_transient2 :{_transient2.GetGuid()}\n\n");
            stringBuilder.Append($"_scoped1 :{_scoped1.GetGuid()}\n");
            stringBuilder.Append($"_scoped2 :{_scoped2.GetGuid()}\n\n");
            return stringBuilder.ToString();
        }
        public IActionResult Index()
        {
            return View();
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
