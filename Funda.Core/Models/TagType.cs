namespace Funda.Core.Models
{
    public class TagType
    {
        public int Id { get; set; }
        public int? ParentTagId { get; set; }
        public TagType ParentTag { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<PropertyTag> Tags { get; set; }
        public List<TagType> ChildTags { get; set; }
    }
}