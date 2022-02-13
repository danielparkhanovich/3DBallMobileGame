using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangeElement
{
    public float Min;
    public float Max;


    public RangeElement()
    {

    }

    public RangeElement(float min, float max)
    {
        this.Min = min;
        this.Max = max;
    }

    public float GetRandomFromRange()
    {
        return Random.Range(Min, Max);
    }

    public float GetDifference()
    {
        return Max - Min;
    }

    public Vector2 GetVector2()
    {
        return new Vector2(Min, Max);
    }

    // Overload + operator
    public static RangeElement operator +(RangeElement b, RangeElement c)
    {
        RangeElement rangeElement = new RangeElement();
        rangeElement.Min = b.Min + c.Min;
        rangeElement.Max = b.Max + c.Max;
        return rangeElement;
    }

    // Overload - operator
    public static RangeElement operator -(RangeElement b, RangeElement c)
    {
        RangeElement rangeElement = new RangeElement();
        rangeElement.Min = b.Min - c.Min;
        rangeElement.Max = b.Max - c.Max;
        return rangeElement;
    }
}
