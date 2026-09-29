using OrganisationSystem.Data;
using OrganisationSystem.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrganisationSystem.Validator
{
    public class VolunteerValidator
    {

        public void ValidateDateOfBirth(DateOnly? date) 
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var minAllowedDate = today.AddYears(-90);

            if (date.HasValue)
            {
                if (date.Value < minAllowedDate)
                {
                    throw new ArgumentException("Invalid date of birth");
                }
                if (date.Value > today)
                {
                    throw new ArgumentException("Date of birth cannot be in the future");
                }
            }
           
            

        }

    }
}
