using System.Collections.Generic;

namespace YangXuAPI.Models
{
    public class CompanyAddDto
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        public ICollection<EmployeeAddDto> Employees { get; set; }=new List<EmployeeAddDto>();
    }
}
