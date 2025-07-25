using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_Users")]
    public class Users
    {
        [Key]
        public string userId { get; set; }

        public string loginId { get; set; }

        public string userName { get; set; }

        [Display(Name = "使用者簡稱")]
        public string userTerseName { get; set; }

        public string employeeNo { get; set; }

        public string employeeDept { get; set; }

        public string proTitleId { get; set; }

        public string proTitleLevelId { get; set; }

        public bool? isService { get; set; }

        public bool? isTester { get; set; }

        public string dimission { get; set; }

        public string DutyCode { get; set; }

        public string empCategory { get; set; }

        public bool? isActive { get; set; }

        public string systemUserId { get; set; }

        public DateTime? systemDt { get; set; }

        [NotMapped]
        public string password { get; set; }

        [NotMapped]
        public string token { get; set; }

        [NotMapped]
        public bool? loggedIn { get; set; }

    }
}
