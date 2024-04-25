using System;
using System.Numerics;

public class XDRVMods
{
    public float[] Values = new float[Enum.GetValues(typeof(XDRVChartModType)).Length];

    #region Speedmods

    public float Speed { get => Values[1]; set => Values[1] = value; }

    #endregion

    #region Camera

    public float CameraPositionX { get => Values[2]; set => Values[2] = value; }
    public float CameraPositionY { get => Values[3]; set => Values[3] = value; }
    public float CameraPositionZ { get => Values[4]; set => Values[4] = value; }

    public Vector3 CameraPosition { get => new Vector3(CameraPositionX, CameraPositionY, CameraPositionZ); set { CameraPositionX = value.X; CameraPositionY = value.Y; CameraPositionZ = value.Z; } }


    public float CameraRotationX { get => Values[5]; set => Values[5] = value; }
    public float CameraRotationY { get => Values[6]; set => Values[6] = value; }
    public float CameraRotationZ { get => Values[7]; set => Values[7] = value; }

    public Vector3 CameraRotationEulers { get => new Vector3(CameraRotationX, CameraRotationY, CameraRotationZ); set { CameraRotationX = value.X; CameraRotationY = value.Y; CameraRotationZ = value.Z; } }

    #endregion

    #region Tracks/Lanes

    public float TrackMoveX { get => Values[8]; set => Values[8] = value; }
    public float TrackMoveY { get => Values[9]; set => Values[9] = value; }
    public float TrackMoveZ { get => Values[10]; set => Values[10] = value; }

    public Vector3 TrackOffset => new Vector3(TrackMoveX, TrackMoveY, TrackMoveZ);


    public float TrackRotateX { get => Values[11]; set => Values[11] = value; }
    public float TrackRotateY { get => Values[12]; set => Values[12] = value; }
    public float TrackRotateZ { get => Values[13]; set => Values[13] = value; }

    public Vector3 TrackRotationEulers => new Vector3(TrackRotateX, TrackRotateY, TrackRotateZ);


    public float TrackLeftMoveX { get => Values[14]; set => Values[14] = value; }
    public float TrackLeftMoveY { get => Values[15]; set => Values[15] = value; }
    public float TrackLeftMoveZ { get => Values[16]; set => Values[16] = value; }

    public Vector3 TrackLeftOnlyOffset => new Vector3(TrackLeftMoveX, TrackLeftMoveY, TrackLeftMoveZ);
    public Vector3 TrackLeftOffset => TrackLeftOnlyOffset + TrackOffset;


    public float TrackLeftRotateX { get => Values[17]; set => Values[17] = value; }
    public float TrackLeftRotateY { get => Values[18]; set => Values[18] = value; }
    public float TrackLeftRotateZ { get => Values[19]; set => Values[19] = value; }

    public Vector3 TrackLeftOnlyRotationEulers => new Vector3(TrackLeftRotateX, TrackLeftRotateY, TrackLeftRotateZ);
    public Vector3 TrackLeftRotationEulers => TrackLeftOnlyRotationEulers + TrackRotationEulers;


    public float TrackRightMoveX { get => Values[20]; set => Values[20] = value; }
    public float TrackRightMoveY { get => Values[21]; set => Values[21] = value; }
    public float TrackRightMoveZ { get => Values[22]; set => Values[22] = value; }

    public Vector3 TrackRightOnlyOffset => new Vector3(TrackRightMoveX, TrackRightMoveY, TrackRightMoveZ);
    public Vector3 TrackRightOffset => TrackRightOnlyOffset + TrackOffset;


    public float TrackRightRotateX { get => Values[23]; set => Values[23] = value; }
    public float TrackRightRotateY { get => Values[24]; set => Values[24] = value; }
    public float TrackRightRotateZ { get => Values[25]; set => Values[25] = value; }

    public Vector3 TrackRightOnlyRotationEulers => new Vector3(TrackRightRotateX, TrackRightRotateY, TrackRightRotateZ);
    public Vector3 TrackRightRotationEulers => TrackRightOnlyRotationEulers + TrackRotationEulers;

    #endregion

    #region Gears

    public float GearMoveX { get => Values[26]; set => Values[26] = value; }
    public float GearMoveY { get => Values[27]; set => Values[27] = value; }
    public float GearMoveZ { get => Values[28]; set => Values[28] = value; }

    public Vector3 GearOffset => new Vector3(GearMoveX, GearMoveY, GearMoveZ);

    public float GearRotateX { get => Values[29]; set => Values[29] = value; }
    public float GearRotateY { get => Values[30]; set => Values[30] = value; }
    public float GearRotateZ { get => Values[31]; set => Values[31] = value; }

    public Vector3 GearRotationEulers => new Vector3(GearRotateX, GearRotateY, GearRotateZ);


    public float GearLeftMoveX { get => Values[32]; set => Values[32] = value; }
    public float GearLeftMoveY { get => Values[33]; set => Values[33] = value; }
    public float GearLeftMoveZ { get => Values[34]; set => Values[34] = value; }

    public Vector3 GearLeftOnlyOffset => new Vector3(GearLeftMoveX, GearLeftMoveY, GearLeftMoveZ);
    public Vector3 GearLeftOffset => GearLeftOnlyOffset + GearOffset;


    public float GearLeftRotateX { get => Values[35]; set => Values[35] = value; }
    public float GearLeftRotateY { get => Values[36]; set => Values[36] = value; }
    public float GearLeftRotateZ { get => Values[37]; set => Values[37] = value; }

    public Vector3 GearLeftOnlyRotationEulers => new Vector3(GearLeftRotateX, GearLeftRotateY, GearLeftRotateZ);
    public Vector3 GearLeftRotationEulers => GearLeftOnlyRotationEulers + GearRotationEulers;


    public float GearRightMoveX { get => Values[38]; set => Values[38] = value; }
    public float GearRightMoveY { get => Values[39]; set => Values[39] = value; }
    public float GearRightMoveZ { get => Values[40]; set => Values[40] = value; }

    public Vector3 GearRightOnlyOffset => new Vector3(GearRightMoveX, GearRightMoveY, GearRightMoveZ);
    public Vector3 GearRightOffset => GearRightOnlyOffset + GearOffset;


    public float GearRightRotateX { get => Values[41]; set => Values[41] = value; }
    public float GearRightRotateY { get => Values[42]; set => Values[42] = value; }
    public float GearRightRotateZ { get => Values[43]; set => Values[43] = value; }

    public Vector3 GearRightOnlyRotationEulers => new Vector3(GearRightRotateX, GearRightRotateY, GearRightRotateZ);
    public Vector3 GearRightRotationEulers => GearRightOnlyRotationEulers + GearRotationEulers;

    #endregion

    #region Notes

    public Vector3 GetNoteOffset(int column)
    {
        return column switch
        {
            0 => Note1Offset,
            1 => Note2Offset,
            2 => Note3Offset,
            3 => Note4Offset,
            4 => Note5Offset,
            5 => Note6Offset,
            6 => GearLeftOffset,
            7 => GearRightOffset,
            _ => NoteOffset,
        };
    }

    public Vector3 GetNoteRotationEulers(int column)
    {
        return column switch
        {
            0 => Note1RotationEulers,
            1 => Note2RotationEulers,
            2 => Note3RotationEulers,
            3 => Note4RotationEulers,
            4 => Note5RotationEulers,
            5 => Note6RotationEulers,
            6 => GearLeftRotationEulers,
            7 => GearRightRotationEulers,
            _ => NoteRotationEulers,
        };
    }

    public float NoteMoveX { get => Values[44]; set => Values[44] = value; }
    public float NoteMoveY { get => Values[45]; set => Values[45] = value; }
    public float NoteMoveZ { get => Values[46]; set => Values[46] = value; }

    public Vector3 NoteOffset => new Vector3(NoteMoveX, NoteMoveY, NoteMoveZ);

    public float NoteRotateX { get => Values[47]; set => Values[47] = value; }
    public float NoteRotateY { get => Values[48]; set => Values[48] = value; }
    public float NoteRotateZ { get => Values[49]; set => Values[49] = value; }

    public Vector3 NoteRotationEulers => new Vector3(NoteRotateX, NoteRotateY, NoteRotateZ);

    public float Note1MoveX { get => Values[50]; set => Values[50] = value; }
    public float Note1MoveY { get => Values[51]; set => Values[51] = value; }
    public float Note1MoveZ { get => Values[52]; set => Values[52] = value; }

    public Vector3 Note1OnlyOffset => new Vector3(Note1MoveX, Note1MoveY, Note1MoveZ);
    public Vector3 Note1Offset => Note1OnlyOffset + NoteOffset;


    public float Note1RotateX { get => Values[53]; set => Values[53] = value; }
    public float Note1RotateY { get => Values[54]; set => Values[54] = value; }
    public float Note1RotateZ { get => Values[55]; set => Values[55] = value; }

    public Vector3 Note1OnlyRotationEulers => new Vector3(Note1RotateX, Note1RotateY, Note1RotateZ);
    public Vector3 Note1RotationEulers => Note1OnlyRotationEulers + NoteRotationEulers;


    public float Note2MoveX { get => Values[56]; set => Values[56] = value; }
    public float Note2MoveY { get => Values[57]; set => Values[57] = value; }
    public float Note2MoveZ { get => Values[58]; set => Values[58] = value; }

    public Vector3 Note2OnlyOffset => new Vector3(Note2MoveX, Note2MoveY, Note2MoveZ);
    public Vector3 Note2Offset => Note2OnlyOffset + NoteOffset;


    public float Note2RotateX { get => Values[59]; set => Values[59] = value; }
    public float Note2RotateY { get => Values[60]; set => Values[60] = value; }
    public float Note2RotateZ { get => Values[61]; set => Values[61] = value; }

    public Vector3 Note2OnlyRotationEulers => new Vector3(Note2RotateX, Note2RotateY, Note2RotateZ);
    public Vector3 Note2RotationEulers => Note2OnlyRotationEulers + NoteRotationEulers;


    public float Note3MoveX { get => Values[62]; set => Values[62] = value; }
    public float Note3MoveY { get => Values[63]; set => Values[63] = value; }
    public float Note3MoveZ { get => Values[64]; set => Values[64] = value; }

    public Vector3 Note3OnlyOffset => new Vector3(Note3MoveX, Note3MoveY, Note3MoveZ);
    public Vector3 Note3Offset => Note3OnlyOffset + NoteOffset;


    public float Note3RotateX { get => Values[65]; set => Values[65] = value; }
    public float Note3RotateY { get => Values[66]; set => Values[66] = value; }
    public float Note3RotateZ { get => Values[67]; set => Values[67] = value; }

    public Vector3 Note3OnlyRotationEulers => new Vector3(Note3RotateX, Note3RotateY, Note3RotateZ);
    public Vector3 Note3RotationEulers => Note3OnlyRotationEulers + NoteRotationEulers;


    public float Note4MoveX { get => Values[68]; set => Values[68] = value; }
    public float Note4MoveY { get => Values[69]; set => Values[69] = value; }
    public float Note4MoveZ { get => Values[70]; set => Values[70] = value; }

    public Vector3 Note4OnlyOffset => new Vector3(Note4MoveX, Note4MoveY, Note4MoveZ);
    public Vector3 Note4Offset => Note4OnlyOffset + NoteOffset;


    public float Note4RotateX { get => Values[71]; set => Values[71] = value; }
    public float Note4RotateY { get => Values[72]; set => Values[72] = value; }
    public float Note4RotateZ { get => Values[73]; set => Values[73] = value; }

    public Vector3 Note4OnlyRotationEulers => new Vector3(Note4RotateX, Note4RotateY, Note4RotateZ);
    public Vector3 Note4RotationEulers => Note4OnlyRotationEulers + NoteRotationEulers;


    public float Note5MoveX { get => Values[74]; set => Values[74] = value; }
    public float Note5MoveY { get => Values[75]; set => Values[75] = value; }
    public float Note5MoveZ { get => Values[76]; set => Values[76] = value; }

    public Vector3 Note5OnlyOffset => new Vector3(Note5MoveX, Note5MoveY, Note5MoveZ);
    public Vector3 Note5Offset => Note5OnlyOffset + NoteOffset;


    public float Note5RotateX { get => Values[77]; set => Values[77] = value; }
    public float Note5RotateY { get => Values[78]; set => Values[78] = value; }
    public float Note5RotateZ { get => Values[79]; set => Values[79] = value; }

    public Vector3 Note5OnlyRotationEulers => new Vector3(Note5RotateX, Note5RotateY, Note5RotateZ);
    public Vector3 Note5RotationEulers => Note5OnlyRotationEulers + NoteRotationEulers;


    public float Note6MoveX { get => Values[80]; set => Values[80] = value; }
    public float Note6MoveY { get => Values[81]; set => Values[81] = value; }
    public float Note6MoveZ { get => Values[82]; set => Values[82] = value; }

    public Vector3 Note6OnlyOffset => new Vector3(Note6MoveX, Note6MoveY, Note6MoveZ);
    public Vector3 Note6Offset => Note6OnlyOffset + NoteOffset;


    public float Note6RotateX { get => Values[83]; set => Values[83] = value; }
    public float Note6RotateY { get => Values[84]; set => Values[84] = value; }
    public float Note6RotateZ { get => Values[85]; set => Values[85] = value; }

    public Vector3 Note6OnlyRotationEulers => new Vector3(Note6RotateX, Note6RotateY, Note6RotateZ);
    public Vector3 Note6RotationEulers => Note6OnlyRotationEulers + NoteRotationEulers;

    #endregion

    #region Black Bars

    public float BlackBarTopPosition { get => Values[86]; set => Values[86] = value; }
    public float BlackBarBottomPosition { get => Values[87]; set => Values[87] = value; }
    public float BlackBarLeftPosition { get => Values[88]; set => Values[88] = value; }
    public float BlackBarRightPosition { get => Values[89]; set => Values[89] = value; }

    public float[] BlackBarPositions => new float[] { BlackBarTopPosition, BlackBarBottomPosition, BlackBarLeftPosition, BlackBarRightPosition };


    public float BlackBarTopRotation { get => Values[90]; set => Values[90] = value; }
    public float BlackBarBottomRotation { get => Values[91]; set => Values[91] = value; }
    public float BlackBarLeftRotation { get => Values[92]; set => Values[92] = value; }
    public float BlackBarRightRotation { get => Values[93]; set => Values[93] = value; }

    public float[] BlackBarRotations => new float[] { BlackBarTopRotation, BlackBarBottomRotation, BlackBarLeftRotation, BlackBarRightRotation };

    #endregion

    #region Lane/Track Colors

    public float LaneColorRed { get => Values[94]; set => Values[94] = value; }
    public float LaneColorGreen { get => Values[95]; set => Values[95] = value; }
    public float LaneColorBlue { get => Values[96]; set => Values[96] = value; }
    public float LaneColorAlpha { get => Values[97]; set => Values[97] = value; }

    public Color LaneColor { get => new Color(LaneColorRed, LaneColorGreen, LaneColorBlue, LaneColorAlpha); set { LaneColorRed = value.r; LaneColorGreen = value.g; LaneColorBlue = value.b; LaneColorAlpha = value.a; } }

    #endregion

    public XDRVMods()
    {
        Reset();
    }

    public void Reset()
    {
        Speed = 1;

        CameraPositionX = 0;
        CameraPositionY = 0;
        CameraPositionZ = 0;

        CameraRotationX = 0;
        CameraRotationY = 0;
        CameraRotationZ = 0;

        TrackMoveX = 0;
        TrackMoveY = 0;
        TrackMoveZ = 0;

        TrackRotateX = 0;
        TrackRotateY = 0;
        TrackRotateZ = 0;

        TrackLeftMoveX = 0;
        TrackLeftMoveY = 0;
        TrackLeftMoveZ = 0;

        TrackLeftRotateX = 0;
        TrackLeftRotateY = 0;
        TrackLeftRotateZ = 0;

        TrackRightMoveX = 0;
        TrackRightMoveY = 0;
        TrackRightMoveZ = 0;

        TrackRightRotateX = 0;
        TrackRightRotateY = 0;
        TrackRightRotateZ = 0;

        GearMoveX = 0;
        GearMoveY = 0;
        GearMoveZ = 0;

        GearRotateX = 0;
        GearRotateY = 0;
        GearRotateZ = 0;

        GearLeftMoveX = 0;
        GearLeftMoveY = 0;
        GearLeftMoveZ = 0;

        GearLeftRotateX = 0;
        GearLeftRotateY = 0;
        GearLeftRotateZ = 0;

        GearRightMoveX = 0;
        GearRightMoveY = 0;
        GearRightMoveZ = 0;

        GearRightRotateX = 0;
        GearRightRotateY = 0;
        GearRightRotateZ = 0;

        NoteMoveX = 0;
        NoteMoveY = 0;
        NoteMoveZ = 0;

        NoteRotateX = 0;
        NoteRotateY = 0;
        NoteRotateZ = 0;

        Note1MoveX = 0;
        Note1MoveY = 0;
        Note1MoveZ = 0;

        Note1RotateX = 0;
        Note1RotateY = 0;
        Note1RotateZ = 0;

        Note2MoveX = 0;
        Note2MoveY = 0;
        Note2MoveZ = 0;

        Note2RotateX = 0;
        Note2RotateY = 0;
        Note2RotateZ = 0;

        Note3MoveX = 0;
        Note3MoveY = 0;
        Note3MoveZ = 0;

        Note3RotateX = 0;
        Note3RotateY = 0;
        Note3RotateZ = 0;

        Note4MoveX = 0;
        Note4MoveY = 0;
        Note4MoveZ = 0;

        Note4RotateX = 0;
        Note4RotateY = 0;
        Note4RotateZ = 0;

        Note5MoveX = 0;
        Note5MoveY = 0;
        Note5MoveZ = 0;

        Note5RotateX = 0;
        Note5RotateY = 0;
        Note5RotateZ = 0;

        Note6MoveX = 0;
        Note6MoveY = 0;
        Note6MoveZ = 0;

        Note6RotateX = 0;
        Note6RotateY = 0;
        Note6RotateZ = 0;

        BlackBarTopPosition = 0;
        BlackBarBottomPosition = 0;
        BlackBarLeftPosition = 0;
        BlackBarRightPosition = 0;

        BlackBarTopRotation = 0;
        BlackBarBottomRotation = 0;
        BlackBarLeftRotation = 0;
        BlackBarRightRotation = 0;

        LaneColorRed = 0.075f;
        LaneColorGreen = 0.075f;
        LaneColorBlue = 0.075f;
        LaneColorAlpha = 1f;
    }

    public void SetValue(XDRVChartMod mod, float progress)
    {
        float floatValue = LerpUnclamped(mod.GetStartFloat(), mod.GetFloat(), progress);

        Values[(int)mod.ModType] = floatValue;
    }

    public static float LerpUnclamped(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}