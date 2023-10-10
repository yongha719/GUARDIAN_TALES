using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Marina : Guardian
{
    [Header(nameof(Marina))]
    private int m_bbracjji;


    protected override int minAttackCount => 1;
    protected override int maxAttackCount => 2;

    public override bool HasAdditionalSkill => true;

    [Space]

    [SerializeField, Tooltip("추가 스킬 갈고리")]
    private GameObject anchor;

    [SerializeField, Tooltip("닻 돌리고 있을 시간")]
    private float anchorRotationTime;

    [SerializeField, Tooltip("닻 돌아가는 속도")]
    private float anchorRotationSpeed;

    [SerializeField, Tooltip("닻 돌아가는 회전값")]
    private float anchorRotationValue;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        print("공격");
        guardianData.PlayerWeapon.SetAttackAnimator(AttackPatternCount);
    }

    protected override void AdditionalSkill()
    {
        base.AdditionalSkill();
    }

    protected override async UniTaskVoid AddtionalSkillAsync()
    {
        // 닻 축아래에 닻이 있는 구조
        var anchorAxis = Instantiate(this.anchor, transform.position, Quaternion.identity, transform);

        var anchor = anchorAxis.transform.GetChild(0);

        SetAnchorPositionAndRotate(anchor);

        var startTime = Time.time;

        while (true)
        {
            var elapsedTime = Time.time - startTime;

            if (elapsedTime >= anchorRotationTime)
                break;

            anchorAxis.transform.Rotate(0, 0, -anchorRotationSpeed);
            anchor.Translate(Vector2.left * (anchorRotationValue * Time.deltaTime));

            await UniTask.NextFrame();
        }

        // TODO : 가장 가까운 적 찾아서 닻으로 공격
        var nearestEnemy = GameManager.Instance.GetnearestEnemy();



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