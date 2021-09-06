using CompetitiveRankingWeb.Models;
using System;
using System.Collections.Generic;

namespace CompetitiveRankingWeb.ViewModels
{
    public class GOTYViewModel
    {
        public List<Tuple<int, Game>> GOTYS { get; set; }
        public List<Tuple<int, Game>> GOTDS { get; set; }
        public Game GOAT { get; set; }
    }
}