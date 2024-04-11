using System;
using System.Collections.Generic;

namespace gitSummaryMvc.Models;

public partial class Commit
{
    public int Commitid { get; set; }

    public string? Userid { get; set; }

    public DateTime? Createddate { get; set; }

    public string? Message { get; set; }

    public string? Diff { get; set; }

    public string? Summary { get; set; }

    public virtual User? User { get; set; }
}
