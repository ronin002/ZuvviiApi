using ZuvviiAPI.Models;

namespace ZuvviiAPI.Repository
{
    public interface ICommentsRepository
    {
        public bool Save(Comment comment);
        public Comment GetById(string Id);
        public List<Comment> GetByVideoId(string Id);
        public List<Comment> GetByAuthorId(string Id);
    }
}
