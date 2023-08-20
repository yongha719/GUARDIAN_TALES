using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;


public abstract class Player : MonoBehaviour
{
    [SerializeField]
    protected PlayerData playerData;

    [Tooltip("몇번째 공격인지 나타냄")]
    protected int attackPatternCount = 1;

    [Tooltip("공격 딜레이")]
    public virtual float attackDelay { get; }

    [field: SerializeField, Tooltip("마지막으로 공격한 시간")]
    protected virtual float lastAttackTime { get; set; }

    
    protected SpriteRenderer spriteRenderer;
    
    protected virtual void Start()
    {
        playerData = GameManager.Instance.PlayerData;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void FixedUpdate()
    {
        // Move
        var dir = InputManager.Instance.Dir;

        transform.Translate(dir * (playerData.Speed.Value * Time.deltaTime));

        if (dir.x < 0)
        {
            
        }
    }

    public void TryAttack()
    {
        if (CanAttack())
        {
            // attackPatternCount++;
            Attack();
        }
    }

    protected virtual bool CanAttack()
    {
        if (Time.time - lastAttackTime >= attackDelay)
        {
            // 바로 공격못하게 막음
            // 공격 애니메이션이 끝나야 쿨타임 시작
            lastAttackTime *= 2;
            return true;
        }

        return false;
    }

    protected abstract void Attack();


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        print($"{other.name} : 감지");

        if (other.CompareTag("Enemy"))
        {
            print("적 공격");
        }
    }
}