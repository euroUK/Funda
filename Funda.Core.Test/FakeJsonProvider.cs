using Funda.Core.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Funda.Core.Test
{
    /// <summary>
    /// Class is used in Http request interceptor to feed data
    /// </summary>
    public static class FakeJsonProvider
    {
        private static JsonSerializerOptions GetSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                Converters = { new Funda.Core.Convertors.UnixEpochOffsetConverter(), new Funda.Core.Convertors.UnixEpochOffsetArrayConverter() },
            };
        }
        public static string GetFakeJsonResponse(int pageCount, int currentPage, bool sameObject = false)
        {
            FundaResponse response = new FundaResponse() {
                AccountStatus = 0,
                EmailNotConfirmed = false,
                ValidationFailed = false,
                Website = 0,
                Metadata = new Metadata()
                {
                    ObjectType = "Koopwoningen",
                    Omschrijving = "Koopwoningen > Amsterdam | Tuin",
                    Titel = "Huizen te koop in Amsterdam"
                },
                Paging = new Paging()
                {
                    AantalPaginas = pageCount,
                    HuidigePagina = currentPage,
                    VolgendeUrl = $"/~/koop/amsterdam/tuin/publicatiedatum-na/p{currentPage+1}/"
                },
                TotaalAantalObjecten = pageCount,
                Objects = new List<ApiModels.EstatePropertyModel>()
            };

            int seed = 1;
            if (!sameObject)
            {
                seed = currentPage;
            }

            EstatePropertyModel obj = new EstatePropertyModel()
            {
                AangebodenSindsTekst = "Vandaag",
                AanmeldDatum = DateTimeOffset.Now,
                AantalKamers = currentPage,
                Aanvaarding = "InOverleg",
                Adres = "Adres",
                Afstand = 0,
                BronCode = "NVM",
                ChildrenObjects = new List<EstatePropertyModel>(),
                GlobalId = seed,
                MakelaarId = seed,
                MakelaarNaam = $"Makelaar {seed}",
                Postcode = "1035JR",
                Prijs = new Prijs()
                {
                    Koopprijs = 645000,
                    KoopprijsTot = 645000,
                    Huurprijs = 2500,
                    HuurprijsTot = 2500
                },
                PublicatieDatum = DateTimeOffset.Now.Date,
                VerkoopStatus = "StatusBeschikbaar",
                WGS84X = 4.891949,
                WGS84Y = 52.42381,
                WoonOppervlakteTot = 150,
                Woonoppervlakte = 150,
                Woonplaats = "Amsterdam",
                ZoekType = new List<int>() { 10 }
            };
            response.Objects.Add(obj);

            return JsonSerializer.Serialize(response, GetSerializerOptions());
        }
    }
}
