
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem04.Data;
using StudentManagementSystem04.Model;
using StudentManagementSystem04.seedData;


namespace StudentManagementSystem04
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;
            var options = new DbContextOptionsBuilder<StudentManagementSystemDbContext>()
                .UseSqlServer(config.GetConnectionString("DefaultConnection"))
                .Options;

            // Add services to the container.
            builder.Services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<StudentManagementSystemDbContext>();
            builder.Services.AddDbContext<StudentManagementSystemDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                         .AllowAnyHeader()
                                         .AllowAnyMethod();

                                  });
            });



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            StudentManagementSystemDbContext dbContext = new StudentManagementSystemDbContext(options);


            Seeding seedData = new Seeding(dbContext, app.Services);
            seedData.SeedingData();


            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
