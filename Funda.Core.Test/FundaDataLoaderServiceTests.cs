using Funda.Core.Models;
using Funda.Core.Services;
using Funda.Core.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using FluentAssertions;
using Funda.Core.ApiModels;

namespace Funda.Core.Test
{
    [TestClass]
    public class FundaDataLoaderServiceTests
    {
        private AutoMock _mock = null!;
        private FundaDataLoaderService _subject = null!;

        [TestInitialize]
        public void Init()
        {
            _mock = AutoMock.GetLoose();
            _subject = _mock.Create<FundaDataLoaderService>();

            _mock.StubContext(
                x => x.TagTypes,
                new List<TagType>
                {
                    CreateCityTagType(1, "city1"),
                    CreateFilterTag(2, "filter2"),
                }
            );
            _mock.StubContext(x => x.Agents);
            _mock.StubContext(x => x.EstateProperties);
        }

        [TestMethod]
        public async Task LoadData_TwoEstatePropertiesWithOneAgent_SavesAgent()
        {
            var model1 = CreateEstatePropertyModel(estatePropertyId: 1, agentId: 1);
            var model2 = CreateEstatePropertyModel(estatePropertyId: 2, agentId: 1);
            _mock.StubFundaDataProvider(new List<EstatePropertyModel> { model1, model2 });

            await _subject.LoadData(OfferType.Buy, null).AllResultsAsync();

            _mock.Mock<PropertyDbContext>().Object.Agents.ToList()
                .Should()
                .HaveCount(1)
                .And
                .Contain(x => x.AgentId == model1.MakelaarId && x.AgentName == model1.MakelaarNaam);
        }

        [TestMethod]
        public async Task LoadData_TwoEstatePropertiesWithTwoAgents_SavesAgents()
        {
            var model1 = CreateEstatePropertyModel(estatePropertyId: 1, agentId: 1);
            var model2 = CreateEstatePropertyModel(estatePropertyId: 2, agentId: 2);
            _mock.StubFundaDataProvider(new List<EstatePropertyModel> { model1, model2 });

            await _subject.LoadData(OfferType.Buy, null).AllResultsAsync();

            _mock.Mock<PropertyDbContext>().Object.Agents.ToList()
                .Should()
                .HaveCount(2)
                .And
                .Contain(x => x.AgentId == model1.MakelaarId && x.AgentName == model1.MakelaarNaam)
                .And
                .Contain(x => x.AgentId == model2.MakelaarId && x.AgentName == model2.MakelaarNaam);
        }

        [TestMethod]
        public async Task LoadData_OneEstateProperty_SavesEstateProperty()
        {
            var model = CreateEstatePropertyModel(estatePropertyId: 1, agentId: 1);
            _mock.StubFundaDataProvider(new List<EstatePropertyModel> { model });

            await _subject.LoadData(OfferType.Buy, null).AllResultsAsync();

            _mock.Mock<PropertyDbContext>().Object.EstateProperties.ToList()
                .Should()
                .HaveCount(1)
                .And
                .Contain(x => x.GlobalId == model.GlobalId);
        }

        [TestMethod]
        public async Task LoadData_OneEstatePropertyWithTwoFilterTags_SavesTags()
        {
            var model = CreateEstatePropertyModel(estatePropertyId: 1, agentId: 1);
            _mock.StubFundaDataProvider(new List<EstatePropertyModel> { model });

            var tagType1 = CreateFilterTag(1, "tag1");
            var tagType2 = CreateFilterTag(2, "tag2");

            _mock.StubContext(
                x => x.TagTypes,
                new List<TagType>
                {
                    CreateCityTagType(100, "Amsterdam"),
                    tagType1,
                    tagType2
                }
            );

            await _subject.LoadData(OfferType.Buy, null).AllResultsAsync();

            _mock.Mock<PropertyDbContext>().Object.EstateProperties
                .Single(x => x.GlobalId == model.GlobalId)
                .Tags
                .Should()
                .HaveCount(2)
                .And
                .Contain(x => x.TagType.Id == tagType1.Id && x.TagType.Code == tagType1.Code)
                .And
                .Contain(x => x.TagType.Id == tagType2.Id && x.TagType.Code == tagType2.Code);
        }

        private static TagType CreateCityTagType(int id, string city)
        {
            return new TagType
            {
                Id = id,
                ParentTagId = -1,
                Name = city,
                Code = city
            };
        }

        private static TagType CreateFilterTag(int id, string filter)
        {
            return new TagType
            {
                Id = id,
                ParentTagId = 1,
                Name = filter,
                Code = filter
            };
        }

        private static EstatePropertyModel CreateEstatePropertyModel(int estatePropertyId, int agentId)
        {
            return new EstatePropertyModel
            {
                GlobalId = estatePropertyId,
                MakelaarId = agentId,
                MakelaarNaam = $"Makelaar{agentId}",
                ZoekType = new List<int> { 10 }
            };
        }
    }
}
