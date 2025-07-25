using Params;

namespace Repositorys.NIS
{
    public abstract class NISBaseRepository<TModel> : BaseRepository<TModel> where TModel : class
    {
        public NISBaseRepository()
        {
            DBName = DBParam.DBName.NIS;
            DBType = DBParam.DBType.SYBASE;
        }
    }
}
