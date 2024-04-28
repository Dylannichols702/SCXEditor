#nullable disable // PAIN

public class ChartMetadata
{
    // Fields & Values

    public string MusicTitle = "unknown title";
    public string AlternateTitle = null;
    public string Title => AlternateTitle == null || AlternateTitle == "" ? MusicTitle : AlternateTitle;
    public string MusicArtist = "unknown artist";
    public string MusicAudio = "music.ogg";
    public float MusicPreviewStart = 0f;
    public float MusicPreviewLength = 10f;
    public float MusicVolume = 1f;
    public float MusicOffset = 0f;

    public string JacketImage = "jacket.png";
    public string JacketIllustrator = "unknown illustrator";

    public string ChartAuthor = "unknown author";
    public float[] ChartTags = { 0f, 0f, 0f, 0f };
    public bool ChartBoss = false;
    public XDRVDifficulty ChartDifficulty = XDRVDifficulty.Normal;
    public int ChartLevel = 0;
    public string ChartUnlock = null;
    public int ChartDisplayBPM = 120;
    public float ChartBPM = 120f;

    public bool IsFlashTrack = false;
    public bool IsKeyboardOnly = false;
    public bool IsOriginalMusic = false;

    public string ModfilePath = null;
    public bool RpcHidden = false;
    public bool DisableLeaderboardUploading = false;
    public string StageBackground = "default";

    // Serializer Stuff

    public new string ToString()
    {
        string ms = "";

        ms += $"MUSIC_TITLE={MusicTitle}\n";
        ms += $"ALTERNATE_TITLE={AlternateTitle}\n";
        ms += $"MUSIC_ARTIST={MusicArtist}\n";
        ms += $"MUSIC_AUDIO={MusicAudio}\n";
        ms += $"MUSIC_PREVIEW_START={MusicPreviewStart}\n";
        ms += $"MUSIC_PREVIEW_LENGTH={MusicPreviewLength}\n";
        ms += $"MUSIC_VOLUME={MusicVolume}\n";
        ms += $"MUSIC_OFFSET={MusicOffset}\n";

        ms += $"JACKET_IMAGE={JacketImage}\n";
        ms += $"JACKET_ILLUSTRATOR={JacketIllustrator}\n";

        ms += $"CHART_AUTHOR={ChartAuthor}\n";
        ms += $"CHART_TAGS={ChartTags[0]},{ChartTags[1]},{ChartTags[2]},{ChartTags[3]}\n";
        ms += $"CHART_BOSS={ChartBoss.ToString().ToUpper()}\n";
        ms += $"CHART_DIFFICULTY={ChartDifficulty.ToString().ToUpper()}\n";
        ms += $"CHART_LEVEL={ChartLevel}\n";
        ms += $"CHART_UNLOCK={ChartUnlock}\n";
        ms += $"CHART_DISPLAY_BPM={ChartDisplayBPM}\n";
        ms += $"CHART_BPM={ChartBPM}\n";

        ms += $"FLASH_TRACK={IsFlashTrack.ToString().ToUpper()}\n";
        ms += $"KEYBOARD_ONLY={IsKeyboardOnly.ToString().ToUpper()}\n";
        ms += $"ORIGINAL={IsOriginalMusic.ToString().ToUpper()}\n";

        ms += $"MODFILE_PATH={ModfilePath}\n";
        ms += $"RPC_HIDDEN={RpcHidden.ToString().ToUpper()}\n";
        ms += $"DISABLE_LEADERBOARD_UPLOADING={DisableLeaderboardUploading.ToString().ToUpper()}\n";
        ms += $"STAGE_BACKGROUND={StageBackground}\n";

        return ms;
    }
}
