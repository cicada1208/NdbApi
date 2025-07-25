using Lib;
using Params;
using Repositorys.Inf;
using Repositorys.NIS;
using Repositorys.NISDB;
using Repositorys.PeriPhery;
using Repositorys.SYB1;
using Repositorys.SYB2;

namespace Repositorys
{
    public class DBContext
    {
        private DBUtil _NIS;
        public DBUtil NIS =>
            _NIS ??= new DBUtil(DBParam.DBName.NIS, DBParam.DBType.SYBASE);

        private DBUtil _NISDB;
        public DBUtil NISDB =>
            _NISDB ??= new DBUtil(DBParam.DBName.NISDB, DBParam.DBType.SQLSERVER);

        private DBUtil _Syb1;
        public DBUtil Syb1 =>
            _Syb1 ??= new DBUtil(DBParam.DBName.SYB1, DBParam.DBType.SYBASE);

        private DBUtil _Syb2;
        public DBUtil Syb2 =>
            _Syb2 ??= new DBUtil(DBParam.DBName.SYB2, DBParam.DBType.SYBASE);

        private DBUtil _PeriPhery;
        public DBUtil PeriPhery =>
            _PeriPhery ??= new DBUtil(DBParam.DBName.PeriPhery, DBParam.DBType.SQLSERVER);

        private DBUtil _Inf;
        public DBUtil Inf =>
            _Inf ??= new DBUtil(DBParam.DBName.Inf, DBParam.DBType.SQLSERVER);

        private SchemaRepository _SchemaRepository;
        public SchemaRepository SchemaRepository =>
            _SchemaRepository ??= new SchemaRepository();

        private DBRepository _DBRepository;
        public DBRepository DBRepository =>
            _DBRepository ??= new DBRepository();

        private RECSerialNoRepository _RECSerialNoRepository;
        public RECSerialNoRepository RECSerialNoRepository =>
            _RECSerialNoRepository ??= new RECSerialNoRepository();

        private UsersRepository _UsersRepository;
        public UsersRepository UsersRepository =>
            _UsersRepository ??= new UsersRepository();

        private SysParameterRepository _SysParameterRepository;
        public SysParameterRepository SysParameterRepository =>
            _SysParameterRepository ??= new SysParameterRepository();

        private Mg_mnidRepository _Mg_mnidRepository;
        public Mg_mnidRepository Mg_mnidRepository =>
            _Mg_mnidRepository ??= new Mg_mnidRepository();

        private LoginRepository _LoginRepository;
        public LoginRepository LoginRepository =>
            _LoginRepository ??= new LoginRepository();

        private NisPatInfoRepository _NisPatInfoRepository;
        public NisPatInfoRepository NisPatInfoRepository =>
            _NisPatInfoRepository ??= new NisPatInfoRepository();

        private Ch_dnrRepository _Ch_dnrRepository;
        public Ch_dnrRepository Ch_dnrRepository =>
            _Ch_dnrRepository ??= new Ch_dnrRepository();

        private Ch_hivcaRepository _Ch_hivcaRepository;
        public Ch_hivcaRepository Ch_hivcaRepository =>
            _Ch_hivcaRepository ??= new Ch_hivcaRepository();

        private Mc_mic2Repository _Mc_mic2Repository;
        public Mc_mic2Repository Mc_mic2Repository =>
            _Mc_mic2Repository ??= new Mc_mic2Repository();

        private Ch_rel2Repository _Ch_rel2Repository;
        public Ch_rel2Repository Ch_rel2Repository =>
            _Ch_rel2Repository ??= new Ch_rel2Repository();

        private AllergyRepository _AllergyRepository;
        public AllergyRepository AllergyRepository =>
            _AllergyRepository ??= new AllergyRepository();

        private ASE_BaseInfoRepository _ASE_BaseInfoRepository;
        public ASE_BaseInfoRepository ASE_BaseInfoRepository =>
            _ASE_BaseInfoRepository ??= new ASE_BaseInfoRepository();

        private FallASE_AdultRepository _FallASE_AdultRepository;
        public FallASE_AdultRepository FallASE_AdultRepository =>
            _FallASE_AdultRepository ??= new FallASE_AdultRepository();

        private BedSoresASERepository _BedSoresASERepository;
        public BedSoresASERepository BedSoresASERepository =>
            _BedSoresASERepository ??= new BedSoresASERepository();

        private FocusRecordRepository _FocusRecordRepository;
        public FocusRecordRepository FocusRecordRepository =>
            _FocusRecordRepository ??= new FocusRecordRepository();

        private Ch_torRepository _Ch_torRepository;
        public Ch_torRepository Ch_torRepository =>
            _Ch_torRepository ??= new Ch_torRepository();

        private DHRLRepository _DHRLRepository;
        public DHRLRepository DHRLRepository =>
            _DHRLRepository ??= new DHRLRepository();

        private ESSRepository _ESSRepository;
        public ESSRepository ESSRepository =>
            _ESSRepository ??= new ESSRepository();

        private Ch_cserRepository _Ch_cserRepository;
        public Ch_cserRepository Ch_cserRepository =>
            _Ch_cserRepository ??= new Ch_cserRepository();

        private Mr_tsmitRepository _Mr_tsmitRepository;
        public Mr_tsmitRepository Mr_tsmitRepository =>
            _Mr_tsmitRepository ??= new Mr_tsmitRepository();

        private RRSRepository _RRSRepository;
        public RRSRepository RRSRepository =>
            _RRSRepository ??= new RRSRepository();

        private TubeMagRepository _TubeMagRepository;
        public TubeMagRepository TubeMagRepository =>
            _TubeMagRepository ??= new TubeMagRepository();

        private VitalSignRepository _VitalSignRepository;
        public VitalSignRepository VitalSignRepository =>
            _VitalSignRepository ??= new VitalSignRepository();

        private RestraintMagRepository _RestraintMagRepository;
        public RestraintMagRepository RestraintMagRepository =>
            _RestraintMagRepository ??= new RestraintMagRepository();

        private Mh_mpatRepository _Mh_mpatRepository;
        public Mh_mpatRepository Mh_mpatRepository =>
            _Mh_mpatRepository ??= new Mh_mpatRepository();

        private Mi_mbedRepository _Mi_mbedRepository;
        public Mi_mbedRepository Mi_mbedRepository =>
            _Mi_mbedRepository ??= new Mi_mbedRepository();

        private PtEncHisRepository _PtEncHisRepository;
        public PtEncHisRepository PtEncHisRepository =>
            _PtEncHisRepository ??= new PtEncHisRepository();

        private RecShortCodeRepository _RecShortCodeRepository;
        public RecShortCodeRepository RecShortCodeRepository =>
            _RecShortCodeRepository ??= new RecShortCodeRepository();

        private ClinicalUnitRepository _ClinicalUnitRepository;
        public ClinicalUnitRepository ClinicalUnitRepository =>
            _ClinicalUnitRepository ??= new ClinicalUnitRepository();

        private BedRepository _BedRepository;
        public BedRepository BedRepository =>
            _BedRepository ??= new BedRepository();

        private VSBTRepository _VSBTRepository;
        public VSBTRepository VSBTRepository =>
            _VSBTRepository ??= new VSBTRepository();

        private VSBPRepository _VSBPRepository;
        public VSBPRepository VSBPRepository =>
            _VSBPRepository ??= new VSBPRepository();

        private VSPainRepository _VSPainRepository;
        public VSPainRepository VSPainRepository =>
            _VSPainRepository ??= new VSPainRepository();

        private VSMEDRepository _VSMEDRepository;
        public VSMEDRepository VSMEDRepository =>
            _VSMEDRepository ??= new VSMEDRepository();

        private VSPulseRepository _VSPulseRepository;
        public VSPulseRepository VSPulseRepository =>
            _VSPulseRepository ??= new VSPulseRepository();

        private KDSpRepository _KDSpRepository;
        public KDSpRepository KDSpRepository =>
            _KDSpRepository ??= new KDSpRepository();

        private Ch_erresRepository _Ch_erresRepository;
        public Ch_erresRepository Ch_erresRepository =>
            _Ch_erresRepository ??= new Ch_erresRepository();

        private Ch_chgbedRepository _Ch_chgbedRepository;
        public Ch_chgbedRepository Ch_chgbedRepository =>
            _Ch_chgbedRepository ??= new Ch_chgbedRepository();

        private Ch_resRepository _Ch_resRepository;
        public Ch_resRepository Ch_resRepository =>
            _Ch_resRepository ??= new Ch_resRepository();

        private Mi_clrbedRepository _Mi_clrbedRepository;
        public Mi_clrbedRepository Mi_clrbedRepository =>
            _Mi_clrbedRepository ??= new Mi_clrbedRepository();

        private CSAssignMainRepository _CSAssignMainRepository;
        public CSAssignMainRepository CSAssignMainRepository =>
            _CSAssignMainRepository ??= new CSAssignMainRepository();

        private CSAssignDetailRepository _CSAssignDetailRepository;
        public CSAssignDetailRepository CSAssignDetailRepository =>
            _CSAssignDetailRepository ??= new CSAssignDetailRepository();

        private CSCheckListRepository _CSCheckListRepository;
        public CSCheckListRepository CSCheckListRepository =>
            _CSCheckListRepository ??= new CSCheckListRepository();

        private PtEncounterRepository _PtEncounterRepository;
        public PtEncounterRepository PtEncounterRepository =>
            _PtEncounterRepository ??= new PtEncounterRepository();

        private Mch_mgopRepository _Mch_mgopRepository;
        public Mch_mgopRepository Mch_mgopRepository =>
            _Mch_mgopRepository ??= new Mch_mgopRepository();

        private MedicalTeamRepository _MedicalTeamRepository;
        public MedicalTeamRepository MedicalTeamRepository =>
            _MedicalTeamRepository ??= new MedicalTeamRepository();

        private Ch_dhidRepository _Ch_dhidRepository;
        public Ch_dhidRepository Ch_dhidRepository =>
            _Ch_dhidRepository ??= new Ch_dhidRepository();

        private Ch_tabRepository _Ch_tabRepository;
        public Ch_tabRepository Ch_tabRepository =>
            _Ch_tabRepository ??= new Ch_tabRepository();

        private TeamContactRepository _TeamContactRepository;
        public TeamContactRepository TeamContactRepository =>
            _TeamContactRepository ??= new TeamContactRepository();

        private TeamNoteRepository _TeamNoteRepository;
        public TeamNoteRepository TeamNoteRepository =>
            _TeamNoteRepository ??= new TeamNoteRepository();

        private KDNrNewRepository _KDNrNewRepository;
        public KDNrNewRepository KDNrNewRepository =>
            _KDNrNewRepository ??= new KDNrNewRepository();

        private OperationRepository _OperationRepository;
        public OperationRepository OperationRepository =>
            _OperationRepository ??= new OperationRepository();

        private HisPatientRepository _HisPatientRepository;
        public HisPatientRepository HisPatientRepository =>
            _HisPatientRepository ??= new HisPatientRepository();

        private OPNSChkRecRepository _OPNSChkRecRepository;
        public OPNSChkRecRepository OPNSChkRecRepository =>
            _OPNSChkRecRepository ??= new OPNSChkRecRepository();

        private APACHE1Repository _APACHE1Repository;
        public APACHE1Repository APACHE1Repository =>
            _APACHE1Repository ??= new APACHE1Repository();

        private Gas102Repository _Gas102Repository;
        public Gas102Repository Gas102Repository =>
            _Gas102Repository ??= new Gas102Repository();

        private Gas002Repository _Gas002Repository;
        public Gas002Repository Gas002Repository =>
            _Gas002Repository ??= new Gas002Repository();

        private IOByDayRepository _IOByDayRepository;
        public IOByDayRepository IOByDayRepository =>
            _IOByDayRepository ??= new IOByDayRepository();
    }
}
