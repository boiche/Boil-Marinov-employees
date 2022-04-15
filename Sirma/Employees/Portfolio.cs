using System.Collections.Generic;

namespace Sirma.Employees
{
    public class Portfolio
    {
        public int EmployeeID { get; set; }
        public ICollection<WorkTicket> WorkHistory { get; set; }
    }
}
