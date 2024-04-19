using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_UserFavorite")]
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

