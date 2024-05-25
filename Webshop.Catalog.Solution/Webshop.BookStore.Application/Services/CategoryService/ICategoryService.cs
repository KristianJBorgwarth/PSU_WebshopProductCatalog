using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Services.CategoryService;

public interface ICategoryService
{
    Task<Result<CategoryResult>> GetCategoryAsync(int categoryId);
}