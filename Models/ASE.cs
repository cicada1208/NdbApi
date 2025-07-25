using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_ASE")]
    public class ASE
    {
        [Key]
        public string ptEncounterID { get; set; }

        [Key]
        public string DOC_CODE { get; set; }

        public string DOC_TYPE { get; set; }

        public string PT_NO { get; set; }

        public string ASE_DT { get; set; }

        public string ASE_TIME { get; set; }

        public string ASE_MAN { get; set; }

        public string MD_MAN { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

    }
}

