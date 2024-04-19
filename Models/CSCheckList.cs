using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_CSCheckList")]
    public class CSCheckList
    {
        [Key]
        public string cscheckListId { get; set; }

        public string clinicalUnitId { get; set; }

        public string itemName { get; set; }

        public short? showSEQ { get; set; }

        public DateTime? validDate { get; set; }

        public DateTime? mdfDate { get; set; }

        public DateTime? delDate { get; set; }

        public string systemUserId { get; set; }

    }
}

