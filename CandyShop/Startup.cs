using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CandyShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString
                    ("DefaultConnection")));
                                                        //zelimo da radimo sa podacima iz db-a pomocu entityf   
                                                        //dbcontext=most izmedju app i db-a
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            services.AddControllersWithViews();
            //addscoped: instanca se kreira za svaki request.
            //takodje moze preko singletone-a
            //implementacija interfejsa
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICandyRepository, CandyRepository>(); //registrujem ICandyRepository koji je implementiran u CandyRepository
            services.AddScoped<ShoppingCart>(x => ShoppingCart.GetCart(x)); //kada dodje korisnik za njega se automatski se napravi sopstvena shoping cart
            services.AddScoped<IOrderRepository, OrderRepository>();
            
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //veoma bitan poredak
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession(); //sesija se pre postavlja samog rutiranja
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapRazorPages();
            });
        }
    }
}