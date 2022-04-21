using Funda.Core.Models;
using System.Text.Json.Serialization;

namespace Funda.Core.ApiModels
{
    public class Metadata
    {
        [JsonPropertyName("ObjectType")]
        public string ObjectType { get; set; }

        [JsonPropertyName("Omschrijving")]
        public string Omschrijving { get; set; }

        [JsonPropertyName("Titel")]
        public string Titel { get; set; }
    }

    public class Prijs
    {
        [JsonPropertyName("GeenExtraKosten")]
        public bool GeenExtraKosten { get; set; }

        [JsonPropertyName("HuurAbbreviation")]
        public string HuurAbbreviation { get; set; }

        [JsonPropertyName("Huurprijs")]
        public int? Huurprijs { get; set; }

        [JsonPropertyName("HuurprijsOpAanvraag")]
        public string HuurprijsOpAanvraag { get; set; }

        [JsonPropertyName("HuurprijsTot")]
        public int? HuurprijsTot { get; set; }

        [JsonPropertyName("KoopAbbreviation")]
        public string KoopAbbreviation { get; set; }

        [JsonPropertyName("Koopprijs")]
        public object Koopprijs { get; set; }

        [JsonPropertyName("KoopprijsOpAanvraag")]
        public string KoopprijsOpAanvraag { get; set; }

        [JsonPropertyName("KoopprijsTot")]
        public object KoopprijsTot { get; set; }

        [JsonPropertyName("OriginelePrijs")]
        public object OriginelePrijs { get; set; }

        [JsonPropertyName("VeilingText")]
        public string VeilingText { get; set; }
    }

    public class PromoLabel
    {
        [JsonPropertyName("HasPromotionLabel")]
        public bool HasPromotionLabel { get; set; }

        [JsonPropertyName("PromotionPhotos")]
        public List<object> PromotionPhotos { get; set; }

        [JsonPropertyName("PromotionPhotosSecure")]
        public object PromotionPhotosSecure { get; set; }

        [JsonPropertyName("PromotionType")]
        public int PromotionType { get; set; }

        [JsonPropertyName("RibbonColor")]
        public int RibbonColor { get; set; }

        [JsonPropertyName("RibbonText")]
        public object RibbonText { get; set; }

        [JsonPropertyName("Tagline")]
        public object Tagline { get; set; }
    }

    public class Project
    {
        [JsonPropertyName("AantalKamersTotEnMet")]
        public object AantalKamersTotEnMet { get; set; }

        [JsonPropertyName("AantalKamersVan")]
        public object AantalKamersVan { get; set; }

        [JsonPropertyName("AantalKavels")]
        public object AantalKavels { get; set; }

        [JsonPropertyName("Adres")]
        public object Adres { get; set; }

        [JsonPropertyName("FriendlyUrl")]
        public object FriendlyUrl { get; set; }

        [JsonPropertyName("GewijzigdDatum")]
        public object GewijzigdDatum { get; set; }

        [JsonPropertyName("GlobalId")]
        public int? GlobalId { get; set; }

        [JsonPropertyName("HoofdFoto")]
        public string HoofdFoto { get; set; }

        [JsonPropertyName("IndIpix")]
        public bool IndIpix { get; set; }

        [JsonPropertyName("IndPDF")]
        public bool IndPDF { get; set; }

        [JsonPropertyName("IndPlattegrond")]
        public bool IndPlattegrond { get; set; }

        [JsonPropertyName("IndTop")]
        public bool IndTop { get; set; }

        [JsonPropertyName("IndVideo")]
        public bool IndVideo { get; set; }

        [JsonPropertyName("InternalId")]
        public string InternalId { get; set; }

        [JsonPropertyName("MaxWoonoppervlakte")]
        public object MaxWoonoppervlakte { get; set; }

        [JsonPropertyName("MinWoonoppervlakte")]
        public object MinWoonoppervlakte { get; set; }

        [JsonPropertyName("Naam")]
        public string Naam { get; set; }

        [JsonPropertyName("Omschrijving")]
        public object Omschrijving { get; set; }

        [JsonPropertyName("OpenHuizen")]
        public List<object> OpenHuizen { get; set; }

        [JsonPropertyName("Plaats")]
        public object Plaats { get; set; }

        [JsonPropertyName("Prijs")]
        public object Prijs { get; set; }

        [JsonPropertyName("PrijsGeformatteerd")]
        public object PrijsGeformatteerd { get; set; }

        [JsonPropertyName("PublicatieDatum")]
        public object PublicatieDatum { get; set; }

        [JsonPropertyName("Type")]
        public int Type { get; set; }

        [JsonPropertyName("Woningtypen")]
        public object Woningtypen { get; set; }
    }

    public class EstatePropertyModel
    {
        [JsonPropertyName("AangebodenSindsTekst")]
        public string AangebodenSindsTekst { get; set; }

        [JsonPropertyName("AanmeldDatum")]
        public DateTimeOffset AanmeldDatum { get; set; }

        [JsonPropertyName("AantalBeschikbaar")]
        public object AantalBeschikbaar { get; set; }

        [JsonPropertyName("AantalKamers")]
        public int? AantalKamers { get; set; }

        [JsonPropertyName("AantalKavels")]
        public object AantalKavels { get; set; }

        [JsonPropertyName("Aanvaarding")]
        public string Aanvaarding { get; set; }

        [JsonPropertyName("Adres")]
        public string Adres { get; set; }

        [JsonPropertyName("Afstand")]
        public int Afstand { get; set; }

        [JsonPropertyName("BronCode")]
        public string BronCode { get; set; }

        [JsonPropertyName("ChildrenObjects")]
        public List<EstatePropertyModel> ChildrenObjects { get; set; }

        [JsonPropertyName("DatumAanvaarding")]
        public string DatumAanvaarding { get; set; }

        [JsonPropertyName("DatumOndertekeningAkte")]
        public object DatumOndertekeningAkte { get; set; }

        [JsonPropertyName("Foto")]
        public string Foto { get; set; }

        [JsonPropertyName("FotoLarge")]
        public string FotoLarge { get; set; }

        [JsonPropertyName("FotoLargest")]
        public string FotoLargest { get; set; }

        [JsonPropertyName("FotoMedium")]
        public string FotoMedium { get; set; }

        [JsonPropertyName("FotoSecure")]
        public string FotoSecure { get; set; }

        [JsonPropertyName("GewijzigdDatum")]
        public object GewijzigdDatum { get; set; }

        [JsonPropertyName("GlobalId")]
        public int GlobalId { get; set; }

        [JsonPropertyName("GroupByObjectType")]
        public string GroupByObjectType { get; set; }

        [JsonPropertyName("Heeft360GradenFoto")]
        public bool Heeft360GradenFoto { get; set; }

        [JsonPropertyName("HeeftBrochure")]
        public bool HeeftBrochure { get; set; }

        [JsonPropertyName("HeeftOpenhuizenTopper")]
        public bool HeeftOpenhuizenTopper { get; set; }

        [JsonPropertyName("HeeftOverbruggingsgrarantie")]
        public bool HeeftOverbruggingsgrarantie { get; set; }

        [JsonPropertyName("HeeftPlattegrond")]
        public bool HeeftPlattegrond { get; set; }

        [JsonPropertyName("HeeftTophuis")]
        public bool HeeftTophuis { get; set; }

        [JsonPropertyName("HeeftVeiling")]
        public bool HeeftVeiling { get; set; }

        [JsonPropertyName("HeeftVideo")]
        public bool HeeftVideo { get; set; }

        [JsonPropertyName("HuurPrijsTot")]
        public int? HuurPrijsTot { get; set; }

        [JsonPropertyName("Huurprijs")]
        public int? Huurprijs { get; set; }

        [JsonPropertyName("HuurprijsFormaat")]
        public string HuurprijsFormaat { get; set; }

        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("InUnitsVanaf")]
        public object InUnitsVanaf { get; set; }

        [JsonPropertyName("IndProjectObjectType")]
        public bool IndProjectObjectType { get; set; }

        [JsonPropertyName("IndTransactieMakelaarTonen")]
        public object IndTransactieMakelaarTonen { get; set; }

        [JsonPropertyName("IsSearchable")]
        public bool IsSearchable { get; set; }

        [JsonPropertyName("IsVerhuurd")]
        public bool IsVerhuurd { get; set; }

        [JsonPropertyName("IsVerkocht")]
        public bool IsVerkocht { get; set; }

        [JsonPropertyName("IsVerkochtOfVerhuurd")]
        public bool IsVerkochtOfVerhuurd { get; set; }

        [JsonPropertyName("Koopprijs")]
        public int? Koopprijs { get; set; }

        [JsonPropertyName("KoopprijsFormaat")]
        public string KoopprijsFormaat { get; set; }

        [JsonPropertyName("KoopprijsTot")]
        public int? KoopprijsTot { get; set; }

        [JsonPropertyName("Land")]
        public object Land { get; set; }

        [JsonPropertyName("MakelaarId")]
        public int MakelaarId { get; set; }

        [JsonPropertyName("MakelaarNaam")]
        public string MakelaarNaam { get; set; }

        [JsonPropertyName("MobileURL")]
        public string MobileURL { get; set; }

        [JsonPropertyName("Note")]
        public object Note { get; set; }

        [JsonPropertyName("OpenHuis")]
        public List<DateTimeOffset> OpenHuis { get; set; }

        [JsonPropertyName("Oppervlakte")]
        public int Oppervlakte { get; set; }

        [JsonPropertyName("Perceeloppervlakte")]
        public int? Perceeloppervlakte { get; set; }

        [JsonPropertyName("Postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("Prijs")]
        public Prijs Prijs { get; set; }

        [JsonPropertyName("PrijsGeformatteerdHtml")]
        public string PrijsGeformatteerdHtml { get; set; }

        [JsonPropertyName("PrijsGeformatteerdTextHuur")]
        public string PrijsGeformatteerdTextHuur { get; set; }

        [JsonPropertyName("PrijsGeformatteerdTextKoop")]
        public string PrijsGeformatteerdTextKoop { get; set; }

        [JsonPropertyName("Producten")]
        public List<string> Producten { get; set; }

        [JsonPropertyName("Project")]
        public Project Project { get; set; }

        [JsonPropertyName("ProjectNaam")]
        public string ProjectNaam { get; set; }

        [JsonPropertyName("PromoLabel")]
        public PromoLabel PromoLabel { get; set; }

        [JsonPropertyName("PublicatieDatum")]
        public DateTimeOffset PublicatieDatum { get; set; }

        [JsonPropertyName("PublicatieStatus")]
        public int PublicatieStatus { get; set; }

        [JsonPropertyName("SavedDate")]
        public object SavedDate { get; set; }

        [JsonPropertyName("Soort-aanbod")]
        public string SoortAanbod { get; set; }

        [JsonPropertyName("SoortAanbod")]
        public int SoortAanbodInt { get; set; }

        [JsonPropertyName("StartOplevering")]
        public object StartOplevering { get; set; }

        [JsonPropertyName("TimeAgoText")]
        public object TimeAgoText { get; set; }

        [JsonPropertyName("TransactieAfmeldDatum")]
        public object TransactieAfmeldDatum { get; set; }

        [JsonPropertyName("TransactieMakelaarId")]
        public object TransactieMakelaarId { get; set; }

        [JsonPropertyName("TransactieMakelaarNaam")]
        public object TransactieMakelaarNaam { get; set; }

        [JsonPropertyName("TypeProject")]
        public int TypeProject { get; set; }

        [JsonPropertyName("URL")]
        public string URL { get; set; }

        [JsonPropertyName("VerkoopStatus")]
        public string VerkoopStatus { get; set; }

        [JsonPropertyName("WGS84_X")]
        public double WGS84X { get; set; }

        [JsonPropertyName("WGS84_Y")]
        public double WGS84Y { get; set; }

        [JsonPropertyName("WoonOppervlakteTot")]
        public int? WoonOppervlakteTot { get; set; }

        [JsonPropertyName("Woonoppervlakte")]
        public int? Woonoppervlakte { get; set; }

        [JsonPropertyName("Woonplaats")]
        public string Woonplaats { get; set; }

        [JsonPropertyName("ZoekType")]
        public List<int> ZoekType { get; set; }

        public EstateProperty ToEstateProperty()
        {
            return new EstateProperty()
            {
                Address = Adres,
                BuyPrice = Koopprijs,
                City = Woonplaats,
                GlobalId = GlobalId,
                Latitude = WGS84X,
                Longitude = WGS84Y,
                LivingArea = Woonoppervlakte,
                RoomsNumber = (byte?)AantalKamers,
                Url = URL,
                Status = Aanvaarding,
                RentalPrice = Huurprijs,
                PublicationDate = PublicatieDatum,
                OfferType = (OfferType)ZoekType.Sum(),
                PropertyType = SoortAanbod,
                PostCode = Postcode,
                AgentId = MakelaarId
            };
        }
    }

    public class Paging
    {
        [JsonPropertyName("AantalPaginas")]
        public int AantalPaginas { get; set; }

        [JsonPropertyName("HuidigePagina")]
        public int HuidigePagina { get; set; }

        [JsonPropertyName("VolgendeUrl")]
        public string VolgendeUrl { get; set; }

        [JsonPropertyName("VorigeUrl")]
        public object VorigeUrl { get; set; }
    }

    public class FundaResponse
    {
        [JsonPropertyName("AccountStatus")]
        public int AccountStatus { get; set; }

        [JsonPropertyName("EmailNotConfirmed")]
        public bool EmailNotConfirmed { get; set; }

        [JsonPropertyName("ValidationFailed")]
        public bool ValidationFailed { get; set; }

        [JsonPropertyName("ValidationReport")]
        public object ValidationReport { get; set; }

        [JsonPropertyName("Website")]
        public int Website { get; set; }

        [JsonPropertyName("Metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("Objects")]
        public List<EstatePropertyModel> Objects { get; set; }

        [JsonPropertyName("Paging")]
        public Paging Paging { get; set; }

        [JsonPropertyName("TotaalAantalObjecten")]
        public int TotaalAantalObjecten { get; set; }
    }
}