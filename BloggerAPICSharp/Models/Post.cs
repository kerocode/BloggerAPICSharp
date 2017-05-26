using System;
namespace BloggerAPICSharp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
