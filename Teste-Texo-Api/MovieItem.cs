using System.Collections.Generic;

namespace Teste_Texo_Api
{
    public class MovieItem
    {
        public long Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public List<string> Studios { get; set; } = new List<string>();
        public List<string> Producers { get; set; } = new List<string>();
        public bool Winner { get; set; }
    }
}
