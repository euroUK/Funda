using System.ComponentModel.DataAnnotations;

namespace Funda.Core.Models
{
    public class EstateProperty
    {
        public Guid Id { get; set; }
        public int GlobalId { get; set; }
        public byte? RoomsNumber { get; set; }
        public byte LotsNumber { get; set; }

        [MaxLength(30)]
        public string? Status { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(10)]
        public string? PostCode { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        public double? RentalPrice { get; set; }
        public double? BuyPrice { get; set; }
        public DateTimeOffset PublicationDate { get; set; }

        [MaxLength(100)]
        public string Url { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? LivingArea { get; set; }
        public string PropertyType { get; set; }

        [MaxLength(50)]
        public OfferType OfferType { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }

        public List<PropertyTag> Tags { get; set; } = new List<PropertyTag>();
    }
}