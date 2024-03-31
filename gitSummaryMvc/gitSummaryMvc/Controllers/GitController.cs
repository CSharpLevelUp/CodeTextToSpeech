using Microsoft.AspNetCore.Mvc;

namespace gitSummaryMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitController: Controller
    {
        [HttpGet]
        public IEnumerable<String> Get()
        {
            return new List<string> { ""};
        }

    }
}
