using Russian.Post.Database.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Russian.Post.Database.Models
{
    public class PostClientMessage : IDeletedModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(256)]
        public string Message { get; set; }

        public int AttemptCount { get; set; }

        public bool IsDelivered { get; set; }

        public bool IsDeleted { get; set; }
    }
}
