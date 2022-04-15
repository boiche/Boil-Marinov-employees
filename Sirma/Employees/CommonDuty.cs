using System;

namespace Sirma.Employees
{
    public struct CommonDuty : IComparable<CommonDuty>
    {
        public static readonly CommonDuty MinValue = new((-1, -1), -1, int.MinValue);
        public static readonly CommonDuty MaxValue = new((-1, -1), -1, int.MaxValue);

        private (int, int) employee_ids;
        private readonly int daysInterval;
        private readonly int projectId;

        public int DaysInterval { get => daysInterval; }
        public int ProjectID { get => projectId; }
        public int EmployeeID_First { get => employee_ids.Item1; }
        public int EmployeeID_Second { get => employee_ids.Item2; }

        public CommonDuty((int, int) ids, int projectId, int daysInterval)
        {
            employee_ids = ids;
            this.daysInterval = daysInterval;
            this.projectId = projectId;
        }

        public override string ToString()
        {
            return $"{employee_ids.Item1}, {employee_ids.Item2}, {projectId}, {daysInterval}";
        }

        public int CompareTo(CommonDuty other)
        {
            if (other.daysInterval > daysInterval)
                return 1;
            else if (other.daysInterval < daysInterval)
                return -1;
            else
                return 0;
        }
    }
}
