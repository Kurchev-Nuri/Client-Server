namespace Russian.Post.Database.Models.Common
{
    public interface IDeletedModel
    {
        bool IsDeleted { get; set; }
    }
}
