using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_RecShortCode")]
    public class RecShortCode 
    {
        [Key]
        public string RECMODEL { get; set; }

        [Key]
        public string GROUPCODE { get; set; }

        [Key]
        public string SHORTCODE { get; set; }

        [Key]
        public string CODEVER { get; set; }

        public string FULLNAME { get; set; }

        public string CONTEXT01 { get; set; }

        public string CONTEXT02 { get; set; }

        public string CONTEXT03 { get; set; }

        public string CONTEXT04 { get; set; }

        public string CONTEXT05 { get; set; }

        public string GROUPSEQ { get; set; }

        public string CODESEQ { get; set; }

        public string isActive { get; set; }

        public string UUserNO { get; set; }

        public string UDateTime { get; set; }

    }
}

