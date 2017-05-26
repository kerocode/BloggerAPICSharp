using System;
using System.Collections.Generic;

namespace BloggerAPICSharp.DataLayer.DomainModel
{
    public class Topic
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<PostTopic> PostTopics { get; set; }
	}
}
