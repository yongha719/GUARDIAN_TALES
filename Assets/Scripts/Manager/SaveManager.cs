using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;

public class SaveManager
{
    /// <summary>
    /// 가디언이 장착한 무기 이미지 저장
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="path"></param>
    public static void SaveWeapon(Texture2D texture, string guardianName)
    {
#if UNITY_ANDROID
        // 안드로이드 권한 요청
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false)
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
#endif

        byte[] bytes = texture.EncodeToPNG();

#if UNITY_ANDROID
        string filePath = Path.Combine($"{Application.persistentDataPath}/Guardian");
#else
        string filePath = Path.Combine("Test");
#endif

        // 디렉토리 생성
        if (Directory.Exists(filePath) == false)
        {
            Directory.CreateDirectory(filePath);
            Debug.Log($"Create Directory : {filePath}");
        }
        
        
        File.WriteAllBytes(Path.Combine(filePath, $"{guardianName}.png"), bytes);

        Debug.Log($"Success Save\nDirectory : {filePath}");
    }
}