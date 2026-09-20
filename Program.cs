
using Microsoft.IdentityModel.Tokens;
using OrganisationSystem;
using OrganisationSystem.Data;
using OrganisationSystem.Services;
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
            builder.Services.AddScoped(typeof(GenericRepo<>));

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

            builder.Services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                       options.JsonSerializerOptions.Converters.Add(
                            new JsonStringEnumConverter(
                                namingPolicy: null,
                                allowIntegerValues: true
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
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
