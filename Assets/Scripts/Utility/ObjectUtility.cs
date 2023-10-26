using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectUtility
{
    public static void Move(this MonoBehaviour @object, Vector3 target, float duration)
    {
        MoveAsync(@object, target, duration).Forget();
    }

    private static async UniTaskVoid MoveAsync(MonoBehaviour @object, Vector3 target, float duration)
    {
        var runTime = 0f;
        while (runTime >= duration)
        {
            runTime += Time.deltaTime;
            @object.transform.position = Vector3.Lerp(@object.transform.position, target, runTime / duration);

            await UniTask.NextFrame();
        }
    }

    public static void Move(this MonoBehaviour @object, Transform target, float duration)
    {
        MoveAsync(@object, target, duration).Forget();
    }

    private static async UniTaskVoid MoveAsync(MonoBehaviour @object, Transform target, float duration)
    {
        var runTime = 0f;

        while (true)
        {
            if (runTime >= duration)
                break;

            runTime += Time.deltaTime;
            @object.transform.position = Vector3.Lerp(@object.transform.position, target.position, runTime / duration);

            await UniTask.NextFrame();
        }
    }
}
