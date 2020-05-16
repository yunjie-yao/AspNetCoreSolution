using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YangXuAPI.Models
{
    public class CompanyAddDto
    {
        [Display(Name = "公司名称")]
        [Required(ErrorMessage = "{0}字段必填")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "公司简介")]
        [StringLength(500,MinimumLength = 10,ErrorMessage = "{0}的长度范围是{2}到{1}")]
        public string Introduction { get; set; }
        public ICollection<EmployeeAddDto> Employees { get; set; }=new List<EmployeeAddDto>();
    }
}
