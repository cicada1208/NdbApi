using System.Collections.Generic;

namespace Models
{
    public class MedicalTeam
    {
        /// <summary>
        /// 科別
        /// </summary>
        public string PatDept { get; set; }

        /// <summary>
        /// 科別名稱
        /// </summary>
        public string PatDeptName { get; set; }

        /// <summary>
        /// 主治醫師員編
        /// </summary>
        public string VsNo { get; set; }

        /// <summary>
        /// 主治醫師姓名
        /// </summary>
        public string VsName { get; set; }

        /// <summary>
        /// 主治醫師 Mvpn
        /// </summary>
        public string Mvpn { get; set; }

        private List<MedicalTeamNPPGY> _NPPGY;
        /// <summary>
        /// 專師與住院醫師
        /// </summary>
        public List<MedicalTeamNPPGY> NPPGY
        {
            get => _NPPGY ??= new List<MedicalTeamNPPGY>();
            set => _NPPGY = value;
        }
    }
}
