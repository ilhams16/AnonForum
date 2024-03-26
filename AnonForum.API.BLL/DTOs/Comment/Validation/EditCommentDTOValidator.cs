using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AnonForum.API.BLL.DTOs.Comment.Validation
{
    public class EditCommentDTOValidator : AbstractValidator<EditCommentDTO>
    {
        public EditCommentDTOValidator() 
        {
            RuleFor(c => c.CommentText).NotEmpty().WithMessage("Comment is required");
        }
    }
}
