public class PagedResultDto<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int CurPage { get; set; }
    public int PageTotal { get; set; }
    public long ItemsTotal { get; set; }
    public int ItemsReceived { get; set; }
}