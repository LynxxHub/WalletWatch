using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalletWatchAPI.Data;
using WalletWatchAPI.DTOs;
using WalletWatchAPI.Models;
using WalletWatchAPI.Services.Interfaces;

namespace WalletWatchAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TransactionCategoryDTO> AddCategoryAsync(TransactionCategoryDTO transactionCategoryDto)
        {
            if (transactionCategoryDto == null)
            {
                throw new InvalidOperationException("Transaction Category DTO cannot be null!");
            }

            var category = _mapper.Map<TransactionCategory>(transactionCategoryDto);
            
            _context.Add(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<TransactionCategoryDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? throw new InvalidOperationException($"Transaction category with ID: {id} does not exist.");
            _context.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TransactionCategoryDTO>> GetCategoriesAsync()
        {
            return await _context.Categories.Select(c => _mapper.Map<TransactionCategoryDTO>(c)).ToListAsync();
        }

        public async Task<TransactionCategoryDTO> GetCategoryByIdAsync(string id)
        {
            var dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? throw new InvalidOperationException($"Transaction category with ID: {id} does not exist.");
            return _mapper.Map<TransactionCategoryDTO>(dbCategory);
        }

        public async Task<bool> UpdateCategoryAsync(string id, TransactionCategoryDTO transactionCategoryDto)
        {
            var dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? throw new InvalidOperationException($"Transaction category with ID: {transactionCategoryDto.Id} does not exist.)");

            dbCategory.Update(transactionCategoryDto);

            _context.Update(dbCategory);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

