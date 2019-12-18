using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    /// <summary>
    /// Interface exposing methods available for the Content part of the Content Delivery API
    /// https://cdn.umbraco.io/content
    /// </summary>
    public interface IContentDelivery
    {
        /// <summary>
        /// Gets the root Content items
        /// </summary>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <returns><see cref="IEnumerable{Content}"/></returns>
        Task<IEnumerable<Content>> GetRoot(string culture = null);

        /// <summary>
        /// Gets the root Content items as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContent"/> interface</typeparam>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<T>> GetRoot<T>(string culture = null) where T : IContent;

        /// <summary>
        /// Gets a single Content item by its id
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id of the Content item</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="depth">Integer value specifying the number of levels to retrieve</param>
        /// <returns><see cref="Content"/></returns>
        Task<Content> GetById(Guid id, string culture = null, int depth = 1);

        /// <summary>
        /// Gets a single Content item by its id as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContent"/> interface</typeparam>
        /// <param name="id"><see cref="Guid"/> id of the Content item</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="depth">Integer value specifying the number of levels to retrieve</param>
        /// <returns><see cref="Content"/></returns>
        Task<T> GetById<T>(Guid id, string culture = null, int depth = 1) where T : IContent;

        /// <summary>
        /// Gets a single Content item by its Url
        /// </summary>
        /// <param name="url">Url for the content to retrieve, ie. /home/products</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="depth">Integer value specifying the number of levels to retrieve</param>
        /// <returns><see cref="Content"/></returns>
        Task<Content> GetByUrl(string url, string culture = null, int depth = 1);

        /// <summary>
        /// Gets a single Content item by its Url as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContent"/> interface</typeparam>
        /// <param name="url">Url for the content to retrieve, ie. /home/products</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="depth">Integer value specifying the number of levels to retrieve</param>
        /// <returns><see cref="Content"/></returns>
        Task<T> GetByUrl<T>(string url, string culture = null, int depth = 1) where T : IContent;

        /// <summary>
        /// Gets a paged list of Content items by their Parent Id
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id of the Parent</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent"/></returns>
        Task<PagedContent> GetChildren(Guid id, string culture = null, int page = 1, int pageSize = 10);

        /// <summary>
        /// Gets a paged list of Content items by their Parent Id as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContent"/> interface</typeparam>
        /// <param name="id"><see cref="Guid"/> id of the Parent</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent{T}"/></returns>
        Task<PagedContent<T>> GetChildren<T>(Guid id, string culture = null, int page = 1, int pageSize = 10) where T : IContent;

        /// <summary>
        /// Gets the descendants of a Content item by its Id
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id of the Content to retrieve descendants for</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent"/></returns>
        Task<PagedContent> GetDescendants(Guid id, string culture = null, int page = 1, int pageSize = 10);

        /// <summary>
        /// Gets the descendants of a Content item by its Id as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContent"/> interface</typeparam>
        /// <param name="id"><see cref="Guid"/> id of the Content to retrieve descendants for</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent{T}"/></returns>
        Task<PagedContent<T>> GetDescendants<T>(Guid id, string culture = null, int page = 1, int pageSize = 10) where T : IContent;

        /// <summary>
        /// Gets the ancestors of a Content item by its Id
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id of the Content to retrieve ancestors for</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <returns><see cref="IEnumerable{Content}"/></returns>
        Task<IEnumerable<Content>> GetAncestors(Guid id, string culture = null);

        /// <summary>
        /// Gets the ancestors of a Content item by its Id as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContent"/> interface</typeparam>
        /// <param name="id"><see cref="Guid"/> id of the Content to retrieve ancestors for</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<T>> GetAncestors<T>(Guid id, string culture = null) where T : IContent;

        /// <summary>
        /// Gets all Content of a specific type
        /// </summary>
        /// <param name="contentType">The Content Type</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent"/></returns>
        Task<PagedContent> GetByType(string contentType, string culture = null, int page = 1, int pageSize = 10);

        /// <summary>
        /// Gets all Content of a specific type
        /// </summary>
        /// <remarks>
        /// The Content Type alias is derived from the class type name.
        /// </remarks>
        /// <typeparam name="T">A type that inherits from the <see cref="IContent"/> interface</typeparam>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent{T}"/></returns>
        Task<PagedContent<T>> GetByType<T>(string culture = null, int page = 1, int pageSize = 10) where T : IContent;

        /// <summary>
        /// Gets all Content of a specific type
        /// </summary>
        /// <remarks>
        /// This allows manual specification of the Content Type alias instead of deriving it from the class type name.
        /// </remarks>
        /// <typeparam name="T">A type that inherits from the <see cref="IContent"/> interface</typeparam>
        /// <param name="contentTypeAlias">Alias of the ContentType</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent{T}"/></returns>
        Task<PagedContent<T>> GetByTypeAlias<T>(string contentTypeAlias, string culture = null, int page = 1,
            int pageSize = 10) where T : IContent;

        /// <summary>
        /// Filter content based on property value and optionally content type
        /// </summary>
        /// <param name="filter"><see cref="ContentFilter"/> with at least one property</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns></returns>
        Task<PagedContent> Filter(ContentFilter filter, string culture = null, int page = 1, int pageSize = 10);

        /// <summary>
        /// Search for content by term
        /// </summary>
        /// <param name="term">Search term</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent"/></returns>
        Task<PagedContent> Search(string term, string culture = null, int page = 1, int pageSize = 10);
    }
}
