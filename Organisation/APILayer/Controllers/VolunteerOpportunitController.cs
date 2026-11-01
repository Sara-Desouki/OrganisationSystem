using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Services;
using OrganisationSystem.Domain_Layer.Models;

namespace OrganisationSystem.APILayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class VolunteerOpportunitController(VolunteerOpportunityService opportuntyService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin,Volunteer")]
        public async Task<IActionResult> AddAsync(VolunteerOpportunityDto volunteerOpportunitDto)
        {
            ApiResponse<VolunteerOpportunity> apiResponse = new ApiResponse<VolunteerOpportunity>();
            try
            {
                var result = await opportuntyService.AddAsync(volunteerOpportunitDto);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }

        }

        [HttpGet]
       [Authorize(Roles = "Admin,Volunteer")]
        public async Task<IActionResult> GetOpportuntyByPage(int pageSize, int pageNumber)
        {
            ApiResponse<PagedResult> apiResponse = new ApiResponse<PagedResult>();

            try
            {
                var (result, count) = await opportuntyService.GetByPageSizeAsync(pageSize, pageNumber);

                PagedResult page = new PagedResult()
                {
                    Items = result,
                    totalCount = count,
                    pageSize = pageSize,
                    pageNumber = pageNumber
                };

                return Ok(apiResponse.Successed(page));

            }
            catch (Exception e)
            {
                return NotFound(apiResponse.failed(e.Message, 404));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            ApiResponse<OpportunityDetailsDto> apiResponse = new ApiResponse<OpportunityDetailsDto>();
            try
            {
                var result = await opportuntyService.GetByIdAsync(id);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return NotFound(apiResponse.failed(ex.Message, 404));
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Volunteer")]
        public async Task<IActionResult> UpdateOpportunity(int id, VolunteerOpportunityDto dto)
        {
            ApiResponse<UpdatedOpportunity> apiResponse = new ApiResponse<UpdatedOpportunity>();

            try
            {
                var result = await opportuntyService.UpdateOpportuntyAsync(id, dto);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public async Task<IActionResult> PublishedOpportunty(int id)
        {
            ApiResponse<VolunteerOpportunity> apiResponse = new ApiResponse<VolunteerOpportunity>();
            try
            {
                var result = await opportuntyService.PublishedOpportuntyAsync(id);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public async Task<IActionResult> CancelOpportunityAsync(int id)
        {
            ApiResponse<VolunteerOpportunity> apiResponse = new ApiResponse<VolunteerOpportunity>();
            try
            {
                var result = await opportuntyService.CanceledOppotunityAsync(id);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }
        }
    }
}
