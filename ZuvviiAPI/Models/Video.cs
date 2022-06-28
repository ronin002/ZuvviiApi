using System;
using System.Collections.Generic;

namespace ZuvviiAPI.Models
{
    public class Video
    {
        public Video()
        {
            Comments = new List<Comment>();
        }
        public Guid Id { get; set; }
        public User User { get; set; }
        //public Game Game { get; set; }
        public string? Url { get; set; }
        public string Title { get; set; }
        public string? Thumb { get; set; }
        public string? Tags { get; set; }
        public DateTime CreateAt { get; set; }
        public int Like { get; set; }
        public int Views { get; set; }
        public List<Comment> Comments { get; set; }


    }
}
