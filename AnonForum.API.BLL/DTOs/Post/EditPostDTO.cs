using System;
using System.Collections.Generic;
using System.Text;

namespace AnonForum.API.BLL.DTOs.Post
{
    public class EditPostDTO
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string PostText { get; set; }
        public int PostCategoryID { get; set; }
    }
}
