using System;

namespace Russian.Post.Models
{
    public class ServerMessage
    {
        public string Message { get; set; }

        public string IpAddress { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
