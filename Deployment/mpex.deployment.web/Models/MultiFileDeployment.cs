using System;
using System.ComponentModel.DataAnnotations;

namespace mpex.deployment.web.Models
{
    public class MultiFileDeployment
    {
        public int id { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime updateDate { get; set; }
        [Display(Name = "Total Clients")]
        public string fkClientIdList { get; set; }
        public string fkFileIdList { get; set; }
        public string fkFolderIdList { get; set; }
        [Display(Name = "TargetNode File Location")]
        public string SourceFileLocation { get; set; }
        [Display(Name = "Update Status")]
        public bool UpdateStatus { get; set; }
        [Display(Name = "Process Status")]
        public bool ProcessStatus { get; set; }
    }

}