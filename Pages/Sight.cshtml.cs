using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab_12.Persistence.Contexts;
using Lab_12.Domain.Models;

namespace Lab_12.Pages
{
    [IgnoreAntiforgeryToken]
    public class SightModel : PageModel
    {
        [BindProperty]
        public int CityId { get; set; } = 0;
        [BindProperty]
        public int Id { get; set; } = 0;
        [BindProperty]
        public Sight SightValue { get; set; } = new Sight {Id = 0, CityId = 0 , Name = "", Location = ""};
        public string CityName { get; set; } = "";

        public void OnGet(AppDbContext db, int id)
        {
            SightValue = db.Sights.FirstOrDefault(item => item.Id == id);
            if (SightValue == null) SightValue = new Sight {Id = 0, CityId = 0 , Name = "", Location = ""};
            CityId = SightValue.CityId;
            Id = id;
            CityName = db.Cities.FirstOrDefault(item => item.Id == CityId).Name;
            if (CityName == null) CityName = "";
        }

        public IActionResult OnPostEdit(AppDbContext db){
            Sight temp = db.Sights.FirstOrDefault(item => item.Id == SightValue.Id);
            temp.Name = SightValue.Name;
            temp.Location = SightValue.Location;
            temp.PricePerVisit = SightValue.PricePerVisit;
            db.SaveChanges();
            Id = SightValue.Id;
            return Redirect($"Sight?id={Id}");
        }

        public IActionResult OnPostDelete(AppDbContext db){
            SightValue = db.Sights.FirstOrDefault(item => item.Id == Id);
            CityId = SightValue.CityId;
            db.Sights.Remove(SightValue);
            db.SaveChanges();
            return Redirect($"City?id={CityId}");
        }

    }
}