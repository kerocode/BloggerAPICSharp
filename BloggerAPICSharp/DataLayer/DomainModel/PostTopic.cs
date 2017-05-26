using System;
namespace BloggerAPICSharp.DataLayer.DomainModel
{
    public class PostTopic
    {
        public int PostId
        {
            get;
            set;
        }

        public Post Post { get; set; }
        public int TopicId
        {
            get;
            set;
        }
        public Topic Topic { get; set; }
    }
}
