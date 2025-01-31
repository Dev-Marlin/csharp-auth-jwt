using System.Linq.Expressions;
using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModels;

namespace exercise.wwwapi.Repositories
{
    public interface IRepository
    {
        public Task<IEnumerable<BlogPost>> GetAllBlogPosts();
        public Task<BlogPost> PostBlog(PostBlogPost pbp);
        public Task<BlogPost> EditBlogPost(PutBlogPost pbp, int id);



        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetById(int id);
        public void Insert(User user);
        public void Update(PutUser putUser, int id);
        public void Delete(int id);
        public void Save();
    }
}
