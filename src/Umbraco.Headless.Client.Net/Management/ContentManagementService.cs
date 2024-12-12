﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Security;

namespace Umbraco.Headless.Client.Net.Management
{
    public class ContentManagementService : IContentManagementService
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
        public ContentManagementService(IPasswordBasedConfiguration configuration) : this(configuration, _ => { })
        { }

        /// <summary>
        /// Initializes a new instance of the ContentManagementService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IPasswordBasedConfiguration"/></param>
        /// <param name="configureHttpClient">A delegate to configure the <see cref="HttpClient"/>.</param>
        public ContentManagementService(IPasswordBasedConfiguration configuration, Action<HttpClient> configureHttpClient = null)
        {
            var refitSettings = CreateRefitSettings();
            var authenticationService = new AuthenticationService(configuration, CreateRefitSettings());
            var tokenResolver = new UserPasswordAccessTokenResolver(configuration.Username, configuration.ProjectAlias, authenticationService);
            var httpClient = new HttpClient(new AuthenticatedHttpClientHandler(tokenResolver))
            {
                BaseAddress = new Uri(Constants.Urls.BaseApiUrl)
            };
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("UmbracoHeartcoreNetClient", Constants.GetVersion()));

            if (configureHttpClient != null) configureHttpClient(httpClient);


            Content = new ContentService(configuration, httpClient, refitSettings);
            DocumentType = new DocumentTypeService(configuration, httpClient, refitSettings);
            Forms = new FormService(configuration, httpClient, refitSettings);
            Language = new LanguageService(configuration, httpClient, refitSettings);
            Media = new MediaService(configuration, httpClient, refitSettings);
            MediaType = new MediaTypeService(configuration, httpClient, refitSettings);
            Member = new MemberService(configuration, httpClient, refitSettings);
            MemberGroup = new MemberGroupService(configuration, httpClient, refitSettings);
            MemberType = new MemberTypeService(configuration, httpClient, refitSettings);
            Relation = new RelationService(configuration, httpClient, refitSettings);
            RelationType = new RelationTypeService(configuration, httpClient, refitSettings);
        }

        /// <summary>
        /// Initializes a new instance of the ContentManagementService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IApiKeyBasedConfiguration"/></param>
        /// <param name="configureHttpClient">A delegate to configure the <see cref="HttpClient"/>.</param>
        public ContentManagementService(IApiKeyBasedConfiguration configuration, Action<HttpClient> configureHttpClient = null)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.Urls.BaseApiUrl),
                DefaultRequestHeaders = {{Constants.Headers.ApiKey, configuration.Token}}
            };
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("UmbracoHeartcoreNetClient", Constants.GetVersion()));

            if (configureHttpClient != null) configureHttpClient(httpClient);

            var refitSettings = CreateRefitSettings();

            Content = new ContentService(configuration, httpClient, refitSettings);
            DocumentType = new DocumentTypeService(configuration, httpClient, refitSettings);
            Forms = new FormService(configuration, httpClient, refitSettings);
            Language = new LanguageService(configuration, httpClient, refitSettings);
            Media = new MediaService(configuration, httpClient, refitSettings);
            MediaType = new MediaTypeService(configuration, httpClient, refitSettings);
            Member = new MemberService(configuration, httpClient, refitSettings);
            MemberGroup = new MemberGroupService(configuration, httpClient, refitSettings);
            MemberType = new MemberTypeService(configuration, httpClient, refitSettings);
            Relation = new RelationService(configuration, httpClient, refitSettings);
            RelationType = new RelationTypeService(configuration, httpClient, refitSettings);
        }

        public IContentService Content { get; }
        public IDocumentTypeService DocumentType { get; }
        public IFormService Forms { get; }
        public ILanguageService Language { get; }
        public IMediaService Media { get; }
        public IMediaTypeService MediaType { get; }
        public IMemberService Member { get; }
        public IMemberGroupService MemberGroup { get; }
        public IMemberTypeService MemberType { get; }
        public IRelationService Relation { get; }
        public IRelationTypeService RelationType { get; }


        private static RefitSettings CreateRefitSettings()
        {
            return new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            };
        }
    }
}
