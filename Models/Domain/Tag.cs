﻿namespace Blog_Website.Models.Domain
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
