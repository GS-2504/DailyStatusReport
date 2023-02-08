using DailyReportWeb_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Repository.IRepository
{
  public interface IOragnizationRepository
    {
        /// <summary>
        /// Creates a new organization
        /// Returns a boolean value indicating whether the Organization creation was successful or not.
        /// </summary>
        /// <param name="organization">Returns a boolean value indicating whether the Organization creation was successful or not</param>
        /// <returns>A b</returns>
      
        Task<bool> CreateOrganizationAsync(Organization organization);

        /// <summary>
        /// Removes an organization asynchronously by finding it in the database context and then removing it.
        /// Returns a boolean value indicating whether the removal was successful or not.
        /// </summary>
        /// <param name="organization">The organization to be removed</param>
        /// <returns>A boolean value indicating the success of the operation</returns>
       
        Task<bool> RemoveOrganizationAsync(Organization organization);

        /// <summary>
        /// Updates an existing organization
        /// </summary>
        /// <param name="organization">The updated organization information</param>
        /// <Returns>Return a boolean value indicating whether the Organization updation  was successful or not</Returns> 

        Task<bool> UpdateOrganizationAsync(Organization organization);

        /// <summary>
        /// Retrieves a list of all organizations
        /// </summary>
        /// <returns>An enumerable list of organizations</returns>
     
        Task<IEnumerable<Organization>> GetAllOrganizationAsync();
      
        /// <summary>
        /// Saves all changes made to the database.
        /// 
        /// </summary>
       
        Task<bool> SaveAsync();
       
        /// <summary>
        /// Find Organization With Specified ID.
        /// <param name="id">The ID of the organization to be find</param>
        /// </summary>
        /// <returns>Find Organization With Specified ID</returns>
       
        Task<Organization>  GetOrganizationAsync(int id);
    }
}
