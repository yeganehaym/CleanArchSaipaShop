using SaipaShop.Domain.Exceptions;

namespace SaipaShop.Domain.Primitives;

public class PersonnelCode
{
    public string Code { get; private set; }

    private PersonnelCode(string code)
    {
        Code = code;
    }

    public static PersonnelCode Create(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            throw new EmptyException(nameof(PersonnelCode));
        }

        if (code.StartsWith("00") == false && code.Length!=8)
        {
            throw new InvalidPersonnelCode(nameof(PersonnelCode));
        }
        return new PersonnelCode(code);
    }
    
}