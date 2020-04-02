using System;
using System.Collections.Generic;

namespace WeltenretterDemo.Models
{
    public partial class HeldAgressor
    {
        public int HeldagressorId { get; set; }
        public int HeldId { get; set; }
        public int AgressorId { get; set; }

        public Agressor Agressor { get; set; }
        public Held Held { get; set; }
    }
}
