using System;

namespace YangXuAPI.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Company Company { get; set; }

    }
}