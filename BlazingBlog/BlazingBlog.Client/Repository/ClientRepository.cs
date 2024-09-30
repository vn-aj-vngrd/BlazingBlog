using System.Net.Http.Json;
using BlazingBlog.Shared.Models;
using BlazingBlog.Shared.Repository;

namespace BlazingBlog.Client.Repository;

public class ClientRepository : IBlogPostRepository
{
    private readonly HttpClient _httpClient;

    public ClientRepository(HttpClient httpClient) => _httpClient = httpClient;

    public event Action? OnStateChanged;

    public async Task<BlogPost> GetBlogPost(int id) => await _httpClient!.GetFromJsonAsync<BlogPost>($"api/posts/{id}") ??
                                                       throw new InvalidOperationException($"Failed to load post with ID {id}");
    
    public async Task<List<BlogPost>> GetBlogPosts() => await _httpClient!.GetFromJsonAsync<List<BlogPost>>("api/posts") ??
                                                        throw new InvalidOperationException("Failed to load posts");

    public async Task AddBlogPost(string title, string content)
    {
        var response = await _httpClient.PostAsJsonAsync("api/posts", new BlogPost { Title = title, Content = content });
        if (response.IsSuccessStatusCode)
        {
            OnStateChanged?.Invoke();
        }
        else
        {
            throw new InvalidOperationException("Failed to add post");
        }
    }
    
    public async Task UpdateBlogPost(BlogPost post)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/posts/{post.Id}", post);
        if (response.IsSuccessStatusCode)
        {
            OnStateChanged?.Invoke();
        }
        else
        {
            throw new InvalidOperationException("Failed to update post");
        }
    }
    
    public async Task DeleteBlogPost(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/posts/{id}");
        if (response.IsSuccessStatusCode)
        {
            OnStateChanged?.Invoke();
        }
        else
        {
            throw new InvalidOperationException("Failed to delete post");
        }
    }
}