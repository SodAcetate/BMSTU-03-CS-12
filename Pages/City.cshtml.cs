using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab_12.Persistence.Contexts;
using Lab_12.Domain.Models;

namespace Lab_12.Pages
{
    [IgnoreAntiforgeryToken]
    public class CityModel : PageModel
    {
        [BindProperty]
        public int CityId { get; set; } = 0;
        [BindProperty]
        public City CityValue { get; set; } = new City {Id = 0 , Name = "", Hotels = new List<Hotel>(), Sights = new List<Sight>()};
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public byte TimeZone { get; set; }
        [BindProperty]
        public Hotel NewHotel { get; set; }
        [BindProperty]
        public Sight NewSight { get; set; }
        public string Message { get; set; } = "No changes";
        public void OnGet(AppDbContext db, int id)
        {
            CityId = id;
            CityValue = db.Cities.FirstOrDefault(item => item.Id == CityId);
            if (CityValue == null) CityValue = new City {Id = 0 , Name = "", Hotels = new List<Hotel>(), Sights = new List<Sight>()};
            Name = CityValue.Name;
            TimeZone = CityValue.TimeZone;
        }
        
        public IActionResult OnPostView(AppDbContext db)
        {
            return Redirect($"City?id={CityId}");
        }
        
        public IActionResult OnPostEdit(AppDbContext db){
            CityValue = db.Cities.FirstOrDefault(item => item.Id == CityId);
            if (CityValue!=null){
                CityValue.Name = Name;
                CityValue.TimeZone = TimeZone;
                db.SaveChanges();
            }
            return Redirect($"City?id={CityId}");
        }

        public IActionResult OnPostDelete(AppDbContext db){
            CityValue = db.Cities.FirstOrDefault(item => item.Id == CityId);
            if (CityValue!=null){
                db.Cities.Remove(CityValue);
                db.SaveChanges();
            }
            return Redirect("Cities");
        }

        public IActionResult OnPostAddHotel(AppDbContext db){
            CityValue = db.Cities.FirstOrDefault(item => item.Id == CityId);
            if (CityValue!=null && NewHotel != null){
                CityValue.Hotels.Add(NewHotel);
                db.SaveChanges();
            }
            return Redirect($"City?id={CityId}");
        }

        public IActionResult OnPostAddSight(AppDbContext db){
            CityValue = db.Cities.FirstOrDefault(item => item.Id == CityId);
            if (CityValue!=null && NewHotel != null){
                CityValue.Sights.Add(NewSight);
                db.SaveChanges();
            }
            NewSight = null;
            return Redirect($"City?id={CityId}");
        }


    }
}