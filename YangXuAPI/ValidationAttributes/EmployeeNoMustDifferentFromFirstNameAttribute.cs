using System.ComponentModel.DataAnnotations;
using YangXuAPI.Models;

namespace YangXuAPI.ValidationAttributes
{
    public class EmployeeNoMustDifferentFromFirstNameAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var employeeAddDto = (EmployeeAbstractDto) validationContext.ObjectInstance;//得到类的对象
            if (employeeAddDto.EmployeeNo==employeeAddDto.FirstName)
            {
                //return new ValidationResult("员工编号不可以等于名"
                //    ,new []{nameof(employeeAddDto)});
                return new ValidationResult(ErrorMessage
                    , new[] { nameof(EmployeeAbstractDto) });
            }

            return ValidationResult.Success;
        }
    }
}
