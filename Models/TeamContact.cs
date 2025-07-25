using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_TeamContact")]
    public class TeamContact
    {
        [Key]
        public string REC_NO { get; set; }

        public string ClinicalUnitId { get; set; }

        public string TeamType { get; set; }

        public string EmpNo { get; set; }

        public string EmpName { get; set; }

        public string Ext { get; set; }

        public string Mvpn { get; set; }

        public string REC_STATUS { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

    }
}

