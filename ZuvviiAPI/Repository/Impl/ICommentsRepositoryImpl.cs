using ZuvviiAPI.Models;
using ZuvviiAPI.Data;

namespace ZuvviiAPI.Repository.Impl
{
    public class ICommentsRepositoryImpl : ICommentsRepository
    {
        private readonly DataContext _dataContext;
        public ICommentsRepositoryImpl(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Comment> GetByAuthorId(string Id)
        {
            List<Comment> list = new List<Comment>();
            Guid id = Guid.Parse(Id);
            list = _dataContext.Comments.Where(x => x.Author.Id == id).ToList();
            return list;
        }

        public Comment GetById(string Id)
        {
            Guid id = Guid.Parse(Id);
            var comment = _dataContext.Comments.FirstOrDefault(x => x.Id == id);
            return comment;
        }

        public List<Comment> GetByVideoId(string Id)
        {
            List<Comment> list = new List<Comment>();
            Guid id = Guid.Parse(Id);
            list = _dataContext.Comments.Where(x => x.Video.Id == id).ToList();
            return list;

        }

        public bool Save(Comment comment)
        {
            var commentExists = _dataContext.Comments.Any(x => x.Id == comment.Id);
            if (commentExists)
            {
                return false;
            }

            var commentVideoExists = _dataContext.Comments.Any(x => x.Commentary == comment.Commentary && x.Video.Id == comment.Video.Id);
            if (commentVideoExists)
            {
                return false;
            }

            _dataContext.Comments.Add(comment);
            _dataContext.SaveChanges();
            return true;
        }
    }
}
