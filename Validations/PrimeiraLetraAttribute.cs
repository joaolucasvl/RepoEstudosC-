using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations;

public class PrimeiraLetraAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            var primeiraLetra = value.ToString()![0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra deve ser maiúscula.");
            }
        }
        return ValidationResult.Success;
    }
}
