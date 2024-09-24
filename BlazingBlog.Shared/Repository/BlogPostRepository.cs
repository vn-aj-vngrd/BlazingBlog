using BlazingBlog.Shared.Models;

namespace BlazingBlog.Shared.Repository;

public class BlogPostRepository : IBlogPostRepository
{
    private List<BlogPost> _blogPosts = new List<BlogPost>()
    {
        new BlogPost { Id = 1, Title = "Post 1", Content = "This is the first post content." },
        new BlogPost { Id = 2, Title = "Post 2", Content = "This is the second post content." },
        new BlogPost { Id = 3, Title = "Post 3", Content = "This is the third post content." },
    };


    public Task<List<BlogPost>> GetBlogPosts() => Task.FromResult(_blogPosts);

    public Task<BlogPost> GetBlogPost(int id) => Task.FromResult(_blogPosts.First(x => x.Id == id));

    public Task AddBlogPost(string title, string content)
    {
        var newId = _blogPosts.Count != 0 ? _blogPosts.Max(x => x.Id) + 1 : 0;
        var newBlogPost = new BlogPost { Id = newId, Title = title, Content = content };
        return Task.Run(() => _blogPosts.Add(newBlogPost));
    }

    public Task UpdateBlogPost(BlogPost blogPost) => Task.Run(() =>
    {
        var existingBlogPost = _blogPosts.First(x => x.Id == blogPost.Id);
        existingBlogPost.Title = blogPost.Title;
        existingBlogPost.Content = blogPost.Content;
    });

    public Task DeleteBlogPost(int id) => Task.Run(() => _blogPosts.Remove(_blogPosts.First(x => x.Id == id)));
}