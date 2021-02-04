using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace HoukaifaBlog.Infrastructure.Resources
{
    public class PostResourceValidator : AbstractValidator<PostResource>
    {
        public PostResourceValidator()
        {
            RuleFor(x => x.Author)
                .NotNull()
                .WithName("作者")
                .WithMessage("{PropertyName}是必须的")
                .MaximumLength(50)
                .WithMessage("{PropertyName}的最大长度是{MaxLength}");
        }
    }
}
