
using Microsoft.IdentityModel.Tokens;
using OrganisationSystem.APILayer;
using OrganisationSystem.ApplicationLayer.Interfaces;
using OrganisationSystem.ApplicationLayer.Services;
using OrganisationSystem.ApplicationLayer.Validator;
using OrganisationSystem.Domain_Layer.Models;
using OrganisationSystem.InfrastructureLayer;
using OrganisationSystem.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace Organisation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            var  jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
            builder.Services.AddSingleton(jwtOptions);
            builder.Services.AddScoped<Context>();
            builder.Services.AddScoped<OrganisationService>();
            builder.Services.AddScoped<VolunteerService>();
            builder.Services.AddScoped<VolunteerValidator>();
            builder.Services.AddScoped<VolunteerOpportunityService>();

            builder.Services.AddScoped<IGenericRepo<Volunteer>, GenericRepo<Volunteer>>();
            builder.Services.AddScoped<IGenericRepo<Organisations>, GenericRepo<Organisations>>();
            builder.Services.AddScoped<IGenericRepo<VolunteerOpportunity>, GenericRepo<VolunteerOpportunity>>();

            builder.Services.AddAuthentication().AddJwtBearer("Bearer", options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };

            }

            );

            builder.Services.AddAuthorization();

            builder.Services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                       options.JsonSerializerOptions.Converters.Add(
                            new JsonStringEnumConverter(
                                namingPolicy: null,
                                allowIntegerValues: false
                             )
                );
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<Context>();
                DbSeeder.Seed(db);
            }

            

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
