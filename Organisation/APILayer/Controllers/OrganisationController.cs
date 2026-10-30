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
        public ActionResult Add(OrganisationDto organisationDto)
        {
            ApiResponse<Organisations>  apiRequest = new ApiResponse<Organisations>();
            try
            {
                var result = organisationServer.Add(organisationDto);

                return Ok(apiRequest.Successed(result));
            }
            catch (Exception e)
            {
                return BadRequest(apiRequest.failed(e.Message , 400)); 
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetByPageSize(int pagesize , int pagenumber)
        {
            ApiResponse<PagedResult> apiResponse = new ApiResponse<PagedResult>();

            var result = organisationServer.GetByPageSize(pagesize, pagenumber);
            return Ok(apiResponse.Successed(result));
        }

    }
}
