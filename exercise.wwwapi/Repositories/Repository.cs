using System.Linq.Expressions;
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories
{
    public class Repository : IRepository
    {
        private DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        // Blogposts
        public async Task<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            return await _db.Blogs.ToListAsync();
        }
        public async Task<BlogPost> PostBlog(PostBlogPost postBlogPost)
        {
            BlogPost bp = new BlogPost()
            {
                Id = postBlogPost.Id,
                Text = postBlogPost.Text,
                UserId = postBlogPost.UserId
            };

            await _db.Blogs.AddAsync(bp);
            await _db.SaveChangesAsync();

            return bp;
        }
        public async Task<BlogPost> EditBlogPost(PutBlogPost pbp, int id)
        {
            BlogPost blogPost = await _db.Blogs.FirstAsync(x => x.Id == id);
            var blogPostToUpdate = _db.Blogs.Update(blogPost).Entity;

            if(pbp.Text != null)
            {
                blogPostToUpdate.Text = pbp.Text;
            }

            if (pbp.UserId != null)
            {
                blogPostToUpdate.UserId = (int)pbp.UserId;
            }

            await _db.SaveChangesAsync();

            return blogPostToUpdate;
        }
        public async void Delete(int id)
        {
            BlogPost blogPostToDelete = await _db.Blogs.FirstAsync(x => x.Id == id);
            _db.Remove(blogPostToDelete);
            await _db.SaveChangesAsync() ;
        }


        // Users
        public async Task<IEnumerable<User>> GetAll(params Expression<Func<User, object>>[] includeExpressions)
        {
            if (includeExpressions.Any())
            {
                var set = includeExpressions
                    .Aggregate<Expression<Func<User, object>>, IQueryable<User>>
                     (_db.Users, (current, expression) => current.Include(expression));
            }
            return await _db.Users.ToListAsync();
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _db.Users.ToListAsync();
        }
        public async Task<User> GetById(int id)
        {
            return await _db.Users.FirstAsync(x => x.Id == id);
        }
        public async void Insert(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }
        public async void Save()
        {
            await _db.SaveChangesAsync();
        }
        public async void Update(PutUser putUser, int id)
        {
            User user = await _db.Users.FirstAsync(x => x.Id == id);
            var userToUpdate = _db.Users.Update(user).Entity;

            if(putUser.Username !=  null)
            {
                userToUpdate.Username = putUser.Username;
            }

            if (putUser.PasswordHash != null)
            {
                userToUpdate.PasswordHash = putUser.PasswordHash;
            }

            if (putUser.Email != null)
            {
                userToUpdate.Email = putUser.Email;
            }

            await _db.SaveChangesAsync();
        }
    }
}
