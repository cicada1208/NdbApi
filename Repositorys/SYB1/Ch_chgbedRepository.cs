using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Ch_chgbedRepository : SYB1BaseRepository<Ch_chgbed>
    {
        /// <summary>
        /// 查詢護理站轉床記錄檔
        /// <para>1: 依參數自動組建</para>
        /// </summary>
        public async Task<ApiResult<List<Ch_chgbed>>> GetCh_chgbed(Ch_chgbed param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
            }

            var query = (await DBUtil.QueryIntgrAsync<Ch_chgbed>(sql, param, schemaOnly: option != 1)).ToList();
            return new ApiResult<List<Ch_chgbed>>(query);
        }

        /// <summary>
        /// 查詢院內轉床來源的病人需求床位等級 (來源：其他單位)
        /// </summary>
        public async Task<ApiResult<List<Ch_chgbed>>> GetBedClassFromInternal(Ch_chgbed param)
        {
            string sql = @"
            select top 1 substring(chgbed_filler,6,2) as chgbed_bed_cls, *
            from ch_chgbed
            where chgbed_pat_no = @chgbed_pat_no --來源病歷號
            and chgbed_exe_status = 'G'
            and chgbed_bed_room = @chgbed_bed_room --預約床位(前四碼)
            and chgbed_bed_no = @chgbed_bed_no --預約床位(後兩碼)";

            var query = (await DBUtil.QueryAsync<Ch_chgbed>(sql, param)).ToList();
            return new ApiResult<List<Ch_chgbed>>(query);
        }

        public async Task<string> SetMessage(Ch_chgbed param)
        {
            string result = string.Empty;
            if (param.chgbed_pat_no.HasValue && !param.chgbed_bed_room.IsNullOrWhiteSpace())
            {
                string[] msgs;
                Ch_chgbed ch_chgbed = (await GetCh_chgbed(param, 1)).Data.FirstOrDefault();
                if (ch_chgbed != null)
                {
                    msgs = new[]{ ch_chgbed.chgbed_message_1, ch_chgbed.chgbed_message_2,
                    ch_chgbed.chgbed_message_3, ch_chgbed.chgbed_message_4,
                    ch_chgbed.chgbed_message_5,ch_chgbed.chgbed_message_6,
                    ch_chgbed.chgbed_message_7, ch_chgbed.chgbed_message_8};
                    result = string.Join(Environment.NewLine, msgs);
                }
            }
            return result;
        }

    }
}
