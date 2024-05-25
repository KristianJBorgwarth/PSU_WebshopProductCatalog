using System.Text.Json;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CategoryServiceClient");
    }

    public async Task<Result<CategoryResult>> GetCategoryAsync(int categoryId)
    {
        string url = $"{categoryId}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail<CategoryResult>(
                    Errors.General.UnspecifiedError($"Error: An error occurred while retrieving category with id {categoryId}. Status code: {response.StatusCode}"));
            }

            string responseContent = await response.Content.ReadAsStringAsync();

            CategoryResponse category = JsonSerializer.Deserialize<CategoryResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            return Result.Ok(category.Result!);
        }
        catch (Exception ex)
        {
            return Result.Fail<CategoryResult>(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}