namespace Orbit.Models
{
    public class Planet
    {
        public int PlanetId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string HeroImage { get; set; }
        public double SunDistance { get; set; }
        public double Diameter { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
    }
}
