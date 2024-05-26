namespace Webshop.BookStore.Application.Test.Integration.Utilities;

public class ErrorResponse
{
    public object Result { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime TimeGenerated { get; set; }
}