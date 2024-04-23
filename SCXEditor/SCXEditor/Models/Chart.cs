using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.Models
{
    public class Chart
    {
        public Chart() { }

        public string? ChartAuthor { get; set; }
        public string? ChartDifficulty { get; set; }
        public int? ChartLevel { get; set; }
        public float? MusicPreviewStart { get; set; }
        public float? MusicPreviewLength { get; set; }
        public bool? IsUnlock { get; set; }
        public float? DisplayBPM { get; set; }
        public float? BPM { get; set; }
        public bool? IsRPCHidden { get; set; }
        public string? StageBackground { get; set; }
    }
}
