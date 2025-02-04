using Service.DataEntities;

namespace CV_Site.CacheServices
{
    public class CacheRepo
    {
        public DateTime LastUpdate { get; set; }= DateTime.Now;
        public List<Repo> Repos { get; set; }
    }
}
