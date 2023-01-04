using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab_12.Persistence.Contexts;
using Lab_12.Domain.Models;

namespace Lab_12.Pages
{
    [IgnoreAntiforgeryToken]
    public class HotelModel : PageModel
    {
        [BindProperty]
        public int CityId { get; set; } = 0;
        [BindProperty]
        public int Id { get; set; } = 0;
        [BindProperty]
        public Hotel HotelValue { get; set; } = new Hotel {Id = 0, CityId = 0 , Name = "", Address = ""};
        public string CityName { get; set; } = "";

        public string Message { get; set; } = "No changes";
        public void OnGet(AppDbContext db, int id)
        {
            HotelValue = db.Hotels.FirstOrDefault(item => item.Id == id);
            if (HotelValue == null) HotelValue = new Hotel {Id = 0, CityId = 0 , Name = "", Address = ""};
            CityId = HotelValue.CityId;
            Id = id;
            CityName = db.Cities.FirstOrDefault(item => item.Id == CityId).Name;
            if (CityName == null) CityName = "";
        }

        public IActionResult OnPostEdit(AppDbContext db){
            Hotel temp = db.Hotels.FirstOrDefault(item => item.Id == HotelValue.Id);
            temp.Name = HotelValue.Name;
            temp.Address = HotelValue.Address;
            temp.PricePerNight = HotelValue.PricePerNight;
            db.SaveChanges();
            Id = HotelValue.Id;
            return Redirect($"Hotel?id={Id}");
        }

        public IActionResult OnPostDelete(AppDbContext db){
            HotelValue = db.Hotels.FirstOrDefault(item => item.Id == Id);
            CityId = HotelValue.CityId;
            db.Hotels.Remove(HotelValue);
            db.SaveChanges();
            return Redirect($"City?id={CityId}");
        }

    }
}