using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.Models
{
    public class ChartSetManager
    {
        public static ChartSet _ActiveChartSet = new ChartSet() {
            MusicTitle = "Test",
            MusicArtist = "Test Artist",
            MusicAudioPath = "/test/path/"
        };

        public static ChartSet GetActiveChartSet()
        {
            return _ActiveChartSet;
        }

        public static void SetActiveChartSet(ChartSet chartSet)
        {
            _ActiveChartSet = chartSet;
        }
    }
}
