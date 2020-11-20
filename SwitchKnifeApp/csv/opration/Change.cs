using System.Collections.Generic;

namespace SwitchKnifeApp.csv.opration
{
    public class Change
    {
        public Header Header { get; set; }

        public List<Move> Moves { get; set; }

        public Shrink Shrink { get; set; }
    }
}
