#nullable disable // no i dont think i will consider making a field nullable

using System.Collections.Generic;

public class XDRVChartRow
{
    public int[] Notes;
    public int[] GearShifts;
    public int Drift;

    public int[] Data => new int[9] { Notes[0], Notes[1], Notes[2], Notes[3], Notes[4], Notes[5], GearShifts[0], GearShifts[1], Drift };
    public List<XDRVTimingSegment> Segments;

    public XDRVChartRow()
    {
        //Mods = new List<ChartMod>();
        Notes = new int[6];
        GearShifts = new int[2];
        Drift = 0;
    }

    public new string ToString()
    {
        string modString = "";
        if (Segments != null)
        {
            foreach (XDRVTimingSegment mod in Segments)
            {
                modString += mod.ToString() + '\n';
            }
        }
        string noteString = $"{Notes[0]}{Notes[1]}{Notes[2]}-{Notes[3]}{Notes[4]}{Notes[5]}|{GearShifts[0]}{GearShifts[1]}|{Drift}";

        return modString + noteString;
    }

    public bool IsEmpty()
    {
        foreach (int i in Notes)
            if (i != 0)
                return false;
        foreach (int i in GearShifts)
            if (i != 0)
                return false;
        if (Drift != 0)
            return false;
        if (Segments != null && Segments.Count != 0)
            return false;
        return true;
    }
}

