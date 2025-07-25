using Params;

namespace Repositorys.PeriPhery
{
    public abstract class PeriPheryBaseRepository<TModel> : BaseRepository<TModel> where TModel : class
    {
        public PeriPheryBaseRepository()
        {
            DBName = DBParam.DBName.PeriPhery;
            DBType = DBParam.DBType.SQLSERVER;
        }
    }
}
