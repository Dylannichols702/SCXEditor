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
        public const int BLANK_VALUE = 0;
        public const int TAP_NOTE_VALUE = 1;
        public const int MAX_QUANTIZATION = 48;
        public static readonly int[] Quantizations = new int[] { 48, 24, 16, 12, 8, 6, 4, 3, 1 };
        
        [ObservableProperty] private static int selectedAbsoluteRow = 0;

        private int selectedQuantization = 0;
        private int selectedBeat = 0;
        private int selectedRow = 0;

        private static EditorManager? _instance;
        public static EditorManager Instance
        {
            get
            {
                if (_instance == null) return new EditorManager();
                return _instance;
            }
        }

        public EditorManager()
        {
            _instance = this;
            InputManager.Instance.OnWKeyPressed += TraverseRowForward;
            InputManager.Instance.OnAKeyPressed += DecrementQuantization;
            InputManager.Instance.OnSKeyPressed += TraverseRowBackward;
            InputManager.Instance.OnDKeyPressed += IncrementQuantization;
            InputManager.Instance.OnNoteKeyPressed += PlaceNote;
            InputManager.Instance.OnSaveHotkeyPressed += SaveChart;
        }

        public void IncrementQuantization(object sender, EventArgs args)
        {
            if (selectedQuantization < Quantizations.Length) selectedQuantization++;
        }

        public void DecrementQuantization(object sender, EventArgs args)
        {
            if (selectedQuantization > 0) selectedQuantization--;
        }

        // TODO: you might be able to make this whole thing more compact, idrk :/
        public void TraverseRowForward(object sender, EventArgs args)
        {
            selectedAbsoluteRow += Quantizations[selectedQuantization];
            UpdateRowSelection();
        }

        public void TraverseRowBackward(object sender, EventArgs args)
        {
            if (selectedAbsoluteRow - Quantizations[selectedQuantization] >= 0)
            {
                selectedAbsoluteRow -= Quantizations[selectedQuantization];
                UpdateRowSelection();
            }
        }

        private void UpdateRowSelection()
        {
            selectedBeat = selectedAbsoluteRow / MAX_QUANTIZATION;
            selectedRow = selectedAbsoluteRow % MAX_QUANTIZATION;
        }

        public void PlaceNote(object sender, InputManager.OnNoteKeyPressedEventArgs args)
        {
            int numBeatsGenerated = ChartManager._ActiveChart?.chartBody.Beats.Count ?? 0;

            if (selectedBeat >= numBeatsGenerated)
            {
                for (int i = numBeatsGenerated; i <= selectedBeat; i++)
                {
                    ChartManager._ActiveChart?.chartBody.Beats.Add(new XDRVChartBeat());
                }
            }

            XDRVChartBeat beat = ChartManager._ActiveChart?.chartBody.Beats[selectedBeat] ?? new XDRVChartBeat();

            if (beat.Rows[selectedRow].Notes[args.column] == 0)
            {
                beat.Rows[selectedRow].Notes[args.column] = TAP_NOTE_VALUE;
            }
            else
            {
                beat.Rows[selectedRow].Notes[args.column] = BLANK_VALUE;
            }

            ChartManager._ActiveChart?.chartBody.Beats[selectedBeat].SetData(beat.Rows.ToList());
        }

        public void SaveChart(object sender, EventArgs args)
        {
            ChartManager._ActiveChart?.Serialize();
        }
    }
}
