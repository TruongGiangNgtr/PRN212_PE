using System;
using System.Collections.Generic;

namespace Q2.Entities;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string RatingCode { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public int DurationMinutes { get; set; }

    public int GenreId { get; set; }

    public int StudioId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Studio Studio { get; set; } = null!;
}
