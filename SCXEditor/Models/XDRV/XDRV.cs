#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// The .xdrv file/chart format info is stored here. <br/>
/// </summary>
public class XDRV
{
    /// <summary>
    /// The file path to the .xdrv file.
    /// </summary>
    public string filePath = null;

    /// <summary>
    /// The chart's metadata, all the information stored from a chart is here.
    /// </summary>
    public ChartMetadata chartMetadata = new ChartMetadata();

    /// <summary>
    /// The chart's body, containing notes, beat breaks, and mods
    /// </summary>
    public XDRVChartBody chartBody = new XDRVChartBody();

    /// <summary>
    /// The timing segments of the chart.
    /// </summary>
    public List<XDRVTimingSegment> chartTimingSegments = new List<XDRVTimingSegment>();

    /// <summary>
    /// The mods instance.
    /// </summary>
    public XDRVModfile Mods;

    /// <summary>
    /// The XDRV constructor. Sets the filePath and waits for the user to start deserialization.
    /// </summary>
    /// <param name="filePath">The file path to the .xdrv file.</param>
    public XDRV(string filePath)
    {
        this.filePath = filePath;
    }

    /// <summary>
    /// Static method for deserializing a chart from a text/.xdrv file.
    /// </summary>
    /// <param name="filePath">The file path to the .xdrv file.</param>
    /// <returns>The XDRV file class, which contains the file path, ChartMetadata, and ChartBody.</returns>
    public static XDRV DeserializeFromFile(string filePath)
    {
        XDRV xdrv = new XDRV(filePath);
        xdrv.Deserialize();

        return xdrv;
    }

    /// <summary>
    /// Save and overwrite the existing .xdrv file.
    /// </summary>
    public void Serialize()
    {
        string xdrvFile = chartMetadata.ToString() + chartBody.ToString();
        File.WriteAllText(filePath, xdrvFile);
    }

    /// <summary>
    /// Save the .xdrv file to a specified file path.
    /// </summary>
    /// <param name="filePathOverride">The file path to save to.</param>
    public void Serialize(string filePathOverride)
    {
        string xdrvFile = chartMetadata.ToString() + chartBody.ToString();
        File.WriteAllText(filePathOverride, xdrvFile);
    }

    /// <summary>
    /// Deserialize a chart from a text/.xdrv file.
    /// </summary>
    public void Deserialize()
    {
        string[] xdrv = File.ReadAllText(filePath).Split('\n');

        // separate meta from body

        List<string> metadata = new List<string>();
        List<string> body = new List<string>();

        // Filter out all comments, and put metadata and body in their own lists
        bool parseMetadata = true;
        for (int i = 0; i < xdrv.Length; i++)
        {
            string line = xdrv[i];

            // Process comments
            int commentIndex = line.IndexOf("//");
            if (commentIndex != -1)
                line = line.Substring(0, commentIndex);

            line = line.Trim(); // Remove extra whitespace

            // Check for metadata/body parsing
            if (parseMetadata && line.StartsWith("--"))
                parseMetadata = false;
            if (line.Length <= 0)
                continue;

            if (parseMetadata)
            {
                metadata.Add(line);
            }
            else
            {
                body.Add(line);
            }
        }

        // Process the Chart Metadata
        for (int i = 0; i < metadata.Count; i++)
        {
            string line = metadata[i];

            int equalsIndex = line.IndexOf('=');
            if (equalsIndex == -1)
                continue;
            string key = line.Substring(0, equalsIndex);
            string value = line.Substring(equalsIndex + 1);
            ProcessMetadata(key, value);
        }

        string jacket = chartMetadata.JacketImage;
        if (jacket != null)
        {
            string path = Path.Combine(Path.GetDirectoryName(filePath), jacket);
            if (File.Exists(path))
            {
                // do some image loading stuff here for jackets
            }
        }

        // Process the Chart Body
        for (int i = 0; i < body.Count; i++)
        {
            string line = body[i];

            ProcessChartBody(line);
        }

        SetRowAndBeatOfSegments();

        chartTimingSegments = chartTimingSegments.OrderBy(t => t.Row).ThenBy(t => t.Type).ToList();

        AddTimingSegmentsToValues();

        SetTimeOfSegments();

        chartTimingSegments = chartTimingSegments.OrderBy(t => t.Time).ThenBy(t => t.Row).ThenBy(t => t.Type).ToList();

        SetDisplayBeatOfSegments();

        // mods

        if (chartMetadata.ModfilePath != null)
        {
            string path = Path.GetDirectoryName(filePath);
            path = Path.Combine(path, chartMetadata.ModfilePath);
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                XDRVModfile mods = new XDRVModfile(this, text);
                Mods = mods;
            }
        }
    }

    private void SetRowAndBeatOfSegments()
    {
        bool shouldGenerateCheckpoint = true;

        for (int b = 0; b < chartBody.Beats.Count; b++)
        {
            XDRVChartBeat beat = chartBody.Beats[b];
            for (int r = 0; r < beat.Rows.Length; r++)
            {
                XDRVChartRow row = beat.Rows[r];
                int currentRow = XDRVChartUtils.BeatToNoteRow(b) + r;

                if (row.Segments != null)
                {
                    for (int m = 0; m < row.Segments.Count; m++)
                    {
                        XDRVTimingSegment segment = row.Segments[m];

                        segment.Row = currentRow;
                        segment.Beat = XDRVChartUtils.NoteRowToBeat(r + (b * XDRVChartUtils.ROWS_PER_BEAT));

                        if (currentRow == 0 && shouldGenerateCheckpoint && segment.Type == XDRVTimingSegmentType.Checkpoint)
                        {
                            shouldGenerateCheckpoint = false;
                        }

                        chartTimingSegments.Add(segment);
                    }
                }

                if (currentRow == 0 && shouldGenerateCheckpoint)
                {
                    row.Segments ??= new List<XDRVTimingSegment>();

                    XDRVTimingSegment defaultCheckpoint = new XDRVTimingSegment("#CHECKPOINT", "Start Line");

                    row.Segments.Add(defaultCheckpoint);
                }
            }
        }
    }

    private void SetTimeOfSegments()
    {
        for (int b = 0; b < chartBody.Beats.Count; b++)
        {
            XDRVChartBeat beat = chartBody.Beats[b];
            for (int r = 0; r < beat.Rows.Length; r++)
            {
                XDRVChartRow row = beat.Rows[r];

                if (row.Segments != null)
                {
                    for (int m = 0; m < row.Segments.Count; m++)
                    {
                        XDRVTimingSegment segment = row.Segments[m];

                        segment.Time = XDRVTimingSegmentUtil.FindTimeAtRow(segment.Row, this);
                    }
                }
            }
        }
    }

    private void SetDisplayBeatOfSegments()
    {
        List<XDRVTimingSegment> BPMChanges = this.BPMChanges;
        List<XDRVTimingSegment> Scrolls = this.Scrolls;

        int bpmChangeCount = BPMChanges.Count;
        int scrollCount = Scrolls.Count;

        int bpmChangeIndex = 0;
        int scrollIndex = 0;

        int timingSegmentCount = chartTimingSegments.Count;
        int timingSegmentIndex = 0;

        float initialBpm = chartMetadata.ChartBPM;
        float maximumBpm = XDRVTimingSegmentUtil.GetMaximumBPM(this);

        float currentBpm = initialBpm;
        float beatsPerRow = 1f / XDRVChartUtils.ROWS_PER_BEAT;

        int elapsedRow = 0;
        double elapsedBeat = 0;
        float currentScroll = 1;
        float currentRatio = XDRVTimingSegmentUtil.GetScrollRatio(currentBpm, maximumBpm, currentScroll);

        double elapsedDisplayBeat = 0;

        int rowCount = chartBody.Beats.Count * XDRVChartUtils.ROWS_PER_BEAT;

        for (int r = 0; r < rowCount; r++)
        {
            for (int i = bpmChangeIndex; i < bpmChangeCount; i++)
            {
                XDRVTimingSegment segment = BPMChanges[bpmChangeIndex];

                if (segment.Row <= elapsedRow)
                {
                    currentBpm = segment.Value.GetFloat(0);
                    currentRatio = XDRVTimingSegmentUtil.GetScrollRatio(currentBpm, maximumBpm, currentScroll);
                    bpmChangeIndex++;
                }
                else
                {
                    break;
                }
            }

            for (int i = scrollIndex; i < scrollCount; i++)
            {
                XDRVTimingSegment segment = Scrolls[scrollIndex];

                if (segment.Row <= elapsedRow)
                {
                    currentScroll = segment.Value.GetFloat(0);
                    currentRatio = XDRVTimingSegmentUtil.GetScrollRatio(currentBpm, maximumBpm, currentScroll);
                    scrollIndex++;
                }
                else
                {
                    break;
                }
            }

            for (int i = timingSegmentIndex; i < timingSegmentCount; i++)
            {
                XDRVTimingSegment segment = chartTimingSegments[timingSegmentIndex];

                if (segment.Row <= elapsedRow)
                {
                    segment.DisplayBeat = (float)elapsedDisplayBeat;
                    timingSegmentIndex++;
                }
                else
                {
                    break;
                }
            }

            elapsedRow++;
            elapsedBeat += beatsPerRow;
            elapsedDisplayBeat += beatsPerRow * currentRatio;
        }
    }

    // Chart Body Parsing Functions

    private XDRVChartBeat currentBeat = null;
    private readonly List<XDRVChartRow> currentRows = new List<XDRVChartRow>();
    private XDRVChartRow currentRow = null;

    public List<XDRVTimingSegment> BPMChanges;
    public List<XDRVTimingSegment> Warps;
    public List<XDRVTimingSegment> Stops;
    public List<XDRVTimingSegment> StopSeconds;
    public List<XDRVTimingSegment> Scrolls;
    public List<XDRVTimingSegment> TimeSignatures;
    public List<XDRVTimingSegment> ComboTicks;
    public List<XDRVTimingSegment> Labels;
    public List<XDRVTimingSegment> Fakes;
    public List<XDRVTimingSegment> Events;
    public List<XDRVTimingSegment> Checkpoints;

    private void AddTimingSegmentsToValues()
    {
        BPMChanges = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.BPM);
        Warps = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.Warp);
        Stops = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.Stop);
        StopSeconds = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.StopSeconds);
        Scrolls = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.Scroll);
        TimeSignatures = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.TimeSignature);
        ComboTicks = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.ComboTick);
        Labels = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.Label);
        Fakes = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.Fake);
        Events = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.Event);
        Checkpoints = XDRVTimingSegmentUtil.GetTimingSegments(this, XDRVTimingSegmentType.Checkpoint);
    }

    private static Regex _regex = new Regex("\\d\\d\\d-\\d\\d\\d\\|\\d\\d\\|\\d", RegexOptions.IgnoreCase);
    /// <summary>
    /// Takes a line of text and parses it to proper chart format.
    /// </summary>
    /// <param name="line"></param>
    private void ProcessChartBody(string line)
    {
        int commentIndex = line.IndexOf("//");
        if (commentIndex != -1)
        {
            line = line.Substring(0, commentIndex);
        }

        line = line.Trim();

        if (line.StartsWith("--"))
        {
            if (currentBeat == null) // first beat break to say we've begun parsing the body
            {
                currentBeat = new XDRVChartBeat();
            }
            else
            {
                // apply our shrunken data to the beat
                currentBeat.SetData(currentRows);
                // add our beat to our chart body
                chartBody.Beats.Add(currentBeat);
                // and continue the process
                currentBeat = new XDRVChartBeat();
                currentRows.Clear();
            }
            // input beat break
        }
        else if (line.StartsWith("#"))
        {
            int equalsIndex = line.IndexOf('=');
            if (equalsIndex == -1)
            {
                Console.WriteLine($"Invalid timing segment string: {line}");
                return;
            }
            string key = line.Substring(0, equalsIndex);
            string value = line.Substring(equalsIndex + 1);

            currentRow ??= new XDRVChartRow();
            // input mod function
            currentRow.Segments ??= new List<XDRVTimingSegment>();
            currentRow.Segments.Add(new XDRVTimingSegment(key, value));
        }
        else if (_regex.IsMatch(line))
        {
            currentRow ??= new XDRVChartRow();
            // read as normal chart input

            // this is kinda gross but fairly efficient somehow
            for (int i = 0; i < 12; i++)
            {
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                        // parse left hand
                        currentRow.Notes[i] = ParseInt(line[i]);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        // parse right hand
                        currentRow.Notes[i - 1] = ParseInt(line[i]);
                        break;
                    case 8:
                    case 9:
                        // parse gear shifts
                        currentRow.GearShifts[i - 8] = ParseInt(line[i]);
                        break;
                    case 11:
                        // parse drift
                        currentRow.Drift = ParseInt(line[i]);
                        break;
                    case 3:
                    case 7:
                    case 10:
                    default:
                        break;
                }
            }

            currentRows.Add(currentRow);
            currentRow = null;
        }
    }

    // Metadata Parsing Functions

    /// <summary>
    /// Takes a key and a value, and parses that and sets the chartMetadata object's values.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void ProcessMetadata(string key, string value)
    {
        switch (key)
        {
            // Process all string values
            case "MUSIC_TITLE":
                chartMetadata.MusicTitle = value;
                break;
            case "ALTERNATE_TITLE":
                chartMetadata.AlternateTitle = value;
                break;
            case "MUSIC_ARTIST":
                chartMetadata.MusicArtist = value;
                break;
            case "MUSIC_AUDIO":
                chartMetadata.MusicAudio = value;
                break;
            case "JACKET_IMAGE":
                chartMetadata.JacketImage = value;
                break;
            case "JACKET_ILLUSTRATOR":
                chartMetadata.JacketIllustrator = value;
                break;
            case "CHART_AUTHOR":
                chartMetadata.ChartAuthor = value;
                break;
            case "CHART_UNLOCK":
                chartMetadata.ChartUnlock = value;
                break;
            case "STAGE_BACKGROUND":
                chartMetadata.StageBackground = value;
                break;
            case "MODFILE_PATH":
                chartMetadata.ModfilePath = value;
                break;
            // Then process all int values
            case "CHART_LEVEL":
                chartMetadata.ChartLevel = Math.Clamp(ParseInt(value), -99, 99);
                break;
            case "CHART_DISPLAY_BPM":
                chartMetadata.ChartDisplayBPM = ParseInt(value);
                break;
            // Then process all bool values
            case "CHART_BOSS":
                chartMetadata.ChartBoss = ParseBool(value);
                break;
            case "DISABLE_LEADERBOARD_UPLOADING":
                chartMetadata.DisableLeaderboardUploading = ParseBool(value);
                break;
            case "RPC_HIDDEN":
                chartMetadata.RpcHidden = ParseBool(value);
                break;
            case "FLASH_TRACK":
                chartMetadata.IsFlashTrack = ParseBool(value);
                break;
            case "KEYBOARD_ONLY":
                chartMetadata.IsKeyboardOnly = ParseBool(value);
                break;
            case "ORIGINAL":
                chartMetadata.IsOriginalMusic = ParseBool(value);
                break;
            // Then process all float values
            case "MUSIC_PREVIEW_START":
                chartMetadata.MusicPreviewStart = ParseFloat(value);
                break;
            case "MUSIC_PREVIEW_LENGTH":
                chartMetadata.MusicPreviewLength = ParseFloat(value);
                break;
            case "MUSIC_VOLUME":
                chartMetadata.MusicVolume = ParseFloat(value);
                break;
            case "MUSIC_OFFSET":
                chartMetadata.MusicOffset = ParseFloat(value);
                break;
            case "CHART_BPM":
                chartMetadata.ChartBPM = ParseFloat(value);
                break;
            // Then process all string array values
            // Then process all int array values
            // Then process all float array values
            case "CHART_TAGS":
                chartMetadata.ChartTags = ParseFloatArray(value, 4);
                break;
            // Then process all bool array values
            // Finally, process anything else (enums)
            case "CHART_DIFFICULTY":
                chartMetadata.ChartDifficulty = ParseDifficulty(value);
                break;
            // Error handling?
            default:
                break;
        }
    }

    // Utility Functions for Parsing strings

    /// <summary>
    /// Parses an enum given the generic type
    /// </summary>
    /// <typeparam name="T">An enum of some kind</typeparam>
    /// <param name="value">The value in a key/value pair, used for parsing</param>
    /// <returns></returns>
    /// <exception cref="Exception">If T is not an enum.</exception>
    private T ParseEnum<T>(string value) where T : struct
    {
        Type type = typeof(T);
        if (!type.IsEnum)
            throw new Exception($"Type '{type}' was not an enum.");
        if (Enum.TryParse(value, true, out T result))
        {
            return result;
        }
        return default;
    }

    private XDRVDifficulty ParseDifficulty(string value)
    {
        return ParseEnum<XDRVDifficulty>(value);
    }

    /// <summary>
    /// Parses an integer from a char.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private int ParseInt(char value)
    {
        return value - '0';
    }

    /// <summary>
    /// Parses an integer given a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>An integer. -1 if invalid.</returns>
    private int ParseInt(string value)
    {
        return int.TryParse(value, out int result) ? result : -1;
    }

    /// <summary>
    /// Parses a bool given a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>A bool. Can't really check if it's invalid though.</returns>
    private bool ParseBool(string value)
    {
        return bool.TryParse(value, out bool result) && result;
    }

    /// <summary>
    /// Parses a float given a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>A float. -1 if invalid.</returns>
    private float ParseFloat(string value)
    {
        return float.TryParse(value, out float result) ? result : -1;
    }

    /// <summary>
    /// Parses an array of integers given a max size.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="size"></param>
    /// <returns>The array of integers. -1 for invalid values.</returns>
    private int[] ParseIntArray(string value, int size)
    {
        int[] result = new int[size];

        string[] values = value.Split(',');

        for (int i = 0; i < Math.Min(size, values.Length); i++)
        {
            result[i] = ParseInt(values[i]);
        }

        return result;
    }

    /// <summary>
    /// Parses an array of floats given a max size.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="size"></param>
    /// <returns>The array of floats. -1 for invalid values.</returns>
    private float[] ParseFloatArray(string value, int size)
    {
        float[] result = new float[size];

        string[] values = value.Split(',');

        for (int i = 0; i < Math.Min(size, values.Length); i++)
        {
            result[i] = ParseFloat(values[i]);
        }

        return result;
    }

    /// <summary>
    /// Parses an array of bools given a max size.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="size"></param>
    /// <returns>The array of bools.</returns>
    private bool[] ParseBoolArray(string value, int size)
    {
        bool[] result = new bool[size];

        string[] values = value.Split(',');

        for (int i = 0; i < Math.Min(size, values.Length); i++)
        {
            result[i] = ParseBool(values[i]);
        }

        return result;
    }

    /// <summary>
    /// Parses an array of strings given a max size.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="size"></param>
    /// <returns>The array of strings. Empty values are probably null.</returns>
    private string[] ParseStringArray(string value, int size)
    {
        string[] result = new string[size];

        string[] values = value.Split(',');

        for (int i = 0; i < Math.Min(size, values.Length); i++)
        {
            result[i] = values[i];
        }

        return result;
    }
}