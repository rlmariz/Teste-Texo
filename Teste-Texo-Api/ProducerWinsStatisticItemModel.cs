using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Texo_Api
{
    public class ProducerWinsStatisticItemModel
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
