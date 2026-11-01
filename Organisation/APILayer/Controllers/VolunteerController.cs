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
        public async Task<IActionResult> addAsync(VolunteerDto volunteerDto)
        {
            ApiResponse<Volunteer> apiResponse = new ApiResponse<Volunteer>();

            try
            {
                var volunteer = await volunteerService.AddAsync(volunteerDto);
                return Ok(apiResponse.Successed(volunteer));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            ApiResponse<VolunteerResponseDto> apiResponse = new ApiResponse<VolunteerResponseDto>();
            try
            {
                var result =await volunteerService.GetByIdAsync(id);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return NotFound(apiResponse.failed(ex.Message, 404));

            }
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateVolunteer(int id, UpdateVolunteerDto updateVolunteerDto)
        {
            ApiResponse<UpdatedVolunteerDto> apiResponse = new ApiResponse<UpdatedVolunteerDto>();
            try
            {
                var updatedVolunteer = await volunteerService.UpdateVolunteerAsync(id, updateVolunteerDto);
                return Ok(apiResponse.Successed(updatedVolunteer));
            }

            catch (Exception e)
            {
                return BadRequest(apiResponse.failed(e.Message, 400));
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByPazeSizeAsync(int pageSize, int pageNumber)
        {
            ApiResponse<PagedResult> apiResponse = new ApiResponse<PagedResult>();
            var result = await volunteerService.GetByPageSizeAsync(pageSize, pageNumber);

            return Ok(apiResponse.Successed(result));
        }
    }
}
