using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Movies.Models
{
    [DataContract]
    public class MovieResponse<T>
    {
        [DataMember(Name = "results")]
        public IReadOnlyList<T> Results { get; private set; }

        [DataMember(Name = "total_results")]
        public int TotalResults { get; private set; }
    }
}
