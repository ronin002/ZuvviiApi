using System;

namespace ZuvviiAPI.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Commentary { get; set; }
        public DateTime CreateAt { get; set; }
        public User Author { get; set; }
        public Video Video { get; set; }

    }
}
