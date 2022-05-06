using FindPet.Core;

namespace FindPet.Repository
{
    public interface ICommentRepository
    {
        public void AddComment(Comment comment);
        public IEnumerable<Comment> GetAllComments();
        public void DeleteComment(Comment comment);
    }
}
