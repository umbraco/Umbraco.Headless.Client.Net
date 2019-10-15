using System;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Management.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management
{
    public class RelationServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public RelationServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task Create_ReturnsCreatedRelation()
        {
            var service = new RelationService(_configuration,
                GetMockedHttpClient("/relation", RelationServiceJson.Create));

            var relation = await service.Create(new Relation
            {
                ChildId = new Guid("04138156-574e-4bb7-a27a-0ebafff5e83b"),
                Comment = "my comment",
                ParentId = new Guid("9f717c5b-ec3a-43f6-ac11-523ca3114dc9"),
                RelationTypeAlias = "relateParentDocumentOnDelete"
            });

            Assert.NotNull(relation);
            Assert.Equal(new Guid("04138156-574e-4bb7-a27a-0ebafff5e83b"), relation.ChildId);
            Assert.Equal("my comment", relation.Comment);
            Assert.Equal(34, relation.Id);
            Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), relation.ParentId);
            Assert.Equal("relateParentDocumentOnDelete", relation.RelationTypeAlias);
        }

        [Fact]
        public async Task Delete_ReturnsDeletedRelation()
        {
            var service = new RelationService(_configuration,
                GetMockedHttpClient("/relation/34", RelationServiceJson.Create));

            var relation = await service.Delete(34);

            Assert.NotNull(relation);
            Assert.Equal(new Guid("04138156-574e-4bb7-a27a-0ebafff5e83b"), relation.ChildId);
            Assert.Equal("my comment", relation.Comment);
            Assert.Equal(34, relation.Id);
            Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), relation.ParentId);
            Assert.Equal("relateParentDocumentOnDelete", relation.RelationTypeAlias);
        }

        [Fact]
        public async Task GetByAlias_ReturnsRelations()
        {
            var service = new RelationService(_configuration,
                GetMockedHttpClient("/relation/relateDocumentOnCopy", RelationServiceJson.ByAlias));

            var relations = await service.GetByAlias("relateDocumentOnCopy");

            Assert.NotNull(relations);
            Assert.Collection(relations,
                r =>
                {
                    Assert.Empty(r.Comment);
                    Assert.Equal(new Guid("9f717c5b-ec3a-43f6-ac11-523ca3114dc9"), r.ChildId);
                    Assert.Equal(32, r.Id);
                    Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), r.ParentId);
                    Assert.Equal("relateParentDocumentOnDelete", r.RelationTypeAlias);
                }, r =>
                {
                    Assert.Empty(r.Comment);
                    Assert.Equal(new Guid("22fbcdc1-79c6-47d0-a082-540e6fd8c5d0"), r.ChildId);
                    Assert.Equal(33, r.Id);
                    Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), r.ParentId);
                    Assert.Equal("relateParentDocumentOnDelete", r.RelationTypeAlias);
                }, r =>
                {
                    Assert.Empty(r.Comment);
                    Assert.Equal(new Guid("0cbf0bc6-741b-45bb-89a4-336572a84b6c"), r.ChildId);
                    Assert.Equal(34, r.Id);
                    Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), r.ParentId);
                    Assert.Equal("relateParentDocumentOnDelete", r.RelationTypeAlias);
                }
            );
        }

        [Fact]
        public async Task GetChildId_ReturnsRelations()
        {
            var service = new RelationService(_configuration,
                GetMockedHttpClient("/relation/child/9f717c5b-ec3a-43f6-ac11-523ca3114dc9", RelationServiceJson.ByChildId));

            var relations = await service.GetByChildId(new Guid("9f717c5b-ec3a-43f6-ac11-523ca3114dc9"));

            Assert.NotNull(relations);
            Assert.Collection(relations,
                r =>
                {
                    Assert.Empty(r.Comment);
                    Assert.Equal(new Guid("9f717c5b-ec3a-43f6-ac11-523ca3114dc9"), r.ChildId);
                    Assert.Equal(32, r.Id);
                    Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), r.ParentId);
                    Assert.Equal("relateParentDocumentOnDelete", r.RelationTypeAlias);
                }
            );
        }

        [Fact]
        public async Task GetById_ReturnsRelations()
        {
            var service = new RelationService(_configuration,
                GetMockedHttpClient("/relation/32", RelationServiceJson.ById));

            var relation = await service.GetById(32);

            Assert.NotNull(relation);
            Assert.NotNull(relation);
            Assert.Empty(relation.Comment);
            Assert.Equal(new Guid("9f717c5b-ec3a-43f6-ac11-523ca3114dc9"), relation.ChildId);
            Assert.Equal(32, relation.Id);
            Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), relation.ParentId);
            Assert.Equal("relateParentDocumentOnDelete", relation.RelationTypeAlias);
        }

        [Fact]
         public async Task GetByParentId_ReturnsRelations()
         {
             var service = new RelationService(_configuration,
                 GetMockedHttpClient("/relation/parent/916724a5-173d-4619-b97e-b9de133dd6f5", RelationServiceJson.ByParentId));

             var relations = await service.ByParentId(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"));

             Assert.NotNull(relations);
             Assert.Collection(relations,
                 r =>
                 {
                     Assert.Empty(r.Comment);
                     Assert.Equal(new Guid("9f717c5b-ec3a-43f6-ac11-523ca3114dc9"), r.ChildId);
                     Assert.Equal(32, r.Id);
                     Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), r.ParentId);
                     Assert.Equal("relateParentDocumentOnDelete", r.RelationTypeAlias);
                 }, r =>
                 {
                     Assert.Empty(r.Comment);
                     Assert.Equal(new Guid("22fbcdc1-79c6-47d0-a082-540e6fd8c5d0"), r.ChildId);
                     Assert.Equal(33, r.Id);
                     Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), r.ParentId);
                     Assert.Equal("relateParentDocumentOnDelete", r.RelationTypeAlias);
                 }, r =>
                 {
                     Assert.Empty(r.Comment);
                     Assert.Equal(new Guid("0cbf0bc6-741b-45bb-89a4-336572a84b6c"), r.ChildId);
                     Assert.Equal(34, r.Id);
                     Assert.Equal(new Guid("916724a5-173d-4619-b97e-b9de133dd6f5"), r.ParentId);
                     Assert.Equal("relateParentDocumentOnDelete", r.RelationTypeAlias);
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
