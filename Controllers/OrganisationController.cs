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
           var result = organisationServer.Add(organisationDto);
            
            return Ok(result);
        }


        [HttpGet]
        public ActionResult<IEnumerable<Organisations>> GetByPageSize(int pagesize , int pagenumber)
        {
            var result = organisationServer.GetByPageSize(pagesize, pagenumber);
            return Ok(result);
        }

    }
}
