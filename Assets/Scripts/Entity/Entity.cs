using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header(nameof(Entity))]
    private int bbracjji;

    public abstract EntityData Data { get; set; }

    public event Action DeathEvent = () => { };

    protected virtual void Start()
    {
        Data.HpData.OnChange += (hp) =>
        {
            if (hp <= 0)
                DeathEvent();
        };
    }


    public virtual void Hit(in Entity entity)
    {
        Data.HpData -= entity.Data.Damage.ModifyDamage(Data.ElementalAttribute, entity.Data.ElementalAttribute);
    }

    public virtual void Hit(in Entity entity, float damageMultiply)
    {
        int damage = (int)(entity.Data.Damage * damageMultiply);

        Data.HpData -= damage.ModifyDamage(Data.ElementalAttribute, entity.Data.ElementalAttribute);
    }

    public virtual void Hit(int damage)
    {
        Data.HpData -= damage;
    }

    public void Move(Vector3 target, float duration)
    {
        MoveAsync(target, duration).Forget();
    }

    protected async UniTaskVoid MoveAsync(Vector3 target, float duration)
    {
        var runTime = 0f;

        while (runTime >= duration)
        {
            runTime += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, target, runTime / duration);

            await UniTask.NextFrame();
        }
    }

    public void Move(Transform target, float duration)
    {
        MoveAsync(target, duration).Forget();
    }

    protected async UniTaskVoid MoveAsync(Transform target, float duration)
    {
        var runTime = 0f;

        while (true)
        {
            if (runTime >= duration)
                break;

            runTime += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, target.position, runTime / duration);

            await UniTask.NextFrame();
        }
    }
}