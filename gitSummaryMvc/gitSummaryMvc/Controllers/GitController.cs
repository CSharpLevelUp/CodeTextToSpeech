using gitSummaryMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gitSummaryMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitController: Controller
    {
        private readonly TtsdbContext _tsdbContext;

        public GitController(TtsdbContext tsdbContext)
        {
            _tsdbContext = tsdbContext;
        }

        [Route("test")]
        [HttpGet]
        public ActionResult<String> GetTest()
        {
            return Ok("Success");
        }

        [Route("users")]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _tsdbContext.Users.ToList();
        }

        [Route("commits")]
        [HttpGet]
        public IEnumerable<Commit> GetCommitsByUserId(string userId)
        {   
            return _tsdbContext.Commits.Where(commit => commit.Userid == userId).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Commit>> Post(Commit commit)
        {
            if (commit.Userid == null)
            {
                return BadRequest();
            }

            _tsdbContext.Add(commit);
            _tsdbContext.SaveChanges();

            return Ok(commit);
        }

    }
}
