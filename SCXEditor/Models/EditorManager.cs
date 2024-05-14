using Avalonia.Controls.Primitives;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.Models
{
    public partial class EditorManager : ObservableObject
    {
        public const int MAX_QUANTIZATION = 48;
        public static readonly int[] Quantizations = new int[] { 48, 24, 16, 12, 8, 6, 4, 3, 2, 1 };
        
        [ObservableProperty] private static int selectedAbsoluteRow = 0;

        private static int selectedQuantization = 0;
        private static int selectedBeat = 0;
        private static int selectedRow = 0;

        public static void IncrementQuantization()
        {
            if (selectedQuantization < Quantizations.Length) selectedQuantization++;
        }

        public static void DecrementQuantization()
        {
            if (selectedQuantization > 0) selectedQuantization--;
        }

        // TODO: you might be able to make this whole thing more compact, idrk :/
        public static void TraverseRowForward()
        {
            selectedAbsoluteRow += Quantizations[selectedQuantization];
            UpdateRowSelection();
        }

        public static void TraverseRowBackward()
        {
            if (selectedAbsoluteRow - Quantizations[selectedQuantization] >= 0)
            {
                selectedAbsoluteRow -= Quantizations[selectedQuantization];
                UpdateRowSelection();
            }
        }

        private static void UpdateRowSelection()
        {
            selectedBeat = selectedAbsoluteRow / MAX_QUANTIZATION;
            selectedRow = selectedAbsoluteRow % MAX_QUANTIZATION;
        }
    }
}
