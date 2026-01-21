namespace backend.vo
{
    public class QueryParams
    {
        public string? Filter { get; set; }
        public string? Sort { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }

}
