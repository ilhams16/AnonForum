using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AnonForum.API.BLL.DTOs.Comment.Validation
{
    public class CreateCommentDTOValidator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentDTOValidator() 
        {
            RuleFor(c => c.Comment).NotEmpty().WithMessage("Comment is required");
        }
    }
}
