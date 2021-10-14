using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Texo_Api
{
    public class ProducerWins
    {
        public string Producer { get; set; }
        public List<int> Wins { get; set; } = new List<int>();
        public ProducerWinsStatistic Min { get; set; } = new ProducerWinsStatistic();
        public ProducerWinsStatistic Max { get; set; } = new ProducerWinsStatistic();
    }
}
