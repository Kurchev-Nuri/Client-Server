namespace Russian.Post.Common.Results
{
    public class PostError
    {
        string _message;

        public PostErrorCodes Code { get; set; }

        public string Message
        {
            set => _message = value;
            get => string.IsNullOrWhiteSpace(_message) && Code != PostErrorCodes.Ok ? Code.ToString() : _message;
        }
    }
}
