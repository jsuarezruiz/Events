using System.Runtime.Serialization;

namespace Movies.Web.Models.TVShow
{
    [DataContract]
    public class Network 
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}