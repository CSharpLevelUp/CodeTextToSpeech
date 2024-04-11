using Auth0.AspNetCore.Authentication;
using gitSummaryMvc.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System;

namespace gitSummaryMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSwaggerGen();


            builder.Services.AddAuth0WebAppAuthentication(options =>
            {
                options.Domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
                options.ClientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
            });

             builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
                options.Audience = Environment.GetEnvironmentVariable("AUTH0_AUDIENCE");
            });

            builder.Services.AddDbContext<TtsdbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ttsdbManagerContext")));

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}
