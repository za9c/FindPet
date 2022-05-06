using FindPet.Core;
using FindPet.Core.Data;

namespace FindPet.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public void AddComment(Comment comment)
        {
            context.Add(comment);
            context.SaveChanges();
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return context.Comments.ToList();
        }

        public void DeleteComment(Comment comment)
        {
            context.Remove(comment);
            context.SaveChanges();
        }
    }
}
