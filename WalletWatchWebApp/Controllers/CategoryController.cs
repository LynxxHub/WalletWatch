using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;
using WalletWatchWebApp.Models.ViewModels;

namespace WalletWatchWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;

        }
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var categories = await httpClient.GetFromJsonAsync<IEnumerable<CategoryViewModel>>("https://localhost:7234/api/Categories");
            return View(categories);
        }
    }
}
