using System.ComponentModel.DataAnnotations;

namespace WalletWatchAPI.DTOs
{
    public class TransactionCategoryDTO
    {
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }
    }
}
