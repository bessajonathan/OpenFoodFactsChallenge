using System.Collections.Generic;

namespace OpenFoodFacts.Application.Infra
{
    public class ListViewModel<T>
    {
        public T Data { get; set; }
        public bool HasNextPage { get; set; }
        public int TotalItemCount { get; set; }
    }
}
