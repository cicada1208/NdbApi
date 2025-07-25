using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("APACHE1")]
    public class APACHE1
    {
        [Key]
        public string APAUNIKEY { get; set; }

        [Key]
        public string APAUNIKEY1 { get; set; }

        public string STATUS { get; set; }

        public string KEYDATE { get; set; }

        public string HBTMARK { get; set; }

        public decimal? BT { get; set; }

        public int? SBP { get; set; }

        public int? DBP { get; set; }

        public int? MAP { get; set; }

        public int? HR { get; set; }

        public int? RR { get; set; }

        public int? PAO2 { get; set; }

        public int? AADO2 { get; set; }

        public decimal? PH { get; set; }

        public int? NA { get; set; }

        public decimal? K { get; set; }

        public decimal? CR { get; set; }

        public string CRL { get; set; }

        public decimal? HCT { get; set; }

        public decimal? WBC { get; set; }

        public int? GE { get; set; }

        public int? GM { get; set; }

        public int? GV { get; set; }

        public int? GCS { get; set; }

        public int? APS { get; set; }

        public int? SLOWC { get; set; }

        public int? TOTALSUM { get; set; }

        public string AMI { get; set; }

        public string CHF { get; set; }

        public string CAS { get; set; }

        public string ARRYTHMIA { get; set; }

        public string POSTPCI { get; set; }

        public string OTHERS { get; set; }

        public string EMRMARK { get; set; }

        public string EMRDATE { get; set; }

        public string EMRTIME { get; set; }

        public string USERCRT { get; set; }

        public string DATETIMECRT { get; set; }

        public string USERUPD { get; set; }

        public string DATETIMEUPD { get; set; }

    }
}

