using mpex.deployment.web.Models;
using System.Data.Entity;

namespace mpex.deployment.web.Services
{

    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("Connection1")
        {
        }

        public ApplicationDbContext(string DB)
            : base(DB)
        {
        }
        public DbSet<ClientInformation> Clients { get; set; }

        public DbSet<MultiFileDeployment> MultiFileDeployments { get; set; }
        public DbSet<SingleFileDeployment> SingleFileDeployments { get; set; }

        public DbSet<SafeFiles> Files { get; set; }
        public DbSet<SafeFolders> Folders { get; set; }

        public DbSet<ScriptFile> ScriptFiles { get; set; }

        public DbSet<SQLExceptionErrorLog> SQLExceptionErrorLogs { get; set; }

    }
}