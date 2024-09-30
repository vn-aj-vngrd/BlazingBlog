using BlazingBlog.Shared.Models;

namespace BlazingBlog.Shared.Repository;

public interface IBlogPostRepository
{
    Task<List<BlogPost>> GetBlogPosts();
    
    Task<BlogPost> GetBlogPost(int id);
    
    Task AddBlogPost(string title, string content);
    
    Task UpdateBlogPost(BlogPost blogPost);
    
    Task DeleteBlogPost(int id);
    
    event Action? OnStateChanged;
}