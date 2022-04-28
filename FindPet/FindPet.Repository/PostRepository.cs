using FindPet.Core;
using FindPet.Core.Data;

namespace FindPet.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext context;

        public PostRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public void AddPost(Post post)
        {
            context.Add(post);
            context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            context.Remove(post);
            context.SaveChanges();
        }

        public void EditPost(int id, Post newPost)
        {
            var post = context.Posts.Find(id);
            if (post.Status != newPost.Status)
            {
                post.Status = newPost.Status;
            }
            if (post.Photo != newPost.Photo)
            {
                if (!string.IsNullOrEmpty(newPost.Photo))
                {
                    post.Photo = newPost.Photo;
                }
            }
            if (post.Description != newPost.Description)
            {
                post.Description = newPost.Description;
            }
            if (post.Location != newPost.Location)
            {
                post.Location = newPost.Location;
            }

            context.Posts.Update(post);
            context.SaveChanges();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return context.Posts.ToList();
        }

        public Post GetById(int id)
        {
            return context.Posts.Find(id);
        }
    }
}
