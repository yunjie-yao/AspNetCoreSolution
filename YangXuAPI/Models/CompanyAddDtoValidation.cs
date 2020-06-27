using FluentValidation;

namespace YangXuAPI.Models
{
    public class CompanyAddDtoValidation : AbstractValidator<CompanyAddDto>
    {
        public CompanyAddDtoValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(4, 8)
                .WithMessage("公司名称不能为空，且长度为4-8位");
        }
    }
}
