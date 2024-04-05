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

        [HttpGet]
        public IActionResult Create() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PostAsJsonAsync("https://localhost:7234/api/Categories", category);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Notification"] = "Category imported successfully!";
                    return RedirectToAction("Index");
                }

                return View();
            }

            return View(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"https://localhost:7234/api/Categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Category deleted successfully!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var category = await httpClient.GetFromJsonAsync<CategoryViewModel>($"https://localhost:7234/api/Categories/{id}");

            if (category == null)
            {
                TempData["Notification"] = $"ERROR: Category with ID {id} does not exist.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7234/api/Categories/{category.Id}", category);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Notification"] = "Category updated successfully!";
                    return View(category);
                }
                //TODO: Logger
                return RedirectToAction("Index");
            }

            return View();
        }

        
    }
}
