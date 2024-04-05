using System.ComponentModel.DataAnnotations;

namespace WalletWatchWebApp.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
