using Params;

namespace Repositorys.SYB1
{
    public abstract class SYB1BaseRepository<TModel> : BaseRepository<TModel> where TModel : class
    {
        public SYB1BaseRepository()
        {
            DBName = DBParam.DBName.SYB1;
            DBType = DBParam.DBType.SYBASE;
        }
    }
}
