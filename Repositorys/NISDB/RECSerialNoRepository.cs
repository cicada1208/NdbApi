using Lib;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class RECSerialNoRepository : NISDBBaseRepository<RECSerialNo>
    {
        /// <summary>
        /// 建立流水號
        /// </summary>
        /// <param name="param"></param>
        /// <param name="numHead">流水號前綴</param>
        /// <param name="numLen">流水號前綴外的號碼長度</param>
        /// <returns>流水號</returns>
        public async Task<ApiResult<string>> CreateRECSerialNo(RECSerialNo param, string numHead, int numLen)
        {
            string sql = string.Empty;
            RECSerialNo currentSNo, newSNo;
            string currentNum;
            long newNumNo;
            string newNum;
            int rowsAffected;

        Redo:
            currentSNo = (await GetRECSerialNo(param, 2)).Data.FirstOrDefault();
            if (currentSNo != null)
            {
                currentNum = currentSNo.NUM;
                newNumNo = long.Parse(currentNum.SubStr(numHead.Length)) + 1;
                newNum = numHead + newNumNo.ToString().PadLeft(numLen, '0');

                sql = @"
                update ni_RECSerialNo set
                NUM = @newNum
                where SYSID = @SYSID
                and SDATE = @SDATE
                and NUM = @NUM";

                rowsAffected = DBUtil.Execute(sql, new
                {
                    currentSNo.SYSID,
                    currentSNo.SDATE,
                    currentSNo.NUM,
                    newNum
                });

                if (rowsAffected == 0)
                    goto Redo;
            }
            else
            {
                newNum = numHead + "1".PadLeft(numLen, '0');
                newSNo = new RECSerialNo();
                newSNo.SYSID = param.SYSID;
                newSNo.SDATE = param.SDATE;
                newSNo.NUM = newNum;
                rowsAffected = DBUtil.Insert<RECSerialNo>(newSNo);

                if (rowsAffected == 0)
                    goto Redo;
            }

            return new ApiResult<string>(rowsAffected, newNum);
        }

        /// <summary>
        /// 查詢流水號
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依鍵值SYSID、SDATE</para>
        /// </summary>
        public async Task<ApiResult<List<RECSerialNo>>> GetRECSerialNo(RECSerialNo param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select * from ni_RECSerialNo
                    where SYSID = @SYSID 
                    and SDATE = @SDATE";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<RECSerialNo>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<RECSerialNo>>(queryList);
        }

    }
}
