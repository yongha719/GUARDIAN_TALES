using UnityEngine;


[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class BoxHeaderAttribute : PropertyAttribute
{
    public string HeaderText { get; private set; } = "";

    // 몇 줄의 필드를 묶을 것인지 결정
    public int FieldCount { get; private set; } = 0;

    public Color HeaderColor { get; set; } = Color.white;
    public Color BoxColor { get; set; } = Color.black;

    public float Alpha { get; set; } = 0.4f;

    // 추가 하단 높이
    public float BottomHeight { get; set; } = 0f;


    public BoxHeaderAttribute(string header, int fieldCount)
    {
        HeaderText = header;
        FieldCount = fieldCount;
    }

    public BoxHeaderAttribute(string header, int fieldCount, Color boxColor)
    {
        HeaderText = header;
        FieldCount = fieldCount;
        BoxColor = boxColor;
    }
}