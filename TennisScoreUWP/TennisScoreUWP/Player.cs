using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisScoreUWP
{
    public class Player
    {
        public string Name { get; set; }
        public int GameScore { get; set; }
        public int SetScore { get; set; }
        public bool IsServer { get; set; }
    }
}
