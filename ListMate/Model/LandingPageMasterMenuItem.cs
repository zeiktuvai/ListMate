using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListMate
{
    public class LandingPageMasterMenuItem
    {
        public LandingPageMasterMenuItem()
        {
            TargetType = typeof(LandingPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
        public Guid ListID { get; set; }
    }
}