using Microsoft.AspNetCore.Mvc;
using Octokit;
using Service;
using Service.DataEntities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CV_Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public ReposController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }


        // GET: api/<ReposController>
        [HttpGet]
        public async Task<ActionResult<List<Repo>>> GetAll()
        {
            var res = await _gitHubService.GetPortfolio();
            return Ok(res);
        }

        // GET: api/<ReposController>
        [HttpPost]
        public async Task<ActionResult<List<Repo>>> GetBySearch([FromBody] SearchRepo searchRepo)
        {
            var res = await _gitHubService.SearchRepositories(searchRepo.Name, searchRepo.Language, searchRepo.UserName);
            return Ok(res);
        }

        // GET api/<ReposController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ReposController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ReposController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ReposController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
