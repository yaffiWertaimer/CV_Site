using Microsoft.Extensions.Caching.Memory;
using Octokit;
using Service;
using Service.DataEntities;

namespace CV_Site.CacheServices
{
    public class CacheGitHubServices : IGitHubService
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMemoryCache _memoryCache;

        private const string PortfolioKey = "PortfolioKey";

        public CacheGitHubServices(IGitHubService gitHubService, IMemoryCache memoryCache)
        {
            _gitHubService = gitHubService;
            _memoryCache = memoryCache;
        }
        public async Task<List<Repo>> GetPortfolio()
        {
            if (_memoryCache.TryGetValue(PortfolioKey, out List<Repo> cacheRepos))
                //   if(await _gitHubService.HadActive(cacheRepos.LastUpdate) == false)
                // return cacheRepos?.Repos;
                return cacheRepos;

            var cachOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
                .SetSlidingExpiration(TimeSpan.FromSeconds(10));

            var portfolio = await _gitHubService.GetPortfolio();

            _memoryCache.Set(PortfolioKey, portfolio,cachOptions);
            return portfolio;
        }

        public async Task<List<Repo>> SearchRepositories(string repositoryName = "", Language language = Language.C, string username = "")
        {
            return await _gitHubService.SearchRepositories(repositoryName, language, username);
        }

        public async Task<bool> HadActive(DateTime lastUpdate)
        {
            return await _gitHubService.HadActive(lastUpdate);
        }
    }
}
