using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Texo_Api
{
    public class RepositoryMovie : IRepositoryMovie
    {

        protected readonly CatalogContext context;

        public RepositoryMovie(CatalogContext context)
        {
            this.context = context;
        }

        public MovieItem Get(int Id)
        {
            return this.context.TitleItems.FirstOrDefault(b => b.Id == Id);
        }

        public List<MovieItem> GetAll()
        {
            return this.context.TitleItems.ToList();
        }

        public List<MovieItem> GetWinners()
        {
            return this.context.TitleItems.AsQueryable().Where(b => b.Winner).ToList();
        }

        public List<ProducerWins> GetWins()
        {
            Dictionary<string, ProducerWins> ret = new Dictionary<string, ProducerWins>();

            ProducerWins producerWins;

            foreach (var movieItem in GetAll())
            {
                if (movieItem.Winner) { 
                    foreach (var producer in movieItem.Producers)
                    {
                        if (ret.TryGetValue(producer, out producerWins))
                        {
                            if (!producerWins.Wins.Contains(movieItem.Year))
                            {
                                producerWins.Wins.Add(movieItem.Year);
                                producerWins.Wins.Sort();
                            }
                        }
                        else
                        {
                            producerWins = new ProducerWins();
                            producerWins.Producer = producer;
                            producerWins.Wins.Add(movieItem.Year);
                            ret.Add(producer, producerWins);
                        }
                    }
                }
            }

            Dictionary<string, ProducerWins>.ValueCollection values = ret.Values;

            foreach (var producerWins2 in values.Where(v => v.Wins.Count() > 1))
            { 
                for (int i = 0; i < producerWins2.Wins.Count() - 1; i++)
                {
                    var interval = producerWins2.Wins[i + 1] - producerWins2.Wins[i];
                    if (interval < producerWins2.Min.Interval || producerWins2.Min.Interval == 0)
                    {
                        producerWins2.Min.PreviousWin = producerWins2.Wins[i];
                        producerWins2.Min.FollowingWin = producerWins2.Wins[i + 1];
                        producerWins2.Min.Interval = producerWins2.Min.FollowingWin - producerWins2.Min.PreviousWin;
                    }
                }

                producerWins2.Max.PreviousWin = producerWins2.Wins.Min();
                producerWins2.Max.FollowingWin = producerWins2.Wins.Max();
                producerWins2.Max.Interval = producerWins2.Max.FollowingWin - producerWins2.Max.PreviousWin;
            }                

            return values.OrderBy(b => b.Producer).ToList();            
        }

        public ProducerWinsStatisticListModel GetWinsStatistic()
        {
            var minInterval = 0;
            var maxInterval = 0;
            var listWins = GetWins();
            foreach (var producerWins in listWins)
            {
                if((minInterval == 0) || (producerWins.Min.Interval > 0 && producerWins.Min.Interval < minInterval))
                {
                    minInterval = producerWins.Min.Interval;
                }

                if (producerWins.Max.Interval > 0 && producerWins.Max.Interval > maxInterval)
                {
                    maxInterval = producerWins.Max.Interval;
                }
            }

            var ret = new ProducerWinsStatisticListModel();

            foreach (var producerWins in listWins.Where(b => b.Min.Interval == minInterval))
            {
                ret.Min.Add(new ProducerWinsStatisticItemModel()
                {
                    Producer = producerWins.Producer,
                    Interval = producerWins.Min.Interval,
                    PreviousWin = producerWins.Min.PreviousWin,
                    FollowingWin = producerWins.Min.FollowingWin
                });
            }

            foreach (var producerWins in listWins.Where(b => b.Max.Interval == maxInterval))
            {
                ret.Max.Add(new ProducerWinsStatisticItemModel()
                {
                    Producer = producerWins.Producer,
                    Interval = producerWins.Max.Interval,
                    PreviousWin = producerWins.Max.PreviousWin,
                    FollowingWin = producerWins.Max.FollowingWin
                });
            }

            return ret;
        }
    }
}
