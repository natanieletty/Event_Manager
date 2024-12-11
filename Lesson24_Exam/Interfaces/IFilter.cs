using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson24_Exam.Interfaces
{
    public interface IFilter<T>
    {
        public IQueryable<T> Original { get; set; }
        public IQueryable<T> Filtered { get; }
        public bool IsActive { get; }
    }
}
