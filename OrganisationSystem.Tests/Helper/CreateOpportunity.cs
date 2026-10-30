using OrganisationSystem.Domain_Layer.Enums;
using OrganisationSystem.Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisationSystem.Tests.Helper
{
    public class CreateOpportunity
    {
        public  static VolunteerOpportunity CreateVolunteerOpportunity(
               string title = "Test",
                int organisationId = 1)
        {
            return new VolunteerOpportunity(
                title: title,
                description: "Description",
                organisationId: organisationId,
                location: "Cairo",
                startDate: DateOnly.FromDateTime(DateTime.Today),
                endDate: DateOnly.FromDateTime(DateTime.Today).AddDays(1),
                requiredVolunteers: 4,
                type: OpportunityType.NewBusiness);   
        }

        public static VolunteerOpportunity CreatePublishedOpportunity(string title = "Test")
        {
            var opp = CreateVolunteerOpportunity(title);
            opp.PublishedStatus();   
            return opp;             
        }
    }
}
