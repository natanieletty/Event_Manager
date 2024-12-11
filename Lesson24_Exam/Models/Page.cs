using Lesson24_Exam.Interfaces;

namespace Lesson24_Exam.Models
{
    public class Page<T>
    {
        public IFilter<T> Filter { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 0;
        public IEnumerable<T> PageContent { get => Filter?.Filtered.Skip(PageIndex * PageSize).Take(PageSize).AsEnumerable() ?? []; }
        public int PageNumber { get => PageIndex + 1; }
        public int PageItems { get => PageContent?.Count() ?? 0; }
        public int TotalItems { get => Filter?.Filtered.Count() ?? 0; }
        public int TotalPages { get => Filter?.Filtered.Count() / PageSize + ((Filter?.Filtered.Count() % PageSize > 0) ? 1 : 0) ?? 0; }
        public bool HasNext { get => PageNumber < TotalPages; }
        public bool HasPrevious { get => PageIndex > 0; }
    }
}
