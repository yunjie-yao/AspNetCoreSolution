using System;
using YangXuAPI.Entities;

namespace YangXuAPI.Models
{
    public class EmployeeAddDto
    {
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthOfDate { get; set; }
    }
}
