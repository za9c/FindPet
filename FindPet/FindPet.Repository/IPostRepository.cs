using FindPet.Core;

namespace FindPet.Repository
{
    public interface IPostRepository
    {
        public void AddPost(Post post);
        public void EditPost(int id, Post newPost);
        public void DeletePost(Post post);
        public Post GetById(int id);
        public IEnumerable<Post> GetAllPosts();
    }
}
