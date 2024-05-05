using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.ViewModels
{
    public class ChartSetPropertiesViewModel : ViewModelBase
    {
        private int _Volume = 100;
        private double _Offset = 0.00;

        // how do I imagefile????
        private String? _Illustrator;

        public int Volume { get => _Volume; set => _Volume = value; }
        public double Offset { get => _Offset; set => _Offset = value; }
        public String? Illustrator { get => _Illustrator; set => _Illustrator = value; }

        public ChartSetPropertiesViewModel()
        {

        }
    }
}
