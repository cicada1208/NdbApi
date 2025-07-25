using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_TeamNote")]
    public class TeamNote
    {
        [Key]
        public string REC_NO { get; set; }

        public string ClinicalUnitId { get; set; }

        public string Note { get; set; }

        public string REC_STATUS { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

    }
}

