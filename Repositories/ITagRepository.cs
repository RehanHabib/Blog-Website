using Blog_Website.Models.Domain;

namespace Blog_Website.Repositories
{
    public interface ITagRepository
    {
        
       Task<IEnumerable <Tag>> GetAllAsync(string? searchQuery = null, string? sortBy= null, string? sortDirection=null, int pageNumber =1, int pageSize=100);

        Task<Tag> GetAsync(int id);

        Task<Tag>AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(int id);

        Task<int> CountAsync();
    }
}
