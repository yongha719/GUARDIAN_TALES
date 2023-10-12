using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using Color = UnityEngine.Color;

[CustomPropertyDrawer(typeof(BoxHeaderAttribute))]
public class BoxHeaderAttributeDrawer : DecoratorDrawer
{
    private BoxHeaderAttribute Atr => attribute as BoxHeaderAttribute;

    public float Height => 35f; // 5f : 헤더 <-> 첫번째 컨트롤 사이 간격

    public override void OnGUI(Rect position)
    {
        float headerHeight = 20f;
        float oneControlHeight = 20f;
        float boxHeight =
            headerHeight +
            oneControlHeight * (Atr.FieldCount)
            + Atr.BottomHeight + 5f; // 5f : 헤더 <-> 첫번째 컨트롤 사이 간격

        float X = position.x - 5f;
        float Y = position.y + (GetHeight() - headerHeight - 5f); // 5f : 헤더 <-> 첫번째 컨트롤 사이 간격
        float width = position.width + 5f;

        Rect headerRect = new Rect(X, Y, width, headerHeight);
        Rect headerTextRect = new Rect(X + 5f, Y, width, headerHeight);
        Rect boxRect = new Rect(X, Y, width, boxHeight);

        Color hCol = Atr.HeaderColor;
        Color bCol = Atr.BoxColor;

        bCol = SetAlpha(bCol, Atr.Alpha);
        bCol = PlusRGB(bCol, -0.1f);

        // Header Small Box Color
        EditorGUI.DrawRect(headerRect, new Color(bCol.r, bCol.g, bCol.b, 0.5f));

        if (Atr.FieldCount > 0)
        {
            EditorGUI.DrawRect(boxRect, bCol);
        }

        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel);
        headerStyle.normal.textColor = hCol;
        headerStyle.fontSize = 15;

        EditorGUI.LabelField(headerTextRect, Atr.HeaderText, headerStyle);
    }

    Color PlusRGB(Color color, float rgb)
    {
        return new Color(color.r + rgb, color.g + rgb, color.b + rgb, color.a);
    }

    Color SetAlpha(Color color, float alpha)
    {
        if (alpha > 1) alpha = 1f;
        else if (alpha < 0) alpha = 0f;

        return new Color(color.r, color.g, color.b, alpha);
    }
}
