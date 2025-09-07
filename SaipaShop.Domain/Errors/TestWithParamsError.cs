using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Domain.Errors;

public class TestWithParamsError : Error
{
    public TestWithParamsError() : base(ErrorType.Failure,"TestWithParamsError")
    {
    }

    public TestWithParamsError(string[] parameters) : base(ErrorType.Failure,"TestWithParamsError", parameters)
    {
    }
}