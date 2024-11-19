namespace Framework.Application.Shared.Query
{
    public record DataResult<TResult> where TResult : class
    {
        public List<TResult> Data { get; set; } = [];
        public int Total { get; set; }
    }
}