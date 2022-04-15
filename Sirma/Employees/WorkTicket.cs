using System;

namespace Sirma.Employees
{
    public class WorkTicket
    {
        public int EmployeeID { get; set; }
        public int ProjectID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
