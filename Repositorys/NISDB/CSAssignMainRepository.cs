using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoreLinq.Extensions.DistinctByExtension;

namespace Repositorys.NISDB
{
    public class CSAssignMainRepository : NISDBBaseRepository<CSAssignMain>
    {
        /// <summary>
        /// 查詢護理組別
        /// </summary>
        public async Task<ApiResult<List<CSAssignGroup>>> GetCSAssignGroup(string clinicalUnitId, string assignDate)
        {
            string sql = @"
            select am.csassignMainId, am.timeInterval,
            am.loginId, usr.userName, termlib.label as proTitleLevelLabel,
            am.itemId,
            (
                select item.itemName
                from ni_CSItem as item
                where item.itemId = am.itemId
                and item.delDate is null
            ) AS itemName, --班別
            am.csteamId,
            (
                select team.teamName
                from ni_CSTeam as team
                where team.csteamId = am.csteamId
                and team.delDate is null 
            ) as teamName --組別
            from ni_CSAssignMain as am
            left join ni_Users as usr
            on (usr.loginId = am.loginId and usr.isActive = 1)
            left join ni_TermLib as termlib
            on (termlib.termLibId = usr.proTitleLevelId)
            where am.clinicalUnitId = @clinicalUnitId
            and am.assignDate = @assignDate
            and am.delDate is null
            and am.timeInterval not in ('O')";

            var assignGroupList = (await DBUtil.QueryAsync<CSAssignGroup>(sql, new { clinicalUnitId, assignDate })).ToList();

            var assignDetailList = (await DB.CSAssignDetailRepository.GetUnitCSAssignDetail(clinicalUnitId, assignDate)).Data;

            var checkList = (await DB.CSCheckListRepository.Get(new CSCheckList { clinicalUnitId = clinicalUnitId })).Data;

            assignGroupList.ForEach(g =>
            {
                g.beds = string.Join(',', assignDetailList
                    .Where(d => d.csassignMainId == g.csassignMainId && d.assignType == "Bed")
                    .DistinctBy(d => d.typeId)
                    .Select(d => d.typeId)
                    .OrderBy(typeId=> typeId));

                g.checkItems = string.Join(',', assignDetailList
                    .Where(d => d.csassignMainId == g.csassignMainId && d.assignType == "CheckList")
                    .Join(checkList,
                    ad => ad.typeId,
                    chk => chk.cscheckListId,
                    (ad, chk) => chk.itemName)
                    .OrderBy(itemName => itemName));
            });

            List<CSAssignGroup> result = new();
            foreach (var assignGroup in assignGroupList.DistinctBy(g => g.loginId))
            {
                var targetAssignGroup = assignGroupList.FirstOrDefault(g => g.loginId == assignGroup.loginId && !g.beds.IsNullOrWhiteSpace());
                if (targetAssignGroup == null)
                    targetAssignGroup = assignGroupList.FirstOrDefault(g => g.loginId == assignGroup.loginId && !g.checkItems.IsNullOrWhiteSpace());
                if (targetAssignGroup == null)
                    targetAssignGroup = assignGroupList.FirstOrDefault(g => g.loginId == assignGroup.loginId);
                result.Add(targetAssignGroup);
            }

            return new ApiResult<List<CSAssignGroup>>(result);
        }

    }
}
