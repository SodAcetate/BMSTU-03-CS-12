using System.Collections.Generic;

namespace Lab_12.Domain.Models
{
    public class City
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public byte TimeZone {get; set;}
        public IList<Hotel> Hotels {get; set;} = new List<Hotel>();
        public IList<Sight> Sights {get; set;} = new List<Sight>();

    }
}