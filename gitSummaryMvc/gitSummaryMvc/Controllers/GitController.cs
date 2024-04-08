using gitSummaryMvc.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _tsdbContext.Users.ToList();
        }

    }
}
