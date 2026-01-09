namespace backend.vo
{
    public class PageVO<T>
    {
        public int Page { get; set; }            // Trang hiện tại (ví dụ: 1)
        public int Size { get; set; }            // Kích thước trang (ví dụ: 10)
        public long TotalElements { get; set; }  // Tổng số bản ghi trong DB
        public int TotalPages { get; set; }      // Tổng số trang
        public int NumberOfElements { get; set; } // Số bản ghi thực tế ở trang hiện tại
        public List<T> Content { get; set; }     // Dữ liệu

        public PageVO(int page, int size, long totalElements, List<T> content)
        {
            Page = page;
            Size = size;
            TotalElements = totalElements;
            Content = content;
            NumberOfElements = content.Count;
            TotalPages = (int)Math.Ceiling((double)totalElements / size);
        }
    }
}
