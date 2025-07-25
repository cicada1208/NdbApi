using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_RECSerialNo")]
    public class RECSerialNo
    {
        [Key]
        public string SYSID { get; set; }

        [Key]
        public string SDATE { get; set; }

        public string NUM { get; set; }

    }
}
