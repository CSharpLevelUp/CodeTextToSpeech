using System;
using System.Collections.Generic;

namespace gitSummaryMvc.Models;

public partial class User
{
    public string Userid { get; set; } = null!;

    public string? Username { get; set; }

    public virtual ICollection<Commit> Commits { get; set; } = new List<Commit>();
}
