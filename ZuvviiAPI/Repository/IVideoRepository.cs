using ZuvviiAPI.Models;

namespace ZuvviiAPI.Repository
{
    public interface IVideoRepository
    {
        public bool Save(Video video);
        public Video GetById(string Id);
        public Video GetByUrl(string url);
        public List<Video> GetByAuthor(string Author);
        public List<Video> GetByTag(string tag);
        
    }
}
