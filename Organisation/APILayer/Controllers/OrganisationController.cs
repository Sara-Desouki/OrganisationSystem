using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class OrganisationController( OrganisationService organisationServer) : ControllerBase
    {
        [HttpPost]
       [Authorize(Roles = "Admin")]
        public ActionResult Add(OrganisationDto organisationDto)
        {
            try
            {
                var result = organisationServer.Add(organisationDto);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message); 
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetByPageSize(int pagesize , int pagenumber)
        {
            var result = organisationServer.GetByPageSize(pagesize, pagenumber);
            return Ok(result);
        }

    }
}
