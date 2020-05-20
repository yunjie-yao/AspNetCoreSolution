using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace YangXuAPI.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Product { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
