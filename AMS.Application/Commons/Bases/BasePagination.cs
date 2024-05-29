namespace AMS.Application.Commons.Bases
{
    public abstract class BasePagination
    {
        private readonly int NumMaxRecordsPage = 50;
        public int NumPage { get; set; } = 1;
        private int NumRecordsPage { get; set; } = 10;
        public string Order { get; set; } = "asc";
        public string? Sort { get; set; }


        public int Records
        {
            get => NumRecordsPage;
            set
            {
                NumRecordsPage = (value > NumMaxRecordsPage) ? NumMaxRecordsPage : value;
            }
        }
    }
}
