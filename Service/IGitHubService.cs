using Octokit;
using Service.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IGitHubService
    {
        public Task<List<Repo>> GetPortfolio();
        public Task<List<Repo>> SearchRepositories(string repositoryName = "", Language language = Language.C, string username = "");
        public Task<bool> HadActive(DateTime lastUpdate);


    }
}