using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.Models
{
    public class ChartSet
    {
        public ChartSet() { }

        public string? MusicTitle { get; set; } 
        public string? MusicArtist { get; set; }
        public string? MusicAudioPath { get; set; }

        public ObservableCollection<Chart> Charts { get; set;}
    }
}
