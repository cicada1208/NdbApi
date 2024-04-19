using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_CSAssignMain")]
    public class CSAssignMain
    {
        [Key]
        public string csassignMainId { get; set; }

        public string loginId { get; set; }

        public string clinicalUnitId { get; set; }

        public DateTime? assignDate { get; set; }

        public string itemId { get; set; }

        public string csteamId { get; set; }

        public string timeInterval { get; set; }

        public DateTime? validDate { get; set; }

        public DateTime? mdfDate { get; set; }

        public DateTime? delDate { get; set; }

        public string systemUserId { get; set; }

    }
}

