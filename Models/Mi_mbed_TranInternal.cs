using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("mi_mbed")]
    public class Mi_mbed_TranInternal : Mi_mbed_Ext
    {
        /// <summary>
        /// 備註
        /// </summary>
        [NotMapped]
        public string bed_note { get; set; }
    }
}
