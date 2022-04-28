using FindPet.Core;
using FindPet.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FindPet.WebApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository postRepo;
        private readonly IWebHostEnvironment environment;

        public PostsController(PostRepository postR, IWebHostEnvironment env)
        {
            postRepo = postR;
            environment = env;
        }

        public IActionResult Index()
        {
            var posts = postRepo.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([Bind("Status,Description,Location,PhotoFile,Username")] Post model)
        {
            if (ModelState.IsValid)
            {
                var post = await PostWithPhoto(model);
                postRepo.AddPost(post);
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpPost]
        public async Task<string> SavePhoto(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var splitName = uploadedFile.FileName.Split('.');
                var extention = "." + splitName.Last();

                var fileName = Guid.NewGuid() + extention;
                var physicalPath = environment.ContentRootPath + "/Photos/" + fileName.ToString();

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    uploadedFile.CopyTo(stream);
                }
                return fileName.ToString();
            }
            return "";
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postRepo.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public async Task<Post> PostWithPhoto(Post model)
        {
            var photoName = await SavePhoto(model.PhotoFile);
            var post = new Post
            {
                Status = model.Status,
                Description = model.Description,
                Location = model.Location,
                Username = model.Username,
                Photo = photoName
            };

            return post;
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(int id, [Bind("Status,Description,Location,PhotoFile,Username")] Post model)
        {
            if (ModelState.IsValid)
            {
                if (model.PhotoFile != null)
                {
                    DeletePhoto(postRepo.GetById(id).Photo);
                }
                var post = await PostWithPhoto(model);

                try
                {
                    postRepo.EditPost(id, post);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postRepo.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = postRepo.GetById(id);
            DeletePhoto(post.Photo);
            postRepo.DeletePost(post);
            return RedirectToAction("Index");
        }

        public void DeletePhoto(string photoName)
        {
            var imagePath = environment.ContentRootPath + "/Photos/" + photoName;

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}
