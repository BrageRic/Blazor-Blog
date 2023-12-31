﻿namespace ServerBlazor.Models.Entities
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagText { get; set; }

        //Navigational Properties
        public List<Post> Posts { get; } = new();
    }

    public class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
