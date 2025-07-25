using Lib;
using Models;
using Params;

namespace Repositorys
{
    public class DBRepository : BaseRepository<DB>
    {
        /// <summary>
        /// 查詢DB連線主機是否為正式區
        /// </summary>
        public ApiResult<DB> GetDB(DB db, int option = 0)
        {
            if (db.DBName == DBParam.DBName.SYB2)
                db.IsFormal = DBUtil.GetConnString(db.DBName).Contains("Data Source='s2'");
            else
                db.IsFormal = DBUtil.GetConnString(db.DBName).Contains("Data Source='s1'");

            db.Description = (bool)db.IsFormal ? "【正式版】" : "【測試版】";

            var result = new ApiResult<DB>(db);
            return result;
        }
    }
}
