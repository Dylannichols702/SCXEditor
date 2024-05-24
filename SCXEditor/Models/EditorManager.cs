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
        public const int HOLD_START_VALUE = 2;
        public const int HOLD_END_VALUE = 4;
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
            InputManager.Instance.OnTapNoteKeyPressed += PlaceTapNote;
            InputManager.Instance.OnSaveHotkeyPressed += SaveChart;
            InputManager.Instance.OnHoldNoteKeyPressed += PlaceHoldNote;
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

        public void PlaceTapNote(object sender, InputManager.OnNoteKeyPressedEventArgs args)
        {
            ExtendChartIfNecessary(selectedBeat);
            XDRVChartBeat beat = ChartManager._ActiveChart?.chartBody.Beats[selectedBeat] ?? new XDRVChartBeat();

            if (beat.Rows[selectedRow].Notes[args.column] == BLANK_VALUE)
            {
                beat.Rows[selectedRow].Notes[args.column] = TAP_NOTE_VALUE;
            }
            else
            {
                beat.Rows[selectedRow].Notes[args.column] = BLANK_VALUE;
            }

            ChartManager._ActiveChart?.chartBody.Beats[selectedBeat].SetData(beat.Rows.ToList());
        }

        // Attempt to create a hold note with the last tap note in the appropriate column
        public void PlaceHoldNote(object sender, InputManager.OnNoteKeyPressedEventArgs args)
        {
            List<XDRVChartBeat>? beats = ChartManager._ActiveChart?.chartBody.Beats;
            ExtendChartIfNecessary(selectedBeat);

            if (beats != null)
            {
                for (int j = selectedAbsoluteRow; j >= 0; j--)
                {
                    int currentBeat = j / MAX_QUANTIZATION;
                    int currentRow = j % MAX_QUANTIZATION;
                    int currentNote = beats[currentBeat].Rows[currentRow].Notes[args.column];
                    if (currentNote == TAP_NOTE_VALUE) 
                    {
                        beats[currentBeat].Rows[currentRow].Notes[args.column] = HOLD_START_VALUE;
                        beats[selectedBeat].Rows[selectedRow].Notes[args.column] = HOLD_END_VALUE;
                        break;
                    } else if (currentNote != BLANK_VALUE)
                    {
                        // Break the sequence if there is another type of note in the way
                        break;
                    }
                }
            }
        }

        public void SaveChart(object sender, EventArgs args)
        {
            ChartManager._ActiveChart?.Serialize();
        }

        // Extend the chart to the desired beat
        private void ExtendChartIfNecessary(int selectedBeat)
        {
            int numBeatsGenerated = ChartManager._ActiveChart?.chartBody.Beats.Count ?? 0;

            if (selectedBeat >= numBeatsGenerated)
            {
                for (int i = numBeatsGenerated; i <= selectedBeat; i++)
                {
                    ChartManager._ActiveChart?.chartBody.Beats.Add(new XDRVChartBeat());
                }
            }
        }
    }
}
