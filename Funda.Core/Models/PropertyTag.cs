namespace Funda.Core.Models
{
    /// <summary>
    /// Cross table used for many-to-many relation "Each real estate property could have several tags. Each tag can be taged by several properties"
    /// </summary>
    public class PropertyTag
    {
        public int Id { get; set; }
        public EstateProperty Property { get; set; }
        public TagType TagType { get; set; }
    }
}