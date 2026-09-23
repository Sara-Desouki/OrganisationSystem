using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSystem.Data;
using OrganisationSystem.Models;
using OrganisationSystem.Models.DTOs;
using OrganisationSystem.Services;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace OrganisationSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); 
            }
        }


        [HttpGet]
        public ActionResult GetByPageSize(int pagesize , int pagenumber)
        {
            var result = organisationServer.GetByPageSize(pagesize, pagenumber);
            return Ok(result);
        }

    }
}
