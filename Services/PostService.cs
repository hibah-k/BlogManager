using BlogPostManager.Models;
using BlogPostManager.Repositories.Interfaces;
using BlogPostManager.Services.Interfaces;

namespace BlogPostManager.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Post post)
        {
            await _repository.CreateAsync(post);
        }

        public async Task UpdateAsync(Post post)
        {
            await _repository.UpdateAsync(post);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
