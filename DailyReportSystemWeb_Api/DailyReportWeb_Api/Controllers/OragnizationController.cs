using DailyReportWeb_Api.Model;
using DailyReportWeb_Api.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Controllers
{
    [Route("api/Oragnization")]
    [ApiController]
    public class OragnizationController : ControllerBase
    {
        private readonly IOragnizationRepository _organization;
        public OragnizationController(IOragnizationRepository organization)
        {
            _organization = organization;
        }

        /// <summary>
        /// Retrieves a list of all organizations asynchronously.
        /// </summary>
        /// <returns>An action result containing the list of organizations</returns>
        [HttpGet("GetAllOganization")]
        public async Task<IActionResult> GetAllOganization()
        {
            var result = await _organization.GetAllOrganizationAsync();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new organization asynchronously.
        /// </summary>
        /// <param name="organization">The organization to be created</param>
        /// <returns>An action result indicating the success of the operation</returns>
        [HttpPost("CreateOrganization")]
        public async Task<IActionResult> CreateOrganization([FromBody] Organization organization)
        {
            if (!ModelState.IsValid || organization == null) return BadRequest();
            if(await _organization.CreateOrganizationAsync(organization)) return Ok();
            return BadRequest(error:"Organization was not Created");
        }

        /// <summary>
        /// Updates an existing organization asynchronously.
        /// </summary>
        /// <param name="organization">The updated organization</param>
        /// <returns>An action result indicating the success of the operation</returns>

        [HttpPut("UpdateOrganization")]
        public async Task<IActionResult> UpdateOrganization([FromBody] Organization organization)
        {
            if (!ModelState.IsValid || organization == null) return BadRequest();
            if(await _organization.UpdateOrganizationAsync(organization)) return Ok();
            return BadRequest(error: "Something Went Wrong while updating Data");
        }

        /// <summary>
        /// Deletes an organization asynchronously.
        /// </summary>
        /// <param name="id">The ID of the organization to be deleted</param>
        /// <returns>An action result indicating the success of the operation</returns>

        [HttpDelete("DeleteOrganization{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var result = await _organization.GetOrganizationAsync(id);
            if (result == null) return BadRequest();
            if(await _organization.RemoveOrganizationAsync(result)) return Ok();
            return BadRequest(error:"Something Went Wrong while deleting Data");
        }
    }
}
