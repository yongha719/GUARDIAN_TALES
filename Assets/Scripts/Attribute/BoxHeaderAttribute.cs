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

    // 색상 직접 결정할 수 있게 해주는 색상 선택 도구 노출
    //public bool UseColorPicker { get; set; } = false;

    public BoxHeaderAttribute(string header, int fieldCount)
    {
        HeaderText = header;
        FieldCount = fieldCount;
    }
}