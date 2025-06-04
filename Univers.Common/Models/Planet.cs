namespace Univers.Common.Models
{
    public class Planet
    {
        // Props
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Satelite { get; set; }
        public required double Gravity { get; set; }

        // Navigation props
        public IEnumerable<Star>? Stars { get; set; }
    }
}
