using BlazingBlog.Shared.Models;
using BlazingBlog.Shared.Repository;

namespace BlazingBlog;

public static class BlogApiExtensions
{
    public static WebApplication MapBlogApi(this WebApplication app)
    {
        app.MapGet("/api/posts", async (IBlogPostRepository repository) => await repository.GetBlogPosts());

        app.MapGet("/api/posts/{id:int}", async (IBlogPostRepository repository, int id) => await repository.GetBlogPost(id));
        
        app.MapPost("/api/posts", async (IBlogPostRepository repository, BlogPost post) => await repository.AddBlogPost(post.Title, post.Content));

        app.MapPut("/api/posts/{id:int}", async (IBlogPostRepository repository, int id, BlogPost post) =>
        {
            if (id != post.Id)
                throw new InvalidOperationException("The ID in the URL does not match the ID in the body");
            await repository.UpdateBlogPost(post);
            return post;
        });
        
        app.MapDelete("/api/posts/{id:int}", async (IBlogPostRepository repository, int id) =>
        {
            await repository.DeleteBlogPost(id);
            return Results.NoContent();
        });
        
        return app;
    }
}