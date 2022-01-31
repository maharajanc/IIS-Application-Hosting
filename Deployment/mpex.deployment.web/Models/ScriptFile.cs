using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mpex.deployment.web.Models
{
    public class ScriptFile
    {
        public int id { get; set; }
        [Display(Name = "Clients")]

        public string fkClientIdList { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        // [Display(Name = "Source File Location")]
        //public string SourceFileLocation { get; set; }

        public Byte[] byteSource { get; set; }
        [Display(Name = "Update Status")]
        public bool UpdateStatus { get; set; }

        public bool ProcessStatus { get; set; }

        // public String Script { get; set; }

        public List<String> Scripts { get; set; }
    }
}