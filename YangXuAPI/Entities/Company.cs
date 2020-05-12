using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangXuAPI.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
