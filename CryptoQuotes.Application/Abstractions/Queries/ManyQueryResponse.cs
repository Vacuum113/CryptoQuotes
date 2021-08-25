using System.Collections.Generic;

namespace Application.Abstractions.Queries
{
    public class GetManyResponse<TResponse>
    {
        public GetManyResponse(int count, List<TResponse> entities)
        {
            Count = count;
            Entities = entities;
        }

        public int Count { get; set; }
        public IEnumerable<TResponse> Entities { get; set; }
    }
}