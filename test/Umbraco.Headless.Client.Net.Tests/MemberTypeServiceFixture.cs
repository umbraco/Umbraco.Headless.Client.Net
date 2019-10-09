using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class MemberTypeServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public MemberTypeServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task AtRoot_ReturnsAllDocumentTypes()
        {
            var service = new MemberTypeService(_configuration,
                GetMockedHttpClient("/member/type", MemberTypeServiceJson.GetRoot));

            var documentTypes = await service.GetRoot();

            Assert.NotNull(documentTypes);
            Assert.Equal(1, documentTypes.Count());
        }

        [Fact]
        public async Task ByAlias_ReturnsSingleDocumentType()
        {
            var service = new MemberTypeService(_configuration,
                GetMockedHttpClient("/member/type/Member", MemberTypeServiceJson.ByAlias));

            var memberType = await service.GetByAlias("Member");

            Assert.NotNull(memberType);
            Assert.NotNull(memberType);
            Assert.Equal("Member", memberType.Alias);
            Assert.Equal("Member", memberType.Name);
            Assert.Equal(DateTime.Parse("2019-06-17T13:40:01.46Z").ToUniversalTime(), memberType.CreateDate.ToUniversalTime());
            Assert.Equal(DateTime.Parse("2019-06-17T13:40:01.46Z").ToUniversalTime(), memberType.UpdateDate.ToUniversalTime());
            Assert.Equal(Guid.Parse("d59be02f-1df9-4228-aa1e-01917d806cda"), memberType.Id);
            Assert.Empty(memberType.Compositions);
            Assert.Collection(memberType.Groups,
                group =>
                {
                    Assert.Equal("Membership", group.Name);
                    Assert.Equal(1, group.SortOrder);
                    Assert.Collection(group.Properties, prop =>
                        {
                            Assert.Equal("umbracoMemberComments", prop.Alias);
                            Assert.False(prop.IsSensitive);
                            Assert.Equal("Comments", prop.Label);
                            Assert.False(prop.MemberCanEdit);
                            Assert.False(prop.MemberCanView);
                            Assert.Equal("Umbraco.TextArea", prop.PropertyEditorAlias);
                            Assert.Equal(0, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoMemberFailedPasswordAttempts", prop.Alias);
                            Assert.False(prop.IsSensitive);
                            Assert.Equal("Failed Password Attempts", prop.Label);
                            Assert.False(prop.MemberCanEdit);
                            Assert.False(prop.MemberCanView);
                            Assert.Equal("Umbraco.Label", prop.PropertyEditorAlias);
                            Assert.Equal(1, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoMemberApproved", prop.Alias);
                            Assert.False(prop.IsSensitive);
                            Assert.Equal("Is Approved", prop.Label);
                            Assert.False(prop.MemberCanEdit);
                            Assert.False(prop.MemberCanView);
                            Assert.Equal("Umbraco.TrueFalse", prop.PropertyEditorAlias);
                            Assert.Equal(2, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoMemberLockedOut", prop.Alias);
                            Assert.False(prop.IsSensitive);
                            Assert.Equal("Is Locked Out", prop.Label);
                            Assert.False(prop.MemberCanEdit);
                            Assert.False(prop.MemberCanView);
                            Assert.Equal("Umbraco.TrueFalse", prop.PropertyEditorAlias);
                            Assert.Equal(3, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoMemberLastLockoutDate", prop.Alias);
                            Assert.False(prop.IsSensitive);
                            Assert.Equal("Last Lockout Date", prop.Label);
                            Assert.False(prop.MemberCanEdit);
                            Assert.False(prop.MemberCanView);
                            Assert.Equal("Umbraco.Label", prop.PropertyEditorAlias);
                            Assert.Equal(4, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoMemberLastLogin", prop.Alias);
                            Assert.False(prop.IsSensitive);
                            Assert.Equal("Last Login Date", prop.Label);
                            Assert.False(prop.MemberCanEdit);
                            Assert.False(prop.MemberCanView);
                            Assert.Equal("Umbraco.Label", prop.PropertyEditorAlias);
                            Assert.Equal(5, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoMemberLastPasswordChangeDate", prop.Alias);
                            Assert.False(prop.IsSensitive);
                            Assert.Equal("Last Password Change Date", prop.Label);
                            Assert.False(prop.MemberCanEdit);
                            Assert.False(prop.MemberCanView);
                            Assert.Equal("Umbraco.Label", prop.PropertyEditorAlias);
                            Assert.Equal(6, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        }
                    );
                }
            );
        }

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }
    }
}
