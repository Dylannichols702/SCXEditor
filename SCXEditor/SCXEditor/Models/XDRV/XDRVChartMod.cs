using System.Collections.Generic;

public class XDRVChartMod
{
    public bool IsEase = false;
    public XDRVChartModType ModType = XDRVChartModType.Other;
    public bool StartDurationIsTime = false;
    public float StartTime = -1;

    public float SortValue = 0;

    public bool EaseDurationIsEnd = false;
    public float EaseDuration = -1;
    public XDRVEasings EaseType = XDRVEasings.Linear;

    public XDRVChartMod(XDRV xdrv, List<XDRVTimingSegmentUtil.TimingSegmentCache> cache, string name, float value, bool timeBased, float time)
    {
        IsEase = false;
        ModType = GetTypeFromString(name);
        _floatValue = value;
        StartDurationIsTime = timeBased;
        StartTime = time;
        EaseDurationIsEnd = false;
        EaseDuration = 0;
        EaseType = XDRVEasings.Linear;

        SortValue = StartDurationIsTime ? XDRVTimingSegmentUtil.GetBeatAtTime(StartTime, xdrv, cache) : StartTime;
    }

    public XDRVChartMod(XDRV xdrv, List<XDRVTimingSegmentUtil.TimingSegmentCache> cache, string name, float startValue, float endValue, bool timeBased, float startTime, bool isEnd, float endTime, string ease)
    {
        IsEase = true;
        ModType = GetTypeFromString(name);
        StartDurationIsTime = timeBased;
        StartTime = startTime;
        EaseDurationIsEnd = isEnd;
        EaseDuration = endTime;
        EaseType = XDRVEasingFunctions.FromString(ease);
        _floatStartValue = startValue;
        _floatValue = endValue;

        SortValue = StartDurationIsTime? XDRVTimingSegmentUtil.GetBeatAtTime(StartTime, xdrv, cache) : StartTime;
    }

    public override string ToString()
    {
        return $"{(IsEase ? "ease" : "set")}, {ModType}, {EaseType}, {_floatStartValue}, {_floatValue}, {StartTime}, {EaseDuration}, {StartDurationIsTime}, {EaseDurationIsEnd}";
    }

    public XDRVChartModType GetTypeFromString(string value)
    {
        return value switch
        {
            "speed" => XDRVChartModType.Speed,

            "camera_position_x" => XDRVChartModType.CameraPositionX,
            "camera_position_y" => XDRVChartModType.CameraPositionY,
            "camera_position_z" => XDRVChartModType.CameraPositionZ,
            "camera_rotation_x" => XDRVChartModType.CameraRotationX,
            "camera_rotation_y" => XDRVChartModType.CameraRotationY,
            "camera_rotation_z" => XDRVChartModType.CameraRotationZ,
            // alias
            "camera_move_x" => XDRVChartModType.CameraPositionX,
            "camera_move_y" => XDRVChartModType.CameraPositionY,
            "camera_move_z" => XDRVChartModType.CameraPositionZ,
            "camera_rotate_x" => XDRVChartModType.CameraRotationX,
            "camera_rotate_y" => XDRVChartModType.CameraRotationY,
            "camera_rotate_z" => XDRVChartModType.CameraRotationZ,

            "note_move_x" => XDRVChartModType.NoteMoveX,
            "note_move_y" => XDRVChartModType.NoteMoveY,
            "note_move_z" => XDRVChartModType.NoteMoveZ,
            "note_rotate_x" => XDRVChartModType.NoteRotateX,
            "note_rotate_y" => XDRVChartModType.NoteRotateY,
            "note_rotate_z" => XDRVChartModType.NoteRotateZ,

            "note1_move_x" => XDRVChartModType.Note1MoveX,
            "note1_move_y" => XDRVChartModType.Note1MoveY,
            "note1_move_z" => XDRVChartModType.Note1MoveZ,
            "note1_rotate_x" => XDRVChartModType.Note1RotateX,
            "note1_rotate_y" => XDRVChartModType.Note1RotateY,
            "note1_rotate_z" => XDRVChartModType.Note1RotateZ,

            "note2_move_x" => XDRVChartModType.Note2MoveX,
            "note2_move_y" => XDRVChartModType.Note2MoveY,
            "note2_move_z" => XDRVChartModType.Note2MoveZ,
            "note2_rotate_x" => XDRVChartModType.Note2RotateX,
            "note2_rotate_y" => XDRVChartModType.Note2RotateY,
            "note2_rotate_z" => XDRVChartModType.Note2RotateZ,

            "note3_move_x" => XDRVChartModType.Note3MoveX,
            "note3_move_y" => XDRVChartModType.Note3MoveY,
            "note3_move_z" => XDRVChartModType.Note3MoveZ,
            "note3_rotate_x" => XDRVChartModType.Note3RotateX,
            "note3_rotate_y" => XDRVChartModType.Note3RotateY,
            "note3_rotate_z" => XDRVChartModType.Note3RotateZ,

            "note4_move_x" => XDRVChartModType.Note4MoveX,
            "note4_move_y" => XDRVChartModType.Note4MoveY,
            "note4_move_z" => XDRVChartModType.Note4MoveZ,
            "note4_rotate_x" => XDRVChartModType.Note4RotateX,
            "note4_rotate_y" => XDRVChartModType.Note4RotateY,
            "note4_rotate_z" => XDRVChartModType.Note4RotateZ,

            "note5_move_x" => XDRVChartModType.Note5MoveX,
            "note5_move_y" => XDRVChartModType.Note5MoveY,
            "note5_move_z" => XDRVChartModType.Note5MoveZ,
            "note5_rotate_x" => XDRVChartModType.Note5RotateX,
            "note5_rotate_y" => XDRVChartModType.Note5RotateY,
            "note5_rotate_z" => XDRVChartModType.Note5RotateZ,

            "note6_move_x" => XDRVChartModType.Note6MoveX,
            "note6_move_y" => XDRVChartModType.Note6MoveY,
            "note6_move_z" => XDRVChartModType.Note6MoveZ,
            "note6_rotate_x" => XDRVChartModType.Note6RotateX,
            "note6_rotate_y" => XDRVChartModType.Note6RotateY,
            "note6_rotate_z" => XDRVChartModType.Note6RotateZ,

            "track_move_x" => XDRVChartModType.TrackMoveX,
            "track_move_y" => XDRVChartModType.TrackMoveY,
            "track_move_z" => XDRVChartModType.TrackMoveZ,
            "track_rotate_x" => XDRVChartModType.TrackRotateX,
            "track_rotate_y" => XDRVChartModType.TrackRotateY,
            "track_rotate_z" => XDRVChartModType.TrackRotateZ,

            "trackleft_move_x" => XDRVChartModType.TrackLeftMoveX,
            "trackleft_move_y" => XDRVChartModType.TrackLeftMoveY,
            "trackleft_move_z" => XDRVChartModType.TrackLeftMoveZ,
            "trackleft_rotate_x" => XDRVChartModType.TrackLeftRotateX,
            "trackleft_rotate_y" => XDRVChartModType.TrackLeftRotateY,
            "trackleft_rotate_z" => XDRVChartModType.TrackLeftRotateZ,

            "trackright_move_x" => XDRVChartModType.TrackRightMoveX,
            "trackright_move_y" => XDRVChartModType.TrackRightMoveY,
            "trackright_move_z" => XDRVChartModType.TrackRightMoveZ,
            "trackright_rotate_x" => XDRVChartModType.TrackRightRotateX,
            "trackright_rotate_y" => XDRVChartModType.TrackRightRotateY,
            "trackright_rotate_z" => XDRVChartModType.TrackRightRotateZ,

            "gear_move_x" => XDRVChartModType.GearMoveX,
            "gear_move_y" => XDRVChartModType.GearMoveY,
            "gear_move_z" => XDRVChartModType.GearMoveZ,
            "gear_rotate_x" => XDRVChartModType.GearRotateX,
            "gear_rotate_y" => XDRVChartModType.GearRotateY,
            "gear_rotate_z" => XDRVChartModType.GearRotateZ,

            "gearleft_move_x" => XDRVChartModType.GearLeftMoveX,
            "gearleft_move_y" => XDRVChartModType.GearLeftMoveY,
            "gearleft_move_z" => XDRVChartModType.GearLeftMoveZ,
            "gearleft_rotate_x" => XDRVChartModType.GearLeftRotateX,
            "gearleft_rotate_y" => XDRVChartModType.GearLeftRotateY,
            "gearleft_rotate_z" => XDRVChartModType.GearLeftRotateZ,

            "gearright_move_x" => XDRVChartModType.GearRightMoveX,
            "gearright_move_y" => XDRVChartModType.GearRightMoveY,
            "gearright_move_z" => XDRVChartModType.GearRightMoveZ,
            "gearright_rotate_x" => XDRVChartModType.GearRightRotateX,
            "gearright_rotate_y" => XDRVChartModType.GearRightRotateY,
            "gearright_rotate_z" => XDRVChartModType.GearRightRotateZ,

            "black_bar_top_position" => XDRVChartModType.BlackBarTopPosition,
            "black_bar_bottom_position" => XDRVChartModType.BlackBarBottomPosition,
            "black_bar_left_position" => XDRVChartModType.BlackBarLeftPosition,
            "black_bar_right_position" => XDRVChartModType.BlackBarRightPosition,

            "black_bar_top_rotation" => XDRVChartModType.BlackBarTopRotation,
            "black_bar_bottom_rotation" => XDRVChartModType.BlackBarBottomRotation,
            "black_bar_left_rotation" => XDRVChartModType.BlackBarLeftRotation,
            "black_bar_right_rotation" => XDRVChartModType.BlackBarRightRotation,

            "lane_color_red" => XDRVChartModType.LaneColorRed,
            "lane_color_green" => XDRVChartModType.LaneColorGreen,
            "lane_color_blue" => XDRVChartModType.LaneColorBlue,
            "lane_color_alpha" => XDRVChartModType.LaneColorAlpha,

            "lane_left_color_red" => XDRVChartModType.LaneLeftColorRed,
            "lane_left_color_green" => XDRVChartModType.LaneLeftColorGreen,
            "lane_left_color_blue" => XDRVChartModType.LaneLeftColorBlue,
            "lane_left_color_alpha" => XDRVChartModType.LaneLeftColorAlpha,

            "lane_right_color_red" => XDRVChartModType.LaneRightColorRed,
            "lane_right_color_green" => XDRVChartModType.LaneRightColorGreen,
            "lane_right_color_blue" => XDRVChartModType.LaneRightColorBlue,
            "lane_right_color_alpha" => XDRVChartModType.LaneRightColorAlpha,

            "drift_move_x" => XDRVChartModType.DriftMoveX,
            "drift_move_y" => XDRVChartModType.DriftMoveY,
            "drift_move_z" => XDRVChartModType.DriftMoveZ,
            "drift_rotate_x" => XDRVChartModType.DriftRotateX,
            "drift_rotate_y" => XDRVChartModType.DriftRotateY,
            "drift_rotate_z" => XDRVChartModType.DriftRotateZ,

            "driftleft_move_x" => XDRVChartModType.DriftLeftMoveX,
            "driftleft_move_y" => XDRVChartModType.DriftLeftMoveY,
            "driftleft_move_z" => XDRVChartModType.DriftLeftMoveZ,
            "driftleft_rotate_x" => XDRVChartModType.DriftLeftRotateX,
            "driftleft_rotate_y" => XDRVChartModType.DriftLeftRotateY,
            "driftleft_rotate_z" => XDRVChartModType.DriftLeftRotateZ,

            "driftright_move_x" => XDRVChartModType.DriftRightMoveX,
            "driftright_move_y" => XDRVChartModType.DriftRightMoveY,
            "driftright_move_z" => XDRVChartModType.DriftRightMoveZ,
            "driftright_rotate_x" => XDRVChartModType.DriftRightRotateX,
            "driftright_rotate_y" => XDRVChartModType.DriftRightRotateY,
            "driftright_rotate_z" => XDRVChartModType.DriftRightRotateZ,

            _ => XDRVChartModType.Other
        };
    }

    private float _floatStartValue = -1;
    public float GetStartFloat() => _floatStartValue;

    private float _floatValue = -1;
    public float GetFloat() => _floatValue;
}