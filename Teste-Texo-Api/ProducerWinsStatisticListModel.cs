using System.Collections.Generic;

namespace Teste_Texo_Api
{
    public class ProducerWinsStatisticListModel
    {
        public List<ProducerWinsStatisticItemModel> Min { get; set; } = new List<ProducerWinsStatisticItemModel>();
        public List<ProducerWinsStatisticItemModel> Max { get; set; } = new List<ProducerWinsStatisticItemModel>();
    }
}
