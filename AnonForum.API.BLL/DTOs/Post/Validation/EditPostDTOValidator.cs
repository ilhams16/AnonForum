using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AnonForum.API.BLL.DTOs.Post.Validation
{
    public class EditPostDTOValidator : AbstractValidator<EditPostDTO>
    {
        public EditPostDTOValidator() 
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Maximum character is 50");
            RuleFor(x => x.PostText).NotEmpty().WithMessage("Post is required");
            RuleFor(x => x.PostCategoryID).NotEmpty().WithMessage("Post category is required");
            RuleFor(x => x.PostCategoryID).GreaterThan(0).WithMessage("Post category not available");
        }
    }
}
