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
        /// <returns><see cref="IEnumerable{Content}"/></returns>
        Task<IEnumerable<Content>> GetRoot(string culture = "en-us");

        /// <summary>
        /// Gets the root Content items as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContentBase"/> interface</typeparam>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<T>> GetRoot<T>(string culture = "en-us") where T : IContentBase;

        /// <summary>
        /// Gets a single Content item by its id
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id of the Content item</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="depth">Integer value specifying the number of levels to retrieve</param>
        /// <returns><see cref="Content"/></returns>
        Task<Content> GetById(Guid id, string culture = "en-us", int depth = 1);

        /// <summary>
        /// Gets a single Content item by its id as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContentBase"/> interface</typeparam>
        /// <param name="id"><see cref="Guid"/> id of the Content item</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="depth">Integer value specifying the number of levels to retrieve</param>
        /// <returns><see cref="Content"/></returns>
        Task<T> GetById<T>(Guid id, string culture = "en-us", int depth = 1) where T : IContentBase;

        /// <summary>
        /// Gets a single Content item by its Url
        /// </summary>
        /// <param name="url">Url for the content to retrieve, ie. /home/products</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="depth">Integer value specifying the number of levels to retrieve</param>
        /// <returns><see cref="Content"/></returns>
        Task<Content> GetByUrl(string url, string culture = "en-us", int depth = 1);

        /// <summary>
        /// Gets a single Content item by its Url as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContentBase"/> interface</typeparam>
        /// <param name="url">Url for the content to retrieve, ie. /home/products</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="depth">Integer value specifying the number of levels to retrieve</param>
        /// <returns><see cref="Content"/></returns>
        Task<T> GetByUrl<T>(string url, string culture = "en-us", int depth = 1) where T : IContentBase;

        /// <summary>
        /// Gets a paged list of Content items by their Parent Id
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id of the Parent</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent"/></returns>
        Task<PagedContent> GetChildren(Guid id, string culture = "en-us", int page = 1, int pageSize = 10);

        /// <summary>
        /// Gets a paged list of Content items by their Parent Id as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContentBase"/> interface</typeparam>
        /// <param name="id"><see cref="Guid"/> id of the Parent</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent{T}"/></returns>
        Task<PagedContent<T>> GetChildren<T>(Guid id, string culture = "en-us", int page = 1, int pageSize = 10) where T : IContentBase;

        /// <summary>
        /// Gets the descendants of a Content item by its Id
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id of the Content to retrieve descendants for</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent"/></returns>
        Task<PagedContent> GetDescendants(Guid id, string culture = "en-us", int page = 1, int pageSize = 10);

        /// <summary>
        /// Gets the descendants of a Content item by its Id as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContentBase"/> interface</typeparam>
        /// <param name="id"><see cref="Guid"/> id of the Content to retrieve descendants for</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <param name="page">Integer specifying the page number (Optional)</param>
        /// <param name="pageSize">Integer specifying the page size (Optional)</param>
        /// <returns><see cref="PagedContent{T}"/></returns>
        Task<PagedContent<T>> GetDescendants<T>(Guid id, string culture = "en-us", int page = 1, int pageSize = 10) where T : IContentBase;

        /// <summary>
        /// Gets the ancestors of a Content item by its Id
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id of the Content to retrieve ancestors for</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <returns><see cref="IEnumerable{Content}"/></returns>
        Task<IEnumerable<Content>> GetAncestors(Guid id, string culture = "en-us");

        /// <summary>
        /// Gets the ancestors of a Content item by its Id as the specified type
        /// </summary>
        /// <typeparam name="T">A type that inherits from the <see cref="IContentBase"/> interface</typeparam>
        /// <param name="id"><see cref="Guid"/> id of the Content to retrieve ancestors for</param>
        /// <param name="culture">Content Culture (Optional)</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<T>> GetAncestors<T>(Guid id, string culture = "en-us") where T : IContentBase;
    }
}
