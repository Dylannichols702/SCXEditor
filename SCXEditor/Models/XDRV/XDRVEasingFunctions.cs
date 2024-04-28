using System;
// Source: https://gist.github.com/Kryzarel/bba64622057f21a1d6d44879f9cd7bd4
// https://gist.github.com/Kryzarel

// Made with the help of this great post: https://joshondesign.com/2013/03/01/improvedEasingEquations

// --------------------------------- Other Related Links --------------------------------------------------------------------
// Original equations, bad formulation:	https://github.com/danro/jquery-easing/blob/master/jquery.easing.js
// A few equations, very simplified:	https://gist.github.com/gre/1650294
// Easings.net equations, simplified:	https://github.com/ai/easings.net/blob/master/src/easings/easingsFunctions.ts

public enum XDRVEasings
{
    Linear,
    InQuad,
    OutQuad,
    InOutQuad,
    InCubic,
    OutCubic,
    InOutCubic,
    InQuart,
    OutQuart,
    InOutQuart,
    InQuint,
    OutQuint,
    InOutQuint,
    InSine,
    OutSine,
    InOutSine,
    InExpo,
    OutExpo,
    InOutExpo,
    InCirc,
    OutCirc,
    InOutCirc,
    InElastic,
    OutElastic,
    InOutElastic,
    InBack,
    OutBack,
    InOutBack,
    InBounce,
    OutBounce,
    InOutBounce,
    Bounce,
    Tri,
    Bell,
    Pop,
    Tap,
    Pulse,
    Spike,
    Inverse,
    Instant,
    SmoothStep,
    SmootherStep,
    SmoothestStep
}

public static class XDRVEasingFunctions
{
    public static XDRVEasings FromString(string ease)
    {
        if (Enum.TryParse(ease, true, out XDRVEasings e))
        {
            return e;
        }
        return XDRVEasings.Linear;
    }

    public static float Ease(string ease, float t)
    {
        if (Enum.TryParse(ease, true, out XDRVEasings e))
        {
            return Ease(e, t);
        }
        Console.WriteLine($"Ease type {ease} does not exist! Falling back to Linear.");
        return Ease(XDRVEasings.Linear, t);
    }

    public static float Ease(XDRVEasings ease, float t)
    {
        return ease switch
        {
            XDRVEasings.Linear => Linear(t),
            XDRVEasings.InQuad => InQuad(t),
            XDRVEasings.OutQuad => OutQuad(t),
            XDRVEasings.InOutQuad => InOutQuad(t),
            XDRVEasings.InCubic => InCubic(t),
            XDRVEasings.OutCubic => OutCubic(t),
            XDRVEasings.InOutCubic => InOutCubic(t),
            XDRVEasings.InQuart => InQuart(t),
            XDRVEasings.OutQuart => OutQuart(t),
            XDRVEasings.InOutQuart => InOutQuart(t),
            XDRVEasings.InQuint => InQuint(t),
            XDRVEasings.OutQuint => OutQuint(t),
            XDRVEasings.InOutQuint => InOutQuint(t),
            XDRVEasings.InSine => InSine(t),
            XDRVEasings.OutSine => OutSine(t),
            XDRVEasings.InOutSine => InOutSine(t),
            XDRVEasings.InExpo => InExpo(t),
            XDRVEasings.OutExpo => OutExpo(t),
            XDRVEasings.InOutExpo => InOutExpo(t),
            XDRVEasings.InCirc => InCirc(t),
            XDRVEasings.OutCirc => OutCirc(t),
            XDRVEasings.InOutCirc => InOutCirc(t),
            XDRVEasings.InElastic => InElastic(t),
            XDRVEasings.OutElastic => OutElastic(t),
            XDRVEasings.InOutElastic => InOutElastic(t),
            XDRVEasings.InBack => InBack(t),
            XDRVEasings.OutBack => OutBack(t),
            XDRVEasings.InOutBack => InOutBack(t),
            XDRVEasings.InBounce => InBounce(t),
            XDRVEasings.OutBounce => OutBounce(t),
            XDRVEasings.InOutBounce => InOutBounce(t),
            XDRVEasings.Bounce => Bounce(t),
            XDRVEasings.Tri => Tri(t),
            XDRVEasings.Bell => Bell(t),
            XDRVEasings.Pop => Pop(t),
            XDRVEasings.Tap => Tap(t),
            XDRVEasings.Pulse => Pulse(t),
            XDRVEasings.Spike => Spike(t),
            XDRVEasings.Inverse => Inverse(t),
            XDRVEasings.Instant => Instant(t),
            XDRVEasings.SmoothStep => SmoothStep(t),
            XDRVEasings.SmootherStep => SmootherStep(t),
            XDRVEasings.SmoothestStep => SmoothestStep(t),
            _ => 0,
        };
    }

    public static float Linear(float t) => t;

    public static float InQuad(float t) => t * t;
    public static float OutQuad(float t) => 1 - InQuad(1 - t);
    public static float InOutQuad(float t)
    {
        if (t < 0.5) return InQuad(t * 2) / 2;
        return 1 - InQuad((1 - t) * 2) / 2;
    }

    public static float InCubic(float t) => t * t * t;
    public static float OutCubic(float t) => 1 - InCubic(1 - t);
    public static float InOutCubic(float t)
    {
        if (t < 0.5) return InCubic(t * 2) / 2;
        return 1 - InCubic((1 - t) * 2) / 2;
    }

    public static float InQuart(float t) => t * t * t * t;
    public static float OutQuart(float t) => 1 - InQuart(1 - t);
    public static float InOutQuart(float t)
    {
        if (t < 0.5) return InQuart(t * 2) / 2;
        return 1 - InQuart((1 - t) * 2) / 2;
    }

    public static float InQuint(float t) => t * t * t * t * t;
    public static float OutQuint(float t) => 1 - InQuint(1 - t);
    public static float InOutQuint(float t)
    {
        if (t < 0.5) return InQuint(t * 2) / 2;
        return 1 - InQuint((1 - t) * 2) / 2;
    }

    public static float InSine(float t) => (float)(1 - Math.Cos(t * Math.PI / 2));
    public static float OutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
    public static float InOutSine(float t) => (float)(-(Math.Cos(Math.PI * t) - 1) / 2);

    public static float InExpo(float t) => (float)(t == 0 ? 0 : Math.Pow(2, 10 * t - 10));
    public static float OutExpo(float t) => 1 - InExpo(1 - t);
    public static float InOutExpo(float t)
    {
        if (t < 0.5) return InExpo(t * 2) / 2;
        return 1 - InExpo((1 - t) * 2) / 2;
    }

    public static float InCirc(float t) => -((float)Math.Sqrt(1 - t * t) - 1);
    public static float OutCirc(float t) => 1 - InCirc(1 - t);
    public static float InOutCirc(float t)
    {
        if (t < 0.5) return InCirc(t * 2) / 2;
        return 1 - InCirc((1 - t) * 2) / 2;
    }

    public static float InElastic(float t) => 1 - OutElastic(1 - t);
    public static float OutElastic(float t)
    {
        float p = 0.3f;
        return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
    }
    public static float InOutElastic(float t)
    {
        if (t < 0.5) return InElastic(t * 2) / 2;
        return 1 - InElastic((1 - t) * 2) / 2;
    }

    public static float InBack(float t)
    {
        float s = 1.70158f;
        return t * t * ((s + 1) * t - s);
    }
    public static float OutBack(float t) => 1 - InBack(1 - t);
    public static float InOutBack(float t)
    {
        if (t < 0.5) return InBack(t * 2) / 2;
        return 1 - InBack((1 - t) * 2) / 2;
    }

    public static float InBounce(float t) => 1 - OutBounce(1 - t);
    public static float OutBounce(float t)
    {
        float div = 2.75f;
        float mult = 7.5625f;

        if (t < 1 / div)
        {
            return mult * t * t;
        }
        else if (t < 2 / div)
        {
            t -= 1.5f / div;
            return mult * t * t + 0.75f;
        }
        else if (t < 2.5 / div)
        {
            t -= 2.25f / div;
            return mult * t * t + 0.9375f;
        }
        else
        {
            t -= 2.625f / div;
            return mult * t * t + 0.984375f;
        }
    }
    public static float InOutBounce(float t)
    {
        if (t < 0.5) return InBounce(t * 2) / 2;
        return 1 - InBounce((1 - t) * 2) / 2;
    }

    public static float Bounce(float t)
    {
        return 4 * t * (1 - t);
    }

    public static float Tri(float t)
    {
        return 1 - Math.Abs(2 * t - 1);
    }

    public static float Bell(float t)
    {
        return InOutQuint(Tri(t));
    }

    public static float Pop(float t)
    {
        return 3.5f * (1 - t) * (1 - t) * MathF.Sqrt(t);
    }

    public static float Tap(float t)
    {
        return 3.5f * t * t * MathF.Sqrt(t);
    }

    public static float Pulse(float t)
    {
        return t < .5f ? Tap(t * 2) : -Pop(t * 2 - 1);
    }

    public static float Spike(float t)
    {
        return MathF.Exp(-10 * MathF.Abs(2 * t - 1));
    }

    public static float Inverse(float t)
    {
        return t * t * (1 - t) * (1 - t) / (0.5f - t);
    }

    public static float Instant(float _) => 1;

    public static float SmoothStep(float t) => 3 * MathF.Pow(t, 2) - 2 * MathF.Pow(t, 3);

    public static float SmootherStep(float t) => MathF.Pow(t, 5) * (5 * t * (t * (7 * t * (2 * t - 9) + 108) - 84) + 126);

    public static float SmoothestStep(float t) => MathF.Pow(t, 7) * 1716 + 7 * MathF.Pow(t, 8) * (2 * t * (3 * t * (t * (11 * t * (2 * t - 13) + 390) - 572) + 1430) - 1287);

}
