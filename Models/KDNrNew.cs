using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_KDNrNew")]
    public class KDNrNew
    {
        [Key]
        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        [Key]
        public string Item_Name { get; set; }

        [Key]
        public string Item_Seq { get; set; }

        public string NrObject { get; set; }

        public string Note { get; set; }

        public string REC_STATUS { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        public string SRC_STATUS { get; set; }

    }
}

