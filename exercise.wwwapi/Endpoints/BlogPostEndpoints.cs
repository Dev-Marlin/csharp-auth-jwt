using exercise.wwwapi.Repositories;
using exercise.wwwapi.ViewModels;

namespace exercise.wwwapi.Endpoints
{
    public static class BlogPostEndpoints
    {
        public static void ConfigureBlogPostEndpoints(this WebApplication app)
        {
            var blogposts = app.MapGroup("/blogposts");

            blogposts.MapGet("/all", GetAllBlogPosts);
            blogposts.MapPost("/post/{userid}", PostBlog);
            blogposts.MapPut("/edit/{id}", EditBlogPost);
        }

        public static async Task<IResult> GetAllBlogPosts(IRepository repo)
        {
            return TypedResults.Ok(await repo.GetAllBlogPosts());
        }

        public static async Task<IResult> PostBlog(IRepository repo, PostBlogPost pbp)
        {
            return TypedResults.Ok(await repo.PostBlog(pbp));
        }

        public static async Task<IResult> EditBlogPost(IRepository repo, PutBlogPost putBlogpost, int id)
        {
            return TypedResults.Ok(await repo.EditBlogPost(putBlogpost, id));
        }
    }
}
