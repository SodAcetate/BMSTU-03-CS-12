namespace Lab_12.Domain.Models
{
    public class Sight
    {
        public int Id {get; set;}
        public int CityId {get; set;}
        public City City {get; set;}
        public string Name {get; set;}
        public string Location {get; set;}
        public double PricePerVisit {get; set;}
    }
}