public class XDRVTimingSegment
{
    public int Row;
    public float Beat;
    public float Time;
    public float DisplayBeat;

    public string Key;
    public XDRVTimingSegmentType Type;

    public XDRVTimingSegmentValues Value;

    public XDRVTimingSegment(string key, string value)
    {
        Key = key;
        Value = new XDRVTimingSegmentValues(value);
        Type = LookupTimingSegmentType();

        Row = -1;
        Beat = -1;
        Time = -1;
        DisplayBeat = -1;
    }

    public new string ToString()
    {
        return $"{Key}={Value.ToString()}";
    }

    private XDRVTimingSegmentType LookupTimingSegmentType()
    {
        string key = Key.ToUpper();

        return key switch
        {
            "#BPM" => XDRVTimingSegmentType.BPM,
            "#WARP" => XDRVTimingSegmentType.Warp,
            "#STOP" => XDRVTimingSegmentType.Stop,
            "#STOP_SECONDS" => XDRVTimingSegmentType.StopSeconds,
            "#SCROLL" => XDRVTimingSegmentType.Scroll,
            "#TIME_SIGNATURE" => XDRVTimingSegmentType.TimeSignature,
            "#COMBO_TICKS" => XDRVTimingSegmentType.ComboTick,
            "#COMBO" => XDRVTimingSegmentType.ComboMultiplier,
            "#LABEL" => XDRVTimingSegmentType.Label,
            "#FAKE" => XDRVTimingSegmentType.Fake,
            "#EVENT" => XDRVTimingSegmentType.Event,
            "#CHECKPOINT" => XDRVTimingSegmentType.Checkpoint,
            _ => XDRVTimingSegmentType.Other
        };
    }
}
