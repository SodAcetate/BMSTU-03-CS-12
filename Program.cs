using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Lab_12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            builder.Services.AddSession();
            var app = builder.Build();
            
            app.UseDeveloperExceptionPage();
            
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseMvc();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapRazorPages();
            });
            
            app.Run();
        }

    }
}