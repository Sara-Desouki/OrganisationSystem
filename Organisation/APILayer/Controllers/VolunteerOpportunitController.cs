using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Services;

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
            try
            {
                var result = opportuntyService.Add(volunteerOpportunitDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult GetOpportuntyByPage(int pageSize, int pageNumber)
        {
            try
            {
                var (result, count) = opportuntyService.GetByPageSize(pageSize, pageNumber);

                return (Ok(new PagedResult()
                {
                    item = result,
                    totalCount = count,
                    pageSize = pageSize,
                    pageNumber = pageNumber
                }
                    ));

            }
            catch (Exception e)
            {
                return NotFound("Result Not Found");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = opportuntyService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound($"{ex.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult UpdateOpportunity(int id , VolunteerOpportunityDto dto)
        {
            try
            {
               var result = opportuntyService.UpdateOpportunty(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult PublishedOpportunty(int id)
        {
            try
            {
                var result = opportuntyService.PublishedOpportunty(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message );
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer")]
        public IActionResult CancelOpportunity(int id) 
        {
            try
            {
                var result = opportuntyService.CanceledOppotunity(id);
                return Ok(result);
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
