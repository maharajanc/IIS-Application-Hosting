using mpex.deployment.web.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace mpex.deployment.web.Models
{
    public class ClientInformation
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Client")]
        public string ClientName { get; set; }
        [Required]
        [Display(Name = "IIS File Location")]
        [IsValidFileLocationAttributes]
        public string IISFileLocation { get; set; }
        [Required]
        [Display(Name = "IIS Pool Name")]
       // [IsValidAppPoolAttributes]
        public string IISPoolName { get; set; }
        [Display(Name = "Active")]
        public bool boolIsActive { get; set; }
        [Required]
        [Display(Name = "Database Connection String")]
        [IsValidDatabaseAttribute]
        public string DBConnectionString { get; set; }

        public bool boolIsSelected { get; set; }

        public string DBName { get; set; }
    }
}