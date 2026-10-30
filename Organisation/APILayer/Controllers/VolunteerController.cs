using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.APILayer;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Services;
using OrganisationSystem.Models;

namespace OrganisationSystem.ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize]
    public class VolunteerController(VolunteerService volunteerService) : ControllerBase
    {
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public IActionResult add(VolunteerDto volunteerDto)
        {
            ApiResponse<Volunteer> apiResponse = new ApiResponse<Volunteer>();

            try
            {
                var volunteer = volunteerService.Add(volunteerDto);
                return Ok(apiResponse.Successed(volunteer));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult GetById(int id)
        {
            ApiResponse<VolunteerResponseDto> apiResponse = new ApiResponse<VolunteerResponseDto>();
            try
            {
                var result = volunteerService.GetById(id);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return NotFound(apiResponse.failed(ex.Message, 404));

            }
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public ActionResult UpdateVolunteer(int id, UpdateVolunteerDto updateVolunteerDto)
        {
            ApiResponse<UpdatedVolunteerDto> apiResponse = new ApiResponse<UpdatedVolunteerDto>();
            try
            {
                var updatedVolunteer = volunteerService.UpdateVolunteer(id, updateVolunteerDto);
                return Ok(apiResponse.Successed(updatedVolunteer));
            }

            catch (Exception e)
            {
                return BadRequest(apiResponse.failed(e.Message, 400));
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetByPazeSize(int pageSize, int pageNumber)
        {
            ApiResponse<PagedResult> apiResponse = new ApiResponse<PagedResult>();
            var result = volunteerService.GetByPageSize(pageSize, pageNumber);

            return Ok(apiResponse.Successed(result));
        }
    }
}
