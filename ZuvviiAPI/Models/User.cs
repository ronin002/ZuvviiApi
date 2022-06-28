using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZuvviiAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public List<Video> Videos { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
