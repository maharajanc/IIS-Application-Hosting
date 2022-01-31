
namespace mpex.deployment.web.Models
{
    public class SafeFiles
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }
    }
    public class SafeFolders
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }
    }
}