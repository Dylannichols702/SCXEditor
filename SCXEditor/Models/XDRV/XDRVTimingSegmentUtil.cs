using System;
using System.Collections.Generic;

public static class XDRVTimingSegmentUtil
{
    #region Various
    public static List<XDRVTimingSegment> GetTimingSegments(XDRV xdrv)
    {
        return xdrv.chartTimingSegments;
    }

    public static List<XDRVTimingSegment> GetTimingSegments(XDRV xdrv, params string[] keys)
    {
        List<XDRVTimingSegment> segments = new List<XDRVTimingSegment>();

        for (int i = 0; i < xdrv.chartTimingSegments.Count; i++)
        {
            XDRVTimingSegment segment = xdrv.chartTimingSegments[i];
            foreach (string key in keys)
            {
                if (key == segment.Key)
                {
                    segments.Add(segment);
                    break;
                }
            }
        }
        return segments;
    }

    public static List<XDRVTimingSegment> GetTimingSegments(XDRV xdrv, params XDRVTimingSegmentType[] types)
    {
        List<XDRVTimingSegment> segments = new List<XDRVTimingSegment>();

        for (int i = 0; i < xdrv.chartTimingSegments.Count; i++)
        {
            XDRVTimingSegment segment = xdrv.chartTimingSegments[i];
            foreach (XDRVTimingSegmentType type in types)
            {
                if (type == segment.Type)
                {
                    segments.Add(segment);
                    break;
                }
            }
        }
        return segments;
    }

    private static int FindIntAtBeat(List<XDRVTimingSegment> segments, float beat, int defaultValue = 1)
    {
        int currentValue = defaultValue;

        for (int i = 0; i < segments.Count; i++)
        {
            XDRVTimingSegment data = segments[i];
            if (data.Beat > beat)
                break;
            currentValue = data.Value.GetInt(0);
        }

        return currentValue;
    }

    private static float FindFloatAtBeat(List<XDRVTimingSegment> segments, float beat, float defaultValue = 1)
    {
        float currentValue = defaultValue;

        for (int i = 0; i < segments.Count; i++)
        {
            XDRVTimingSegment data = segments[i];
            if (data.Beat > beat)
                break;
            currentValue = data.Value.GetFloat(0);
        }

        return currentValue;
    }

    private static string FindStringAtBeat(List<XDRVTimingSegment> segments, float beat, string defaultValue = null)
    {
        string currentValue = defaultValue;

        for (int i = 0; i < segments.Count; i++)
        {
            XDRVTimingSegment data = segments[i];
            if (data.Beat > beat)
                break;
            currentValue = data.Value.GetString(0);
        }

        return currentValue;
    }

    public static float GetBPMAtBeat(float beat, XDRV xdrv)
    {
        return FindFloatAtBeat(xdrv.BPMChanges, beat, xdrv.chartMetadata.ChartBPM);
    }

    public static int[] GetTimeSignatureAtBeat(float beat, XDRV xdrv)
    {
        List<XDRVTimingSegment> segments = xdrv.TimeSignatures;

        int currentBeat = 4;
        int currentSubdivision = 4;

        for (int i = 0; i < segments.Count; i++)
        {
            XDRVTimingSegment data = segments[i];
            if (data.Beat > beat)
                break;
            currentBeat = data.Value.GetInt(0);
            currentSubdivision = data.Value.GetInt(1);
        }

        int[] timeSig = new int[] { currentBeat, currentSubdivision };

        return timeSig;
    }

    public static List<float> GetComboTickBeats(XDRV xdrv)
    {
        List<float> list = new List<float>();

        // combo ticks, hell on earth baybeee
        List<XDRVTimingSegment> comboTicks = xdrv.ComboTicks;

        int comboTickCount = comboTicks.Count;

        float currentComboTicksPerBeat = 0;

        double elapsedBeat = 0;

        int rowCount = xdrv.chartBody.Beats.Count * XDRVChartUtils.ROWS_PER_BEAT;
        float currentComboTickBeat = -1;

        for (int row = 0; row < rowCount; row++)
        {
            // eval combo ticks
            for (int i = 0; i < comboTickCount; i++)
            {
                XDRVTimingSegment comboTick = comboTicks[i];

                if (comboTick.Row == row)
                {
                    currentComboTicksPerBeat = 1f / comboTick.Value.GetFloat(0);
                }

                if (comboTick.Row > row)
                    break;
            }

            if (currentComboTicksPerBeat > 0)
            {
                if (elapsedBeat % currentComboTicksPerBeat < currentComboTickBeat)
                {
                    list.Add((float)elapsedBeat);
                }
            }

            currentComboTickBeat = (float)elapsedBeat % currentComboTicksPerBeat;

            elapsedBeat += XDRVChartUtils.BEATS_PER_ROW;
        }

        return list;
    }

    public static int GetComboTicksAtBeat(float beat, XDRV xdrv)
    {
        List<XDRVTimingSegment> modData = xdrv.ComboTicks;
        return FindIntAtBeat(modData, beat, 1);
    }

    public static float GetMaximumBPM(XDRV xdrv)
    {
        float max = xdrv.chartMetadata.ChartBPM;
        foreach (XDRVTimingSegment segment in xdrv.BPMChanges)
        {
            float bpm = segment.Value.GetFloat(0);
            if (bpm > max)
                max = bpm;
        }
        return max;
    }

    public static float GetMinimumBPM(XDRV xdrv)
    {
        float min = xdrv.chartMetadata.ChartBPM;
        foreach (XDRVTimingSegment segment in xdrv.BPMChanges)
        {
            float bpm = segment.Value.GetFloat(0);
            if (bpm < min)
                min = bpm;
        }
        return min;
    }

    public static bool IsFakeActiveAtBeat(XDRV xdrv, float beat)
    {
        List<XDRVTimingSegment> fakes = xdrv.Fakes;
        List<XDRVTimingSegment> warps = xdrv.Warps;
        fakes.AddRange(warps);

        for (int i = 0; i < fakes.Count; i++)
        {
            XDRVTimingSegment data = fakes[i];
            float fakeBeat = data.Beat;
            float fakeDuration = data.Value.GetFloat(0);

            if (beat >= fakeBeat && beat < fakeBeat + fakeDuration)
                return true;
        }
        return false;
    }
    #endregion

    #region Display Beat

    public static float GetScrollRatio(float currentBpm, float maximumBpm, float scroll) => (currentBpm / maximumBpm) * scroll;

    public static List<DisplayBeatCache> BuildDisplayBeatCache(XDRV xdrv)
    {
        List<DisplayBeatCache> cache = new List<DisplayBeatCache>();
        List<XDRVTimingSegment> BPMChanges = xdrv.BPMChanges;
        List<XDRVTimingSegment> Scrolls = xdrv.Scrolls;

        float initialBpm = xdrv.chartMetadata.ChartBPM;
        float maximumBpm = GetMaximumBPM(xdrv);
        float currentBpm = initialBpm;
        float beatsPerRow = 1f / XDRVChartUtils.ROWS_PER_BEAT;

        double elapsedBeat = 0;
        float currentScroll = 1;
        float currentRatio = GetScrollRatio(currentBpm, maximumBpm, currentScroll);

        int scrollCount = Scrolls.Count;
        int scrollIndex = 0;
        int bpmChangeCount = BPMChanges.Count;
        int bpmChangeIndex = 0;

        double elapsedDisplayBeat = 0;

        int rowCount = xdrv.chartBody.Beats.Count * XDRVChartUtils.ROWS_PER_BEAT;

        for (int elapsedRow = 0; elapsedRow < rowCount; elapsedRow++)
        {
            bool generateCacheData = false;

            for (int i = bpmChangeIndex; i < bpmChangeCount; i++)
            {
                XDRVTimingSegment bpmChangeTimingSegment = BPMChanges[i];
                if (bpmChangeTimingSegment.Row <= elapsedRow)
                {
                    currentBpm = bpmChangeTimingSegment.Value.GetFloat(0);
                    currentRatio = GetScrollRatio(currentBpm, maximumBpm, currentScroll);
                    bpmChangeIndex++;
                    generateCacheData = true;
                }
                else
                {
                    break;
                }
            }

            for (int i = scrollIndex; i < scrollCount; i++)
            {
                XDRVTimingSegment scrollTimingSegment = Scrolls[i];
                if (scrollTimingSegment.Row <= elapsedRow)
                {
                    currentScroll = scrollTimingSegment.Value.GetFloat(0);
                    currentRatio = GetScrollRatio(currentBpm, maximumBpm, currentScroll);
                    scrollIndex++;
                    generateCacheData = true;
                }
                else
                {
                    break;
                }
            }

            if (generateCacheData)
            {
                DisplayBeatCache data = new DisplayBeatCache
                {
                    Beat = (float)elapsedBeat,
                    DisplayBeat = (float)elapsedDisplayBeat,
                    ScrollRatio = currentRatio,
                };

                cache.Add(data);
            }

            elapsedBeat += beatsPerRow;
            elapsedDisplayBeat += beatsPerRow * currentRatio;
        }

        return cache;
    }

    public struct DisplayBeatCache
    {
        public float Beat;
        public float DisplayBeat;
        public float ScrollRatio;
    }

    public static float GetDisplayBeat(float beat, XDRV xdrv, List<DisplayBeatCache> cache)
    {
        if (beat < 0)
            return beat;

        float maximumBpm = GetMaximumBPM(xdrv);

        DisplayBeatCache cacheEntry = new DisplayBeatCache();
        cacheEntry.ScrollRatio = GetScrollRatio(xdrv.chartMetadata.ChartBPM, maximumBpm, 1);

        foreach (DisplayBeatCache db in cache)
        {
            if (db.Beat == 0)
            {
                cacheEntry = db;
                break;
            }
        }

        foreach (DisplayBeatCache db in cache)
        {
            if (db.Beat > beat)
                break;
            cacheEntry = db;
        }

        float remainder = beat - cacheEntry.Beat;

        float displayBeat = cacheEntry.DisplayBeat;

        displayBeat += remainder * cacheEntry.ScrollRatio;

        return displayBeat;
    }

    #endregion

    #region Beat at Time and Time at Beat

    public struct TimingSegmentCache
    {
        public int Row;
        public float Beat;
        public float Time;
        public float TimePerRow;
        public float CurrentBPM;
        public float WarpDuration;
        public float StopDuration;
    }

    public static List<TimingSegmentCache> BuildTimingSegmentCache(XDRV xdrv)
    {
        List<TimingSegmentCache> cache = new List<TimingSegmentCache>();

        // need to account for bpm changes
        List<XDRVTimingSegment> bpmChanges = xdrv.BPMChanges;
        // need to account for warps
        List<XDRVTimingSegment> warps = xdrv.Warps;
        // need to account for stops
        List<XDRVTimingSegment> stops = xdrv.Stops;
        // need to account for stop_seconds
        List<XDRVTimingSegment> stopSeconds = xdrv.StopSeconds;

        int stopsCount = stops.Count;
        int stopSecondsCount = stopSeconds.Count;
        int warpsCount = warps.Count;
        int bpmChangesCount = bpmChanges.Count;
        int stopsIndex = 0;
        int stopSecondsIndex = 0;
        int warpsIndex = 0;
        int bpmChangesIndex = 0;

        float initialBpm = xdrv.chartMetadata.ChartBPM;

        float currentBpm = initialBpm;

        float currentTimePerRow = XDRVChartUtils.TIME_PER_ROW / currentBpm;

        double elapsedBeat = 0;
        double elapsedTime = 0;

        int rowCount = xdrv.chartBody.Beats.Count * XDRVChartUtils.ROWS_PER_BEAT;
        int nextRow = -1;
        float warpDuration = 0;
        for (int row = 0; row < rowCount; row++)
        {
            if (nextRow >= 0)
            {
                row = nextRow;
                nextRow = -1;
                warpDuration = 0;
                elapsedBeat += XDRVChartUtils.BEATS_PER_ROW;
            }
            else if (row > 0)
            {
                elapsedTime += currentTimePerRow;
                elapsedBeat += XDRVChartUtils.BEATS_PER_ROW;
            }

            float stopDuration = 0;

            bool shouldGenerateCacheData = false;

            // eval warp
            for (int i = warpsIndex; i < warpsCount; i++)
            {
                XDRVTimingSegment warp = warps[i];

                if (warp.Row <= row)
                {
                    // if warp is active, advance beat
                    float beats = warp.Value.GetFloat(0);

                    elapsedBeat = warp.Beat + beats;
                    nextRow = (int)(warp.Row + (beats * XDRVChartUtils.ROWS_PER_BEAT) + 0.5f);
                    warpDuration = beats;

                    shouldGenerateCacheData = true;

                    warpsIndex++;
                }
                else
                {
                    break;
                }
            }

            // eval bpm changes
            for (int i = bpmChangesIndex; i < bpmChangesCount; i++)
            {
                XDRVTimingSegment bpmChange = bpmChanges[i];

                if (bpmChange.Row <= row)
                {
                    currentBpm = bpmChange.Value.GetFloat(0);
                    currentTimePerRow = XDRVChartUtils.TIME_PER_ROW / currentBpm;

                    shouldGenerateCacheData = true;

                    bpmChangesIndex++;
                }
                else
                {
                    break;
                }
            }

            // eval stops
            for (int i = stopsIndex; i < stopsCount; i++)
            {
                XDRVTimingSegment stop = stops[i];

                if (stop.Row <= row)
                {
                    // if stop is active, prevent the progression of the beat variable for beats->seconds
                    float seconds = stop.Value.GetFloat(0) * (60 / currentBpm);

                    elapsedTime += seconds;
                    stopDuration = seconds;

                    shouldGenerateCacheData = true;

                    stopsIndex++;
                }
                else
                {
                    break;
                }
            }

            // eval stop seconds
            for (int i = stopSecondsIndex; i < stopSecondsCount; i++)
            {
                XDRVTimingSegment stop = stopSeconds[i];

                if (stop.Row <= row)
                {
                    // if stop is active, prevent the progression of the beat variable for beats->seconds
                    float seconds = stop.Value.GetFloat(0);

                    elapsedTime += seconds;
                    stopDuration = seconds;

                    shouldGenerateCacheData = true;

                    stopSecondsIndex++;
                }
                else
                {
                    break;
                }
            }

            if (shouldGenerateCacheData)
            {
                TimingSegmentCache data = new TimingSegmentCache
                {
                    Row = row,
                    Beat = (float)elapsedBeat - warpDuration,
                    Time = (float)elapsedTime - stopDuration,
                    TimePerRow = currentTimePerRow,
                    CurrentBPM = currentBpm,
                    WarpDuration = warpDuration,
                    StopDuration = stopDuration,
                };

                cache.Add(data);
            }
        }

        return cache;
    }

    public static float GetTimeAtRow(int row, XDRV xdrv, List<TimingSegmentCache> cache)
    {
        float initialBpm = xdrv.chartMetadata.ChartBPM;

        float currentTimePerRow = XDRVChartUtils.TIME_PER_ROW / initialBpm;

        if (cache.Count == 0)
        {
            return row * currentTimePerRow;
        }

        TimingSegmentCache cacheEntry = new TimingSegmentCache();
        cacheEntry.TimePerRow = currentTimePerRow;
        foreach (TimingSegmentCache ts in cache)
        {
            if (ts.Row > row)
                break;
            cacheEntry = ts;
        }

        int rowRemainder = row - cacheEntry.Row;

        float timeInRemainder = rowRemainder * cacheEntry.TimePerRow;

        float time = cacheEntry.Time + timeInRemainder;

        return time;
    }

    public static float GetTimeAtBeat(float beat, XDRV xdrv, List<TimingSegmentCache> cache)
    {
        float initialBpm = xdrv.chartMetadata.ChartBPM;
        float currentTimePerRow = XDRVChartUtils.TIME_PER_ROW / initialBpm;

        if (cache.Count == 0)
        {
            return beat * (currentTimePerRow * XDRVChartUtils.ROWS_PER_BEAT);
        }

        TimingSegmentCache cacheEntry = new TimingSegmentCache();
        cacheEntry.TimePerRow = currentTimePerRow;
        foreach (TimingSegmentCache ts in cache)
        {
            if (ts.Beat > beat)
                break;
            cacheEntry = ts;
        }

        float beatRemainder = MathF.Max(0, beat - (cacheEntry.Beat + cacheEntry.WarpDuration));

        float timeRemainder = beatRemainder * (cacheEntry.TimePerRow * XDRVChartUtils.ROWS_PER_BEAT) + (cacheEntry.Beat == beat ? 0 : cacheEntry.StopDuration);

        float time = cacheEntry.Time + timeRemainder;

        return time;
    }

    public static float GetBeatAtTime(float time, XDRV xdrv, List < TimingSegmentCache> cache)
    {
        float initialBpm = xdrv.chartMetadata.ChartBPM;

        if (cache.Count == 0)
        {
            return time * (initialBpm / 60f);
        }

        TimingSegmentCache cacheEntry = new TimingSegmentCache();
        cacheEntry.CurrentBPM = initialBpm;
        foreach (TimingSegmentCache ts in cache)
        {
            if (ts.Time > time)
                break;
            cacheEntry = ts;
        }

        float timeRemainder = MathF.Max(0, time - (cacheEntry.Time + cacheEntry.StopDuration));

        float beatRemainder = timeRemainder * (cacheEntry.CurrentBPM / 60f);

        float beat = cacheEntry.Beat + beatRemainder;

        return beat;
    }

    public static float FindTimeAtRow(int searchRow, XDRV xdrv)
    {
        // need to account for bpm changes
        List<XDRVTimingSegment> bpmChanges = xdrv.BPMChanges;
        // need to account for warps
        List<XDRVTimingSegment> warps = xdrv.Warps;
        // need to account for stops
        List<XDRVTimingSegment> stops = xdrv.Stops;
        // need to account for stop_seconds
        List<XDRVTimingSegment> stopSeconds = xdrv.StopSeconds;

        int stopsCount = stops.Count;
        int stopSecondsCount = stopSeconds.Count;
        int warpsCount = warps.Count;
        int bpmChangesCount = bpmChanges.Count;
        int stopsIndex = 0;
        int stopSecondsIndex = 0;
        int warpsIndex = 0;
        int bpmChangesIndex = 0;

        float initialBpm = xdrv.chartMetadata.ChartBPM;

        float currentBpm = initialBpm;

        float currentTimePerRow = XDRVChartUtils.TIME_PER_ROW / currentBpm;

        double elapsedBeat = 0;
        double elapsedTime = 0;

        int rowCount = xdrv.chartBody.Beats.Count * XDRVChartUtils.ROWS_PER_BEAT;
        int nextRow = -1;

        for (int row = 0; row < rowCount; row++)
        {
            if (nextRow >= 0)
            {
                row = nextRow;
                nextRow = -1;
                elapsedBeat += XDRVChartUtils.BEATS_PER_ROW;
            }
            else if (row > 0)
            {
                elapsedTime += currentTimePerRow;
                elapsedBeat += XDRVChartUtils.BEATS_PER_ROW;
            }

            if (row >= searchRow)
            {
                return (float)elapsedTime;
            }

            // eval warp
            for (int i = warpsIndex; i < warpsCount; i++)
            {
                XDRVTimingSegment warp = warps[i];

                if (warp.Row <= row)
                {
                    // if warp is active, advance beat
                    float beats = warp.Value.GetFloat(0);

                    elapsedBeat = warp.Beat + beats;
                    nextRow = (int)(warp.Row + (beats * XDRVChartUtils.ROWS_PER_BEAT) + 0.5f);

                    warpsIndex++;
                }
                else
                {
                    break;
                }
            }

            // eval bpm changes
            for (int i = bpmChangesIndex; i < bpmChangesCount; i++)
            {
                XDRVTimingSegment bpmChange = bpmChanges[i];

                if (bpmChange.Row <= row)
                {
                    currentBpm = bpmChange.Value.GetFloat(0);
                    currentTimePerRow = XDRVChartUtils.TIME_PER_ROW / currentBpm;
                    bpmChangesIndex++;
                }
                else
                {
                    break;
                }
            }

            // eval stops
            for (int i = stopsIndex; i < stopsCount; i++)
            {
                XDRVTimingSegment stop = stops[i];

                if (stop.Row <= row)
                {
                    // if stop is active, prevent the progression of the beat variable for beats->seconds
                    float seconds = stop.Value.GetFloat(0) * (60 / currentBpm);

                    elapsedTime += seconds;

                    stopsIndex++;
                }
                else
                {
                    break;
                }
            }

            // eval stop seconds
            for (int i = stopSecondsIndex; i < stopSecondsCount; i++)
            {
                XDRVTimingSegment stop = stopSeconds[i];

                if (stop.Row <= row)
                {
                    // if stop is active, prevent the progression of the beat variable for beats->seconds
                    float seconds = stop.Value.GetFloat(0);

                    elapsedTime += seconds;

                    stopSecondsIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        return (float)elapsedTime;
    }

    #endregion
}
