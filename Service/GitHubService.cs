using Microsoft.Extensions.Options;
using Octokit;
using Octokit.Clients;
using Service.DataEntities;

namespace Service
{
    public class GitHubService : IGitHubService

    {
        private readonly GitHubClient _client;
        private readonly GitHubIntegrationOptions _options;

        public GitHubService(IOptions<GitHubIntegrationOptions> options)
        {
            _client = new GitHubClient(new ProductHeaderValue("my-app"));
            _options = options.Value;
            var cradToken = new Credentials(_options.Token);
            _client.Credentials = cradToken;
        }

        public async Task<List<Repo>> GetPortfolio()
        {

            var repositories = await _client.Repository.GetAllForCurrent();

            List<Repo> portfolio = new List<Repo>();

            foreach (var repo in repositories)
            {
                var languages = await _client.Repository.GetAllLanguages(repo.Id);
                var lastCommit = await _client.Repository.Commit.Get(repo.Id, repo.DefaultBranch);
                var stars = repo.StargazersCount;
                var pullRequests = await _client.PullRequest.GetAllForRepository(repo.Owner.Login, repo.Name);
                var siteUrl = repo.HtmlUrl;

                Repo repoInfo = new Repo
                {
                    Name = repo.Name,
                    Languages = languages.ToList(),
                    LastCommit = lastCommit.Commit.Author.Date,
                    Stars = stars,
                    PullRequests = pullRequests.Count,
                    SiteUrl = siteUrl
                };

                portfolio.Add(repoInfo);
            }

            return portfolio;

        }

        public async Task<List<Repo>> SearchRepositories(string repositoryName , Language language ,string username)
        {

            var request = new SearchRepositoriesRequest(repositoryName)
            {
                //Language = language,
                User = username
            };

            var searchResults = await _client.Search.SearchRepo(request);
            var res = searchResults.Items;

            List<Repo> repositories = new List<Repo>();

            foreach (var repo in searchResults.Items)
            {
                Repo repoInfo = new Repo
                {
                    Name = repo.Name,
                    LastCommit = (DateTimeOffset)repo.PushedAt,
                    Stars = repo.StargazersCount,
                    SiteUrl = repo.HtmlUrl
                };

                repositories.Add(repoInfo);
            }

            return repositories;
        }
        public async Task<bool> HadActive(DateTime lastUpdate) { 
            var res = await _client.Activity.Events.GetAllUserPerformed(_options.UserName);
            return true; 
        }

    }
}
