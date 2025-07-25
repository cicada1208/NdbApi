using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_UserFavorite")]
    public class UserFavorite
    {
        [Key]
        public string userFavoriteId { get; set; }

        public string favoriteTypeId { get; set; }

        public string userId { get; set; }

        public string targetId { get; set; }

        public bool? isDefault { get; set; }

        public string systemUserId { get; set; }

        public DateTime? systemDt { get; set; }

    }
}

