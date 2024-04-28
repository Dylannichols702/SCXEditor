using System.Collections.Generic;

/*
 * The ChartBody contains a List of ChartBeats.
 * Each ChartBeat contains an array of 48 rows which correspond to a quantization.
 * Each ChartRow contains a line of note data, and a list of mods on the same row.
 * After each ChartBeat, a '--' beat break delimiter is placed.
 * Mods are placed above the note data line.
 * 
 * To create a ChartBody, create a 'new ChartBody();'
 * And to add beat data, add to the Beats list with a 'new ChartBeat();'.
 * This ChartBeat object can be given data using 'ChartBeat.SetData(List<ChartRow>);'
 * Creating ChartRow objects will take some time, but each row is 1/48th of a beat.
 * Give it note data by accessing it's fields, and add mods by adding to it's Mods list with ChartMod objects.
 * Each ChartMod has a key and a value. The key will look like '#SCROLL' and the value will look like '1'.
 * Note that the actual line serialized will look like '#SCROLL=1' in this case.
 * 
 */

public class XDRVChartBody
{
    // We'll have a max of 192nd quantization, so 48 rows per beat
    public List<XDRVChartBeat> Beats = new List<XDRVChartBeat>();

    public new string ToString()
    {
        string newLine = "\n";
        string beatBreak = "--" + newLine;

        string chartString = beatBreak;

        for (int i = 0; i < Beats.Count; i++)
        {
            chartString += Beats[i].ToString();
            chartString += beatBreak;
        }

        return chartString;
    }
}