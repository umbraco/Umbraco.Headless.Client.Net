using System;
using System.Net.Http;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Security;

namespace Umbraco.Headless.Client.Net.Management
{
    public class ContentManagementService
    {
        /// <summary>
        /// Initializes a new instance of the ContentManagementService class
        /// </summary>
        /// <param name="projectAlias">Alias of the Project</param>
        /// <param name="apiKey">Api Key</param>
        public ContentManagementService(string projectAlias, string apiKey) : this(new ApiKeyBasedConfiguration(projectAlias, apiKey))
        { }

        /// <summary>
        /// Initializes a new instance of the ContentManagementService class
        /// </summary>
        /// <param name="projectAlias">Alias of the Project</param>
        /// <param name="username">Umbraco Backoffice Username</param>
        /// <param name="password">Umbraco Backoffice User Password</param>
        public ContentManagementService(string projectAlias, string username, string password) : this(new PasswordBasedConfiguration(projectAlias, username, password))
        { }

        /// <summary>
        /// Initializes a new instance of the ContentManagementService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IPasswordBasedConfiguration"/></param>
        public ContentManagementService(IPasswordBasedConfiguration configuration)
        {
            var httpClient = new HttpClient(new AuthenticatedHttpClientHandler(configuration))
            {
                BaseAddress = new Uri(Constants.Urls.BaseApiUrl)
            };
            DocumentType = new DocumentTypeService(configuration, httpClient);
            Language = new LanguageService(configuration, httpClient);
            MediaType = new MediaTypeService(configuration, httpClient);
            MemberGroup = new MemberGroupService(configuration, httpClient);
            MemberType = new MemberTypeService(configuration, httpClient);
            RelationType = new RelationTypeService(configuration, httpClient);
        }

        /// <summary>
        /// Initializes a new instance of the ContentManagementService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IApiKeyBasedConfiguration"/></param>
        public ContentManagementService(IApiKeyBasedConfiguration configuration)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.Urls.BaseApiUrl),
                DefaultRequestHeaders = {{Constants.Headers.ApiKey, configuration.Token}}
            };
            DocumentType = new DocumentTypeService(configuration, httpClient);
            Language = new LanguageService(configuration, httpClient);
            MediaType = new MediaTypeService(configuration, httpClient);
            MemberGroup = new MemberGroupService(configuration, httpClient);
            MemberType = new MemberTypeService(configuration, httpClient);
            RelationType = new RelationTypeService(configuration, httpClient);
        }

        public IDocumentTypeService DocumentType { get; }
        public ILanguageService Language { get; }
        public IMediaTypeService MediaType { get; }
        public IMemberTypeService MemberType { get; }
        public IMemberGroupService MemberGroup { get; }
        public IRelationTypeService RelationType { get; }
    }
}
