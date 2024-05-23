namespace Webshop.BookStore.Application.Services.CategoryService;

public class CategoryResponse
{
    public CategoryResult? Result { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime TimeGenerated { get; set; }
}

public class CategoryResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ParentId { get; set; }
    public List<CategoryResult> Categories { get; set; }
}

