using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Lab_12.Domain.Models;
using Lab_12.Persistence.Contexts;
 
namespace Lab_12.Pages
{
    [IgnoreAntiforgeryToken]
    public class CitiesModel : PageModel
    {

        [BindProperty]
        public City NewCity { get; set; } = new City {Id = 0 , Name = "", Hotels = new List<Hotel>(), Sights = new List<Sight>()};
        public List<City> CityList = new List<City>();
        public List<Hotel> HotelList = new List<Hotel>();
        
 
        public void OnGet(AppDbContext db)
        {
            CityList = db.Cities.ToList();
            HotelList = db.Hotels.ToList();
        }

        public void OnPostAddCity(AppDbContext db){
            if (NewCity != null)
            {
                db.Cities.Add(NewCity);
                db.SaveChanges();
            }
            OnGet(db);
        }

    }
}