﻿namespace Univers.Common.Models
{
    public class Galaxy
    {
        // Props
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        // Nav Props
        public IEnumerable<Star>? Stars { get; set; }
    }
}