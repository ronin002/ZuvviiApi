using ZuvviiAPI.Models;
using ZuvviiAPI.Data;

namespace ZuvviiAPI.Repository.Impl
{
    public class IVideoRepositoryImpl : IVideoRepository
    {
        private readonly DataContext _dataContext;

        public IVideoRepositoryImpl(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Video> GetByAuthor(string Author)
        {
            List<Video> list = new List<Video>();
            Guid author = Guid.Parse(Author);
            list = _dataContext.Videos.Where(x=>x.User.Id == author).ToList();
            return list;
        }

        public Video GetById(string Id)
        {
            Guid id = Guid.Parse(Id);
            var video = _dataContext.Videos.FirstOrDefault(x=>x.Id == id);
            return video;
        }

        public List<Video> GetByTag(string tag)
        {
            List<Video> list = new List<Video>();
            list = _dataContext.Videos.Where(x=>x.Tags.Contains(tag.ToLower())).ToList();
            return list;
        }

        public Video GetByUrl(string url)
        {
            var video = new Video();
            video = _dataContext.Videos.FirstOrDefault(x => x.Url == url);
            return video;
        }

        public bool Save(Video video)
        {
            var videoExists = _dataContext.Videos.Any(x => x.Url == video.Url);
            if (videoExists)
            {
                return false;
            }

            var videoTitleExists = _dataContext.Videos.Any(x => x.Title == video.Title && x.User.Id == video.User.Id);
            if (videoTitleExists)
            {
                return false;
            }

            video.Url = video.Url.ToLower();
            video.Url = video.Tags.ToLower();
     

            _dataContext.Videos.Add(video);
            _dataContext.SaveChanges();
            return true;
        }
    }
}
