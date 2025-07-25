using Params;

namespace Repositorys.Inf
{
    public abstract class InfBaseRepository<TModel> : BaseRepository<TModel> where TModel : class
    {
        public InfBaseRepository()
        {
            DBName = DBParam.DBName.Inf;
            DBType = DBParam.DBType.SQLSERVER;
        }
    }
}
