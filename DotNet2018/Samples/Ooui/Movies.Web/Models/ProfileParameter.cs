using Movies.Web.Models.People;
using System.Collections.Generic;

namespace Movies.Web.Models
{
    public class ProfileParameter
    {
        public Profile Current { get; set; }

        public List<Profile> Images { get; set; }
    }
}