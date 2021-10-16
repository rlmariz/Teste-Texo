using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Texo_Api
{
    public interface IRepositoryMovie
    {
        List<MovieItem> GetAll();
        MovieItem Get(int Id);
        List<ProducerWins> GetWins();
        ProducerWinsStatisticListModel GetWinsStatistic();
        List<MovieItem> GetWinners();
    }
}
