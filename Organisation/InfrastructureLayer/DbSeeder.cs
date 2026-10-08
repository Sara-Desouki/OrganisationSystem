using Bogus;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.Domain_Layer.Enums;
using OrganisationSystem.Domain_Layer.Models;
using OrganisationSystem.InfrastructureLayer;
using OrganisationSystem.Models;
using OrganisationSystem.Models.Enums;

public static class DbSeeder
{
    public static void Seed(Context db)
    { 
        if (db.Set<Organisations>().Any())
            return;


        var faker = new Faker("en");

        
        var organisations = new List<Organisations>();
        for (int i = 0; i < 100; i++)
        {
            organisations.Add(new Organisations(
                name: $"{faker.Company.CompanyName()} {i + 1}",   
                email: faker.Internet.Email(),
                type: faker.PickRandom("NGO", "Charity", "Foundation")
            )
            { Status = faker.PickRandom<Status>() });
        }

        db.AddRange(organisations);
        db.SaveChanges();

        
        var volunteers = new List<Volunteer>();
        for (int i = 0; i < 100; i++)
        {
            volunteers.Add(new Volunteer(
                name: $"{faker.Name.FullName()} {i + 1}",         
                email: faker.Internet.Email(),
                organisationId: faker.PickRandom(organisations).Id,
                phoneNumber: faker.Phone.PhoneNumber("010########"),
                dateOfBirth: DateOnly.FromDateTime(faker.Date.Past(40, DateTime.Now.AddYears(-18))),
                maritalStatus: faker.PickRandom<MaritalStatus>()
            ));
        }

        db.AddRange(volunteers);
        db.SaveChanges();

        
        var opportunities = new List<VolunteerOpportunity>();
        for (int i = 0; i < 100; i++)
        {
            var start = DateOnly.FromDateTime(faker.Date.Soon(60));
            var end = start.AddDays(faker.Random.Int(1, 30));

            opportunities.Add(new VolunteerOpportunity(
                title: faker.Lorem.Sentence(3),
                description: faker.Lorem.Paragraph(),
                organisationId: faker.PickRandom(organisations).Id,
                location: faker.Address.City(),
                startDate: start,
                endDate: end,
                requiredVolunteers: faker.Random.Int(1, 50),
                type: faker.PickRandom<OpportunityType>()
            ));
        }

        db.AddRange(opportunities);
        db.SaveChanges();
    }
}
