namespace ProvaPub.Models
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; } = new();
        public bool HasNext { get; set; }
        public int TotalCount { get; set; }
    }
}
