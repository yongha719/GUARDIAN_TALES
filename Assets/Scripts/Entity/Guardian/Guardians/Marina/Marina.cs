using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using UnityEditor;

public class Marina : Guardian, IGuardianAdditionalSkill
{
    [Header(nameof(Marina))]
    private int m_bbracjji;
    protected override int minAttackCount => 1;
    protected override int maxAttackCount => 2;

    public int AdditionalSkillDamage => Data.Damage * 3;

    [Space(20f, order = 0)]
    [BoxHeader("추가 스킬", 4)]
    [SerializeField, Tooltip("추가 스킬 닻 프리팹")]
    private GameObject anchor;

    [SerializeField, Tooltip("닻 돌리고 있을 시간")]
    private float timeToRotateAnchor;

    [SerializeField, Tooltip("닻 돌아가는 속도")]
    private float anchorRotationSpeed;

    [SerializeField, Tooltip("닻 돌아가는 회전값")]
    private float anchorRotationValue;

    protected override void Start()
    {
        base.Start();

        print(guardianData.Damage - 40);
    }

    protected override void Attack()
    {
        print("공격");
        Weapon.SetAttackAnimator(AttackPatternCount);
    }


    void IGuardianAdditionalSkill.AdditionalSkill()
    {
        AddtionalSkillAsync().Forget();
    }

    async UniTaskVoid IGuardianAdditionalSkill.AddtionalSkillAsync()
    {
        // 닻 축 자식으로 닻이 있는 구조
        var anchorAxis = Instantiate(this.anchor, transform.position, Quaternion.identity, transform);

        var anchor = anchorAxis.transform.GetChild(0);

        SetAnchorPositionAndRotate(anchor);

        var startTime = Time.time;

        while (true)
        {
            var elapsedTime = Time.time - startTime;

            if (elapsedTime >= timeToRotateAnchor)
                break;

            // 축이 돌아가고 닻은 왼쪽으로 이동
            anchorAxis.transform.Rotate(0, 0, -anchorRotationSpeed);
            anchor.Translate(Vector2.left * (anchorRotationValue * Time.deltaTime));

            await UniTask.NextFrame();
        }

        // TODO : 가장 가까운 적 찾아서 닻으로 공격
        var nearestEnemy = GameManager.Instance.GetNearestEnemy();

        print($"닻으로 공격한 적 : {nearestEnemy.name}");
        nearestEnemy.Data.HpData -= AdditionalSkillDamage;

        Destroy(anchorAxis);
    }

    private void SetAnchorPositionAndRotate(Transform anchor)
    {
        var moveDir = InputManager.Instance.PlayerMoveDirType;

        var offset = 0.4f;
        var rotationAngle = 0f;

        switch (moveDir)
        {
            case MoveDirType.Up:
                anchor.position += new Vector3(0, offset, 0);
                rotationAngle = -90;
                break;

            case MoveDirType.Right:
                anchor.position += new Vector3(offset, 0, 0);
                rotationAngle = -180;
                break;

            case MoveDirType.Down:
                anchor.position += new Vector3(0, -offset, 0);
                rotationAngle = -270;
                break;

            case MoveDirType.Left:
                anchor.position += new Vector3(-offset, 0, 0);
                break;
        }

        anchor.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}