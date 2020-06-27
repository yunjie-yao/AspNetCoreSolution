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
            RuleFor(x => x.Introduction)
                .Must(IntroductionMustBeThis)
                .WithMessage("公司介绍必须以'Introduction:'开头");
        }

        private bool IntroductionMustBeThis(CompanyAddDto companyAddDto,string startWith="Introduction:")
        {
            return !companyAddDto.Introduction.StartsWith(startWith);
        }
    }
}

// FluentValidation
// 如果要添加自定义的验证，如：判断公司介绍是否符合规范，这里就先定义这个校验方法，
// 如：IntroductionMustBeThis 这个方法我们自定义的，然后再RuleFor().Must(IntroductionMustBeThis) 即可应用
// 自定义的校验方法必须是（model,string）这种形式
