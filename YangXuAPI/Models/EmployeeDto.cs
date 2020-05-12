using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangXuAPI.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public string GenderDisplay { get; set; }
        public int Age { get; set; }
    }
}
