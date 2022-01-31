using mpex.deployment.web.Services;
using System.ComponentModel.DataAnnotations;

namespace mpex.deployment.web.ValidationAttributes
{
    public class IsValidFileLocationAttributes : ValidationAttribute   
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null) 
            {
                string l = value.ToString();
                if (FileTransfer.objFileTransfer.FileLocationExists(l)) 
                {
                    return ValidationResult.Success;
                }
                else 
                {
                    return new ValidationResult("File location not Exists ");
                }
            }
            else 
            {
                return new ValidationResult(""+ validationContext.DisplayName + "is required" );
            }
        }
    }
}