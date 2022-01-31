using mpex.deployment.web.Services;
using System.ComponentModel.DataAnnotations;

namespace mpex.deployment.web.ValidationAttributes
{
    public class IsValidAppPoolAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string d = value.ToString();
                if (AppPool.Instance.PoolExists(d))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Application Pool not Exists in this Server Machin");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + "is required");
            }
        }
    }
}