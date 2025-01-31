using exercise.wwwapi.Repositories;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            blogposts.MapGet("/testadmin", TestAdmin);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetAllBlogPosts(IRepository repo)
        {
            return TypedResults.Ok(await repo.GetAllBlogPosts());
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> PostBlog(IRepository repo, PostBlogPost pbp)
        {
            return TypedResults.Ok(await repo.PostBlog(pbp));
        }

        [Authorize(Roles ="Admin, User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> EditBlogPost(IRepository repo, PutBlogPost putBlogpost, int id)
        {
            return TypedResults.Ok(await repo.EditBlogPost(putBlogpost, id));
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> TestAdmin()
        {
            return TypedResults.Ok("ADMIN CHECK");
        }
    }
}
