using System;
using System.Collections.Generic;

namespace Q2.Entities;

public partial class Studio
{
    public int StudioId { get; set; }

    public string StudioName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
