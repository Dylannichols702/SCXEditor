using System.Globalization;

public struct Color
{
    public float r;
    public float g;
    public float b;
    public float a;

    public Color()
    {
        this.r = 1; this.g = 1; this.b = 1; this.a = 1;
    }

    public Color(float r, float g, float b, float a)
    {
        this.r = r; this.g = g; this.b = b; this.a = a;
    }

    public Color(float r, float g, float b)
    {
        this.r = r; this.g = g; this.b = b; this.a = 1;
    }

    public static bool TryParseHtmlString(string htmlString, out Color color)
    {
        try
        {
            int argb = int.Parse(htmlString.Replace("#", ""), NumberStyles.HexNumber);
            int alpha = (argb >> 24) & 0xFF;
            int red = (argb >> 16) & 0xFF;
            int green = (argb >> 8) & 0xFF;
            int blue = (argb) & 0xFF;
            color = new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
        }
        catch
        {
            // i am lazy
            color = new Color();
            return false;
        }
        return true;
    }
}
