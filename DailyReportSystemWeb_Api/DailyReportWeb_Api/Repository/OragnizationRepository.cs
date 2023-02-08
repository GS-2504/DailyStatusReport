using DailyReportWeb_Api.Identity;
using DailyReportWeb_Api.Model;
using DailyReportWeb_Api.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Repository
{   
        public class OragnizationRepository : IOragnizationRepository
        {
               /// <summary>
               /// An instance of the ApplicationDbContext class, used for accessing the database.
               /// </summary>
                private readonly ApplicationDbContext _context;

              public OragnizationRepository(ApplicationDbContext context)
              {
                       _context = context;
              }
      
        /// <summary>
        /// Creates a new organization asynchronously by adding it to the database context and then saving the changes.
        /// Returns a boolean value indicating whether the creation was successful or not.
        /// </summary>
        /// <param name="organization">The organization to be created</param>
        /// <returns>A boolean value indicating the success of the operation</returns>
        public async Task<bool> CreateOrganizationAsync(Organization organization)
        {
            await _context.AddAsync(organization);
            return await SaveAsync();
        }


        /// <summary>
        /// Retrieves an organization with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the organization to be retrieved</param>
        /// <returns>The organization with the specified ID</returns>
        public async Task<Organization> GetOrganizationAsync(int id)
        {
            return await _context.Organizations.FindAsync(id);
        }

        /// <summary>
        /// Retrieves a list of all organizations.
        /// </summary>
        /// <returns>An enumerable list of all organizations</returns>
       public async Task<IEnumerable<Organization>> GetAllOrganizationAsync()
        {
           return await Task.FromResult(_context.Organizations.ToList());
        }

        /// <summary>
        /// Removes an organization with the specified ID and saves the changes to the database.
        /// Returns a boolean value indicating whether the Deletion was successful or not.
        /// </summary>
        /// <param name="id">The ID of the organization to be removed</param>
        public async Task<bool> RemoveOrganizationAsync(Organization organization)
        {
            await Task.FromResult(_context.Organizations.Remove(organization));
             return await SaveAsync();
        }


        /// <summary>
        /// Saves all changes made to the database and return a boolean value.
        /// </summary>
        public async Task<bool> SaveAsync()
        {
           return  await _context.SaveChangesAsync()==1?true:false;
        }

        /// <summary>
        /// Updates an existing organization and saves the changes to the database.
        /// Returns a boolean value indicating whether the updateorganization was successful or not.
        /// </summary>
        /// <param name="organization">The updated organization information</param>
        public async Task<bool> UpdateOrganizationAsync(Organization organization)
        {
           await  Task.FromResult(_context.Organizations.Update(organization));
                return await SaveAsync();
        }
    }
}
