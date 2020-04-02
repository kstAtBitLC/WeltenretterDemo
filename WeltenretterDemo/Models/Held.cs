using System;
using System.Collections.Generic;

namespace WeltenretterDemo.Models
{
    public partial class Held
    {
        public Held()
        {
            HeldAgressor = new HashSet<HeldAgressor>();
        }

        public int HeldId { get; set; }
        public string Heldname { get; set; }

        public ICollection<HeldAgressor> HeldAgressor { get; set; }
    }
}
