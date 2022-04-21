using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Autofac.Extras.Moq;
using Funda.Core.ApiModels;
using Funda.Core.Models;
using Funda.Core.Options;
using Funda.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Moq;

namespace Funda.Core.Test.Extensions
{
    public static class AutoMockExtensions
    {
        public static void StubContext<T>(
            this AutoMock mock,
            Expression<Func<PropertyDbContext, DbSet<T>>> getter,
            List<T>? data = null)
            where T : class
        {
            data ??= new List<T>();

            var queryable = data.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet
                .Setup(x => x.Add(It.IsAny<T>()))
                .Returns(
                    (T item) =>
                    {
                        data.Add(item);
                        return (item as EntityEntry<T>)!;
                    }
                );

            mock.Mock<PropertyDbContext>()
                .Setup(getter)
                .Returns(mockSet.Object);
        }

        public static void StubFundaDataProvider(this AutoMock mock, List<EstatePropertyModel> data)
        {
            mock.Mock<IFundaDataProvider>()
                .Setup(
                    x => x.GetEstatePropertiesAsync(
                        It.IsAny<OfferType>(),
                        It.IsAny<string>(),
                        It.IsAny<DateTime?>(),
                        It.IsAny<CancellationToken>()

                    )
                )
                .ReturnsAsync(data);
        }

        public static void StubFundaOptions(this AutoMock mock, FundaOptions? data = null)
        {
            mock.Mock<IOptions<FundaOptions>>()
                .Setup(x => x.Value)
                .Returns(
                    data ??
                    new FundaOptions()
                    {
                        ApiEndpoint = "http://localhost",
                        ApiKey = "key",
                        PageSize = 25,
                        RateInterval = 60,
                        RateLimit = 100
                    }
                );
        }
    }
}