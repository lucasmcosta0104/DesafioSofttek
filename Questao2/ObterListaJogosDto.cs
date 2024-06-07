using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2
{
    public class ObterListaJogosDto
    {
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<Jogo> Data { get; set; }
    }

    public class Jogo
    {
        public string Competition { get; set; }
        public string Year { get; set; }
        public string Round { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Team1goals { get; set; }
        public string Team2goals { get; set; }
    }
}
