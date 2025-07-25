namespace Lib
{
    public class UtilLocator
    {
        private ApiUtil _Api;
        public ApiUtil Api =>
            _Api ??= new ApiUtil();

        private DataTableUtil _DataTable;
        public DataTableUtil DataTable =>
            _DataTable ??= new DataTableUtil();

        private DateTimeUtil _DateTime;
        public DateTimeUtil DateTime =>
            _DateTime ??= new DateTimeUtil();

        private HostUtil _Host;
        public HostUtil Host =>
            _Host ??= new HostUtil();

        private ModelUtil _Model;
        public ModelUtil Model =>
            _Model ??= new ModelUtil();

        private MedicalUtil _Medical;
        public MedicalUtil Medical =>
            _Medical ??= new MedicalUtil();

        private RuleUtil _Rule;
        public RuleUtil Rule =>
            _Rule ??= new RuleUtil();

        private SqlBuildUtil _SqlBuild;
        public SqlBuildUtil SqlBuild =>
            _SqlBuild ??= new SqlBuildUtil();

        private StrUtil _Str;
        public StrUtil Str =>
            _Str ??= new StrUtil();

    }
}
