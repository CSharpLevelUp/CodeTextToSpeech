using System;
using System.Collections.Generic;

namespace gitSummaryMvc.Models;

public partial class Commit
{
    public int? CommitId { get; set; }

    public int? UserId { get; set; }

    public DateTime? Created { get; set; }

    public string? Message { get; set; }

    public string? Diff { get; set; }

    public string? Summary { get; set; }
}
