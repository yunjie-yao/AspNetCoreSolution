using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YangXuAPI.Entities;
using YangXuAPI.ValidationAttributes;

namespace YangXuAPI.Models
{
    //验证优先级：属性级别，类级别（DataAnnotations，IValidatableObject，ValidationAttributes）
    [EmployeeNoMustDifferentFromFirstName(ErrorMessage = "员工编号不可以和名一样！！")]
    public abstract class EmployeeAbstractDto:IValidatableObject
    {
        //将属性设置为abstract,可以在add和update里分别定义各自的规则，以实现差异化处理
        public abstract string EmployeeNo { get; set; }

        [Display(Name = "名")]
        [Required(ErrorMessage = "{0}字段必填"), MaxLength(10, ErrorMessage = "{0}最大长度{1}")]
        public string FirstName { get; set; }

        [Display(Name = "姓"), Required(ErrorMessage = "{0}字段必填"), MaxLength(10, ErrorMessage = "{0}最大长度{1}")]
        public string LastName { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [Display(Name = "出生日期")]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //主要用于跨属性级别的验证，或者整个类级别的验证
            //在此可以自定义一些复杂的验证逻辑
            //优先级：DataAnnotations，Validate
            if (FirstName == LastName)
            {
                //yield return new ValidationResult("姓和名不能一样",new []{nameof(EmployeeAddDto) });//类级别错误
                yield return new ValidationResult("姓和名不能一样", new[] { nameof(FirstName), nameof(LastName) });//属性级别错误
            }
        }

        // TODO：枚举和DateTime怎么实现输入验证
        // Gender只能1和2
        // DateOfBirth不能为空
    }
}
