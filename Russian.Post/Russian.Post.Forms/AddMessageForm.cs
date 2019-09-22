using System.ComponentModel.DataAnnotations;

namespace Russian.Post.Forms
{
    public class AddMessageForm
    {
        [Required]
        public string Message { get; set; }
    }
}
