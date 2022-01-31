using mpex.deployment.web.Services;
using System.ComponentModel.DataAnnotations;

namespace mpex.deployment.web.ValidationAttributes
{
    public class IsValidDatabaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string d = value.ToString();
                if (ScriptUpdate.ChechDBExists(d))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Database not Exists or Connection string problems");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + "is required");
            }
        }
    }
}