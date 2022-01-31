using System.Data.Entity;

namespace mpex.deployment.web.Services
{
    public class ScriptDBContext : DbContext
    {
        public ScriptDBContext(string clientDB) : base(clientDB) { }
    }
}