public static class XDRVChartUtils
{
    /// <summary>
    /// A constant defining the rows per beat. Can be increased but will force you to adjust everything in here.
    /// </summary>
    public const int ROWS_PER_BEAT = 48;

    public const float BEATS_PER_ROW = 1f / ROWS_PER_BEAT;
    public const float TIME_PER_ROW = 60f / ROWS_PER_BEAT;


    /// <summary>
    /// Converts your row index (in a ChartBeat) to a beat. (Beat 0.5 would be row 24)
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public static float NoteRowToBeat(int row)
    {
        return row / (float)ROWS_PER_BEAT;
    }

    /// <summary>
    /// Converts your beat to a note row index (in a ChartBeat). (Beat 0.5 would be row 24)
    /// </summary>
    /// <param name="beat"></param>
    /// <returns></returns>
    public static int BeatToNoteRow(float beat)
    {
        return (int)(beat * ROWS_PER_BEAT + 0.5f);
    }

    /// <summary>
    /// Converts your note row index to a quantization, 4ths, 8ths, 12ths, etc.
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public static int GetNoteQuantFromRow(int row)
    {
        if (row % (ROWS_PER_BEAT / 1) == 0) return 4;
        else if (row % (ROWS_PER_BEAT / 2) == 0) return 8;
        else if (row % (ROWS_PER_BEAT / 3) == 0) return 12;
        else if (row % (ROWS_PER_BEAT / 4) == 0) return 16;
        else if (row % (ROWS_PER_BEAT / 6) == 0) return 24;
        else if (row % (ROWS_PER_BEAT / 8) == 0) return 32;
        else if (row % (ROWS_PER_BEAT / 12) == 0) return 48;
        else if (row % (ROWS_PER_BEAT / 16) == 0) return 64;
        else return 192;
    }

    /// <summary>
    /// Converts your beat to a note quantization. (Beat 0.5 would return 8, for an 8th note.)
    /// </summary>
    /// <param name="beat"></param>
    /// <returns></returns>
    public static int BeatToNoteQuant(float beat)
    {
        return GetNoteQuantFromRow(BeatToNoteRow(beat));
    }

    public static float NoteQuantToBeat(int quant)
    {
        return quant switch
        {
            4 => 1f,
            8 => 1f / 2,
            12 => 1f / 3,
            16 => 1f / 4,
            24 => 1f / 6,
            32 => 1f / 8,
            48 => 1f / 12,
            64 => 1f / 16,
            _ => 1f / 48,
        };
    }
}
