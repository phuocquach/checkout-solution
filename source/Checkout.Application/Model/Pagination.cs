namespace Checkout.Application.Model
{
    public class Pagination<T>
    {
        public Pagination()
        {

        }

        public Pagination(IEnumerable<T> results, PagedRequest request, int totalResults)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalResults / (decimal)request.PerPage);
            this.Results = results;
            this.ResultParams = new ResultParams
            {
                Page = request.Page,
                PerPage = request.PerPage,
                TotalPages = totalPages,
                TotalResults = totalResults,
                OrderBy = request.OrderBy,
                OrderByDirection = request.OrderByDirection,
                ReachedBottom = totalPages <= request.Page
            };
        }

        public Pagination(IEnumerable<T> results, PagedRequest request, int totalResults, bool reachedBottom)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalResults / (decimal)request.PerPage);
            this.Results = results;
            this.ResultParams = new ResultParams
            {
                Page = request.Page,
                PerPage = request.PerPage,
                TotalPages = totalPages,
                TotalResults = totalResults,
                OrderBy = request.OrderBy,
                OrderByDirection = request.OrderByDirection,
                ReachedBottom = reachedBottom
            };
        }

        public IEnumerable<T> Results { get; set; }

        public ResultParams ResultParams { get; set; }
    }
    public class ResultParams
    {
        public int? Page { get; set; }

        public int? TotalResults { get; set; }

        public int? TotalPages { get; set; }

        public int? PerPage { get; set; }

        public string OrderBy { get; set; }

        public string OrderByDirection { get; set; }

        public bool? ReachedBottom { get; set; }

        public long? LastCreationTime { get; set; }

        public Guid? LastItemGuid { get; set; }
    }
}
