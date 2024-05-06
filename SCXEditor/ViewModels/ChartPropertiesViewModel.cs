using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.ViewModels
{
    public class ChartPropertiesViewModel : ViewModelBase
    {
        private String? _ChartAuthor;
        private Array _Difficulties = Enum.GetValues(typeof(XDRVDifficulty));
        private XDRVDifficulty _SelectedDifficulty;
        private int _ChartLevel = 1;

        private double _MusicPreviewStart = 0.00;
        private double _MusicPreviewLength = 0.00;

        private double _DisplayBPM = 150.00;
        private double _TrueBPM = 150.00;

        private bool _IsUnlockedByDefault = true;
        private bool _IsHideRPC = false;

        // how do I stagebackground??

        public String? ChartAuthor { get => _ChartAuthor; set => _ChartAuthor = value; }
        public Array Difficulties { get => _Difficulties;  }
        public XDRVDifficulty SelectedDifficulty { get => _SelectedDifficulty; set => _SelectedDifficulty = value; }
        public int ChartLevel { get => _ChartLevel; set => _ChartLevel = value; }

        public double MusicPreviewStart { get => _MusicPreviewStart; set => _MusicPreviewStart = value; }
        public double MusicPreviewLength { get => _MusicPreviewLength; set => _MusicPreviewLength = value; }

        public double DisplayBPM { get => _DisplayBPM; set => _DisplayBPM = value; }
        public double TrueBPM { get => _TrueBPM; set => _TrueBPM = value; }

        public bool IsUnlockedByDefault { get => _IsUnlockedByDefault; set => _IsUnlockedByDefault = value; }
        public bool IsHideRPC { get => _IsHideRPC; set => _IsHideRPC = value; }

        public ChartPropertiesViewModel()
        {

        }
    }
}
