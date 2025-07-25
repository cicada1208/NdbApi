namespace Models
{
    public class Ch_tab_dhid : Ch_tab
    {
        /// <summary>
        /// 醫師身份代碼
        /// 總值:01
        /// 主治醫師:02
        /// 住院醫師:03
        /// 專科護理師:04
        /// </summary>
        public string dhid_dr_type { get; set; }

        /// <summary>
        /// 醫師身份
        /// </summary>
        public string dhid_dr_type_name =>
            dhid_dr_type switch
            {
                "01" => "總值",
                "02" => "主治醫師",
                "03" => "住院醫師",
                "04" => "專師",
                _ => dhid_dr_type
            };

    }
}
