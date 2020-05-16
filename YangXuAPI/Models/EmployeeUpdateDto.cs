using System;
using System.ComponentModel.DataAnnotations;
using YangXuAPI.Entities;

namespace YangXuAPI.Models
{
    public class EmployeeUpdateDto:EmployeeAbstractDto
    {
        [Display(Name = "员工编号")]
        [Required(ErrorMessage = "{0}字段必填")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0}字段长度必须为{1}")]
        public override string EmployeeNo { get; set; }
    }
}
