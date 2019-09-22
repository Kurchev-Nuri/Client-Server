namespace Russian.Post.Models
{
    public class ClientMessage
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int AttemptCount { get; set; }

        public bool IsDelivered { get; set; }
    }
}
