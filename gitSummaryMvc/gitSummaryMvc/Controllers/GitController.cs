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
        [Authorize]
        [HttpGet]
        public ActionResult<String> GetTest()
        {
            return Ok("Success");
        }

        [Route("users")]
        [HttpGet]
        [Authorize]
        public IEnumerable<User> GetUsers(int userid)
        {
            return _tsdbContext.Users.ToList();
        }

        [Route("commits")]
        [HttpGet]
        public IEnumerable<Commit> GetCommitsByUserId(int userId)
        {   
            return _tsdbContext.Commits.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Commit>> Post(Commit commit)
        {
            if (commit.UserId == null)
            {
                return BadRequest();
            }

            _tsdbContext.Add(commit);
            _tsdbContext.SaveChanges();

            return Ok(commit);
        }

    }
}
