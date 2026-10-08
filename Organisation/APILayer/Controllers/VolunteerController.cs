using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Services;

namespace OrganisationSystem.ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class VolunteerController(VolunteerService volunteerService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles ="Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult GetByPazeSize(int pageSize , int pageNumber)
        {
            var result = volunteerService.GetByPageSize(pageSize , pageNumber);

            return Ok(result);
        }
    }
}
