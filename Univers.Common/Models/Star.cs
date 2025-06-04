namespace Univers.Common.Models
{
    public class Star
    {
        // Props
        public int Id { get; set; }
        public required string Name { get; set; }
        public required bool IsDeath { get; set; }

        // Nav Props
        public Galaxy? Galaxy { get; set; }
        public IEnumerable<Planet>? Planets { get; set; }
    }
}