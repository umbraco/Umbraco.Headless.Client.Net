using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IFormService
    {
        /// <summary>
        /// Get all forms
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Form>> GetAll();

        /// <summary>
        /// Get form by id
        /// </summary>
        /// <param name="id">The form id</param>
        /// <returns></returns>
        Task<Form> GetById(Guid id);

        /// <summary>
        /// Submit a new form entry
        /// </summary>
        /// <param name="id">The form id</param>
        /// <param name="data">The data to submit, can be a simple object or a <see cref="IDictionary{TKey,TValue}"/> of type <see cref="string"/>, <see cref="object"/></param>
        /// <returns></returns>
        Task SubmitEntry(Guid id, object data);
    }
}
