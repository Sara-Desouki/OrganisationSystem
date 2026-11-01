using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSystem.APILayer;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Services;

using OrganisationSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace OrganisationSystem.ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
   [Authorize]
    public class OrganisationController( OrganisationService organisationServer ) : ControllerBase
    {
       [HttpPost]
       [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddAsync(OrganisationDto organisationDto)
        {
            ApiResponse<Organisations>  apiRequest = new ApiResponse<Organisations>();
            try
            {
                var result = await organisationServer.AddAsync(organisationDto);

                return Ok(apiRequest.Successed(result));
            }
            catch (Exception e)
            {
                return BadRequest(apiRequest.failed(e.Message , 400)); 
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetByPageSize(int pagesize , int pagenumber)
        {
            ApiResponse<PagedResult> apiResponse = new ApiResponse<PagedResult>();

            var result = await organisationServer.GetByPageSizeAsync(pagesize, pagenumber);
            return Ok(apiResponse.Successed(result));
        }

    }
}
