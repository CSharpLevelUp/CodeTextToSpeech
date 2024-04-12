using gitSummaryMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace gitSummaryMvc.Controllers
{
    public class CommitController : Controller
    {
        private readonly TtsdbContext _tsdbContext;

        private readonly ILogger<HomeController> _logger;

        public CommitController(ILogger<HomeController> logger, TtsdbContext tsdbContext)
        {
            _tsdbContext = tsdbContext;
            _logger = logger;
        }

        public IActionResult ListAll()
        {
            return View();
        }

        public IActionResult Commit(int commitId)
        {
          var commit = _tsdbContext.Commits.FirstOrDefault(i => i.Commitid == commitId);

          ViewData["Diff"] = commit.Diff;
          ViewData["CommitId"] = commit.Commitid;
          ViewData["Summary"] = commit.Summary;
          return View();
        }
    }
}
