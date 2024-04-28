using System;

public class XDRVTimingSegmentValues
{
    private readonly string _originalValue;

    public string[] Values;

    // for caching
    private float[] _floatValues;
    private int[] _intValues;
    private bool[] _boolValues;
    private Color[] _colorValues;

    public XDRVTimingSegmentValues(string value)
    {
        _originalValue = value;

        // Ease values should be separate
        Values = value.Split(',');

        // do this instead of try parsing whenever we need a value
        _floatValues = new float[Values.Length];
        for (int i = 0; i < Values.Length; i++)
        {
            _floatValues[i] = float.TryParse(Values[i], out float res) ? res : -1f;
        }

        _intValues = new int[Values.Length];
        for (int i = 0; i < Values.Length; i++)
        {
            _intValues[i] = int.TryParse(Values[i], out int res) ? res : -1;
        }

        _boolValues = new bool[Values.Length];
        for (int i = 0; i < Values.Length; i++)
        {
            _boolValues[i] = bool.TryParse(Values[i], out bool res) && res;
        }

        _colorValues = new Color[Values.Length];
        for (int i = 0; i < Values.Length; i++)
        {
            _colorValues[i] = Color.TryParseHtmlString(Values[i], out Color color) ? color : new Color(0, 0, 0, 1);
        }
    }

    public new string ToString()
    {
        return _originalValue;
    }

    public string GetString(int index)
    {
        if (index >= _colorValues.Length)
        {
            string err = $"String argument with index {index} is not defined -- full line: {ToString()}";
            Console.WriteLine(err);
            return string.Empty;
        }

        return Values[index];
    }

    public float GetFloat(int index)
    {
        if (index >= _floatValues.Length)
        {
            string err = $"Float argument with index {index} is not defined -- full line: {ToString()}";
            Console.WriteLine(err);
            return 0;
        }

        return _floatValues[index];
    }

    public int GetInt(int index)
    {
        if (index >= _intValues.Length)
        {
            string err = $"Integer argument with index {index} is not defined -- full line: {ToString()}";
            Console.WriteLine(err);
            return 0;
        }

        return _intValues[index];
    }

    public bool GetBool(int index)
    {
        if (index >= _boolValues.Length)
        {
            string err = $"Boolean argument with index {index} is not defined -- full line: {ToString()}";
            Console.WriteLine(err);
            return false;
        }

        return _boolValues[index];
    }

    public Color GetColor(int index)
    {
        if (index >= _colorValues.Length)
        {
            string err = $"Color argument with index {index} is not defined -- full line: {ToString()}";
            Console.WriteLine(err);
            return new Color(1, 1, 1, 1);
        }

        return _colorValues[index];
    }

    public string OriginalValue => _originalValue;
}