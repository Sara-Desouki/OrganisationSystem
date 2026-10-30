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
        public IActionResult Add(VolunteerOpportunityDto volunteerOpportunitDto)
        {
            ApiResponse<VolunteerOpportunity> apiResponse = new ApiResponse<VolunteerOpportunity>();
            try
            {
                var result = opportuntyService.Add(volunteerOpportunitDto);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }

        }

        [HttpGet]
       [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult GetOpportuntyByPage(int pageSize, int pageNumber)
        {
            ApiResponse<PagedResult> apiResponse = new ApiResponse<PagedResult>();

            try
            {
                var (result, count) = opportuntyService.GetByPageSize(pageSize, pageNumber);

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
        public IActionResult GetById(int id)
        {
            ApiResponse<OpportunityDetailsDto> apiResponse = new ApiResponse<OpportunityDetailsDto>();
            try
            {
                var result = opportuntyService.GetById(id);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return NotFound(apiResponse.failed(ex.Message, 404));
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult UpdateOpportunity(int id, VolunteerOpportunityDto dto)
        {
            ApiResponse<UpdatedOpportunity> apiResponse = new ApiResponse<UpdatedOpportunity>();

            try
            {
                var result = opportuntyService.UpdateOpportunty(id, dto);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult PublishedOpportunty(int id)
        {
            ApiResponse<VolunteerOpportunity> apiResponse = new ApiResponse<VolunteerOpportunity>();
            try
            {
                var result = opportuntyService.PublishedOpportunty(id);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult CancelOpportunity(int id)
        {
            ApiResponse<VolunteerOpportunity> apiResponse = new ApiResponse<VolunteerOpportunity>();
            try
            {
                var result = opportuntyService.CanceledOppotunity(id);
                return Ok(apiResponse.Successed(result));
            }
            catch (Exception ex)
            {
                return BadRequest(apiResponse.failed(ex.Message, 400));
            }
        }
    }
}
