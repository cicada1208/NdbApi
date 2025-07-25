using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("APACHE0")]
    public class APACHE0
    {
        [Key]
        public string APAUNIKEY { get; set; }

        public string STATUS { get; set; }

        public string IPDNO { get; set; }

        public string PATNO { get; set; }

        public string PATNAME { get; set; }

        public string PATBIRTH { get; set; }

        public string PATSEX { get; set; }

        public string PATAGE { get; set; }

        public string PATSOURCE { get; set; }

        public string BEDNO { get; set; }

        public string IPDDATE { get; set; }

        public string INDATE { get; set; }

        public string INTIME { get; set; }

        public string OUTDATE { get; set; }

        public string OUTTIME { get; set; }

        public string INDRNO { get; set; }

        public string INDRNAME { get; set; }

        public string DRNO { get; set; }

        public string DRNAME { get; set; }

        public string TDRNO { get; set; }

        public string TDRNAME { get; set; }

        public string IPDMARK { get; set; }

        public string INIPDDZS { get; set; }

        public string ICUPART { get; set; }

        public string ICUBEDNO { get; set; }

        public string ADDMARK { get; set; }

        public string TFHOUR { get; set; }

        public string FEHOUR { get; set; }

        public string TFLMARK { get; set; }

        public string ORMARK { get; set; }

        public string ICUINREASON { get; set; }

        public string ICUOUTREASON { get; set; }

        public string BACKREASON { get; set; }

        public string ICUINB { get; set; }

        public string ICUOUTB { get; set; }

        public string ICUOUTBDATE { get; set; }

        public string ICUOUTBTIME { get; set; }

        public string EDRNO { get; set; }

        public string EDRNAME { get; set; }

        public string ICUOUTSTATUS { get; set; }

        public string ICUOREASON { get; set; }

        public string ICUOUTAAD { get; set; }

        public string ICUOUTTHREASON { get; set; }

        public string ICUOUTHOSPNAME { get; set; }

        public string INTISS { get; set; }

        public string INTISSSCORE { get; set; }

        public string OUTTISS { get; set; }

        public string OUTTISSSCORE { get; set; }

        public string BACKPLAN { get; set; }

        public string BREASONTYPE { get; set; }

        public string BREASONTYPEOTH { get; set; }

        public string USERCRT { get; set; }

        public string DATETIMECRT { get; set; }

        public string USERUPD { get; set; }

        public string DATETIMEUPD { get; set; }

        public DateTime? LastEditDate { get; set; }

        public DateTime? CreationDate { get; set; }

    }
}

