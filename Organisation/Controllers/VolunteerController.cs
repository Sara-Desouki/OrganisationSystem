using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.Models.DTOs;
using OrganisationSystem.Services;

namespace OrganisationSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VolunteerController(VolunteerService volunteerService) : ControllerBase
    {
        [HttpPost]

        public IActionResult add(VolunteerDto volunteerDto)
        {
            try
            {
                var volunteer = volunteerService.Add(volunteerDto);
                return Ok(volunteer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            try
            {
                var result = volunteerService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }
        [HttpPut]
        public ActionResult UpdateVolunteer(int id, UpdateVolunteerDto updateVolunteerDto)
        {
            try
            {
                var updatedVolunteer = volunteerService.UpdateVolunteer(id, updateVolunteerDto);
                return Ok(updatedVolunteer);
            }
            
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public IActionResult GetByPazeSize(int pageSize , int pageNumber)
        {
            var result = volunteerService.GetVolunteerByPageSize(pageSize , pageNumber);

            return Ok(result);
        }
    }
}
