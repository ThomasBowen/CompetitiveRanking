using CompetitiveRankingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompetitiveRankingWeb.ViewModels
{
    public class ChartViewModel
    {
        public List<IGrouping<int, Playthrough>> PlaythroughsPerYear { get; set; }
        public List<Tuple<string, int>> PlatformTotals { get; set; }
    }
}