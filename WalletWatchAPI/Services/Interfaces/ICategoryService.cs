using WalletWatchAPI.DTOs;
using WalletWatchAPI.Models;

namespace WalletWatchAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<TransactionCategoryDTO>> GetCategoriesAsync();
        public Task<TransactionCategoryDTO> GetCategoryByIdAsync(string id);
        public Task<TransactionCategoryDTO> AddCategoryAsync(TransactionCategoryDTO transactionCategoryDto);
        public Task<bool> UpdateCategoryAsync(string id, TransactionCategoryDTO transactionCategoryDto);
        public Task<bool> DeleteCategoryAsync(string id);
    }
}
