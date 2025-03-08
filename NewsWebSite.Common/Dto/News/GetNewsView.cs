namespace NewsWebSite.Common.Dto.News
{
    public class GetNewsView
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string InsertTime { get; set; }
        public bool IsRemove { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ActiveTime { get; set; }
        public bool? Read { get; set; }
        public string? Reasons { get; set; }
        public Guid? DefaultImageId { get; set; }
        public long CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string ReporterName { get; set; }
        public string ReporterEmail { get; set; }
    }
}
