namespace Webshop.BookStore.Application.Test.Integration.Utilities;

public class SkipOnCiFactAttribute : FactAttribute
{
    public SkipOnCiFactAttribute()
    {
        if (IsCiEnvironment())
        {
            Skip = "This test is skipped on CI environment";
        }
    }

    private static bool IsCiEnvironment()
    {
        var ciEnvironment = Environment.GetEnvironmentVariable("CI");
        return !string.IsNullOrEmpty(ciEnvironment);
    }
}