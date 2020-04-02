using System;
using System.Collections.Generic;

namespace WeltenretterDemo.Models
{
    public partial class Agressor
    {
        public Agressor()
        {
            HeldAgressor = new HashSet<HeldAgressor>();
        }

        public int AgressorId { get; set; }
        public string Agressorname { get; set; }

        public ICollection<HeldAgressor> HeldAgressor { get; set; }
    }
}
