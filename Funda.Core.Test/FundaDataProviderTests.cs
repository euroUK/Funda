using Autofac.Extras.Moq;
using Funda.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Funda.Core.Models;
using Funda.Core.Test.Extensions;

namespace Funda.Core.Test
{
    /// <summary>
    /// For the FundaDataProvider just few basic tests provided. In real world solution we should cover more cases and possible exceptions
    /// </summary>
    [TestClass]
    public class FundaDataProviderTests
    {
        private AutoMock _mock = null!;
        private FundaDataProvider _subject = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mock = AutoMock.GetLoose();

            _mock.StubFundaOptions();

            _subject = _mock.Create<FundaDataProvider>();
        }

        [TestMethod]
        public async Task GetEstateProperties_FiltersOut_Same_Data()
        {
            var totalPages = 2;
            StubFundaDataProvider(totalPages, true);

            var data = await _subject.GetEstatePropertiesAsync(OfferType.Buy, "testTag");

            data.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GetEstateProperties_Returns_Distinct_Data()
        {
            var totalPages = 3;
            StubFundaDataProvider(totalPages);

            var data = await _subject.GetEstatePropertiesAsync(OfferType.Buy, "testTag");

            data.Should().HaveCount(totalPages);
        }

        private void StubFundaDataProvider(int pageCount, bool sameData = false)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            for (var i = 1; i <= pageCount; i++)
            {
                var iCopy = i; //copy should be used in lambda otherwise last value is always used
                var url = $"&page={iCopy}&";

                handlerMock.Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.Is<HttpRequestMessage>(
                            x => x.RequestUri!.Query.Contains(url)
                        ),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(
                        () => new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(
                                FakeJsonProvider.GetFakeJsonResponse(pageCount, iCopy, sameData)
                            )
                        }
                    )
                    .Verifiable();
            }

            _mock.Mock<IHttpClientFactory>()
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(handlerMock.Object));
        }
    }
}
