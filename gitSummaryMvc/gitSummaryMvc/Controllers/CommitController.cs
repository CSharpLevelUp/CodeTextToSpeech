using gitSummaryMvc.Models;
using gitSummaryMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        public IActionResult Commit(int commitId)
        {
          var commit = _tsdbContext.Commits.FirstOrDefault(i => i.Commitid == commitId);

          ViewData["Diff"] = commit.Diff;
          ViewData["CommitId"] = commit.Commitid;
          ViewData["Summary"] = commit.Summary;
          return View();
        }

        [Authorize]
        public IActionResult List()
        {
            var commits = _tsdbContext.Commits.ToList();
            commits.ForEach(a => a.User = _tsdbContext.Users.Where(b => b.Userid == a.Userid).First());

            ViewData["commits"] = commits;
            return View(commits);
        }
    }
}
