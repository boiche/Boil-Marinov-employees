using System;
using System.Collections.Generic;
using System.Linq;

namespace Sirma.Employees
{
    public class WorkAnalyser
    {
        private readonly List<Portfolio> _portfolios;
        public WorkAnalyser(IEnumerable<string> data)
        {
            List<WorkTicket> tickets = new();
            foreach (var inputRow in data)
            {
                string[] rowData = inputRow.Split(',');
                DateTime.TryParse(rowData[2], out DateTime fromDate);
                DateTime.TryParse(rowData[3], out DateTime toDate);
                if (toDate == DateTime.MinValue)
                    toDate = DateTime.Now;

                var ticketData = new WorkTicket()
                {
                    EmployeeID = int.Parse(rowData[0]),
                    ProjectID = int.Parse(rowData[1]),
                    FromDate = fromDate,
                    ToDate = toDate
                };
                tickets.Add(ticketData);
            }
            _portfolios = tickets.GroupBy(x => new { x.EmployeeID, x.ProjectID }, (key, tickets) => new Portfolio { EmployeeID = key.EmployeeID, WorkHistory = tickets.ToList() }).ToList();
        }
        public CommonDuty GetMaxCommonDuty()
        {
            CommonDuty maxCommonDuty = CommonDuty.MinValue;
            for (int i = 0; i < _portfolios.Count - 1; i++)
            {
                for (int j = i + 1; j < _portfolios.Count; j++)
                {
                    if (_portfolios[i].EmployeeID == _portfolios[j].EmployeeID)
                        continue;
                    CommonDuty currentCommonDuty = CalculateMaxCommonDutyFor(_portfolios[i], _portfolios[j]);
                    if (currentCommonDuty.CompareTo(maxCommonDuty) <= 0)
                        maxCommonDuty = currentCommonDuty;
                }
            }
            return maxCommonDuty;
        }
        public List<CommonDuty> GetCommonDuties()
        {
            var result = new List<CommonDuty>();
            for (int i = 0; i < _portfolios.Count - 1; i++)
            {
                for (int j = i + 1; j < _portfolios.Count; j++)
                {
                    if (_portfolios[i].EmployeeID == _portfolios[j].EmployeeID)
                        continue;
                    result.AddRange(CalculateCommonDutiesFor(_portfolios[i], _portfolios[j]));
                }
            }
            return result.OrderByDescending(x => x.DaysInterval).ToList();
        }
        private List<CommonDuty> CalculateCommonDutiesFor(Portfolio first, Portfolio second)
        {
            var result = new List<CommonDuty>();
            foreach (var firstWorkHistory in first.WorkHistory)
            {
                foreach (var secondWorkHistory in second.WorkHistory)
                {
                    if (firstWorkHistory.ToDate < secondWorkHistory.FromDate ||
                        secondWorkHistory.ToDate < firstWorkHistory.FromDate ||
                        firstWorkHistory.ProjectID != secondWorkHistory.ProjectID)
                        continue;
                    else //has intersection
                    {
                        int days;
                        if (firstWorkHistory.FromDate < secondWorkHistory.FromDate) //check who has started first
                            days = (firstWorkHistory.ToDate - secondWorkHistory.FromDate).Days;
                        else
                            days = (secondWorkHistory.ToDate - firstWorkHistory.FromDate).Days;
                        result.Add(new CommonDuty((first.EmployeeID, second.EmployeeID), firstWorkHistory.ProjectID, days));
                    }
                }
            }
            return result.OrderByDescending(x => x.DaysInterval).ToList();
        }
        private CommonDuty CalculateMaxCommonDutyFor(Portfolio first, Portfolio second)
        {
            return CalculateCommonDutiesFor(first, second).FirstOrDefault();
        }
    }
}
