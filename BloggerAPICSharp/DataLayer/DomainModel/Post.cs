using System;
using System.Collections.Generic;

namespace BloggerAPICSharp.DataLayer.DomainModel
{
    public class Post
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int ApplictionUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<PostTopic> PostTopics { get; set; }
    }
}
