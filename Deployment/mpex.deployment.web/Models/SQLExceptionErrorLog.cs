using System;

namespace mpex.deployment.web.Models
{
    public class SQLExceptionErrorLog
    {
        public int id { get; set; }

        public string DataBaseName { get; set; }
        public Int32 LineNumber { get; set; }
        public string Server { get; set; }
        public string Procedure { get; set; }
        public string ErrorMessage { get; set; }

        public int ScriptTextId { get; set; }

        public int DataBaseUpdateId { get; set; }
    }
}