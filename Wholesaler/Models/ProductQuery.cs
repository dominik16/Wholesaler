﻿namespace Wholesaler.DataTransferObject
{
    public class ProductQuery
    {
        public string SearchPhrase { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public SortDirection SortDirection { get; set; }
    }
}
