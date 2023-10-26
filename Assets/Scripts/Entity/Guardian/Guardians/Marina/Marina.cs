using Cysharp.Threading.Tasks;
using UnityEngine;

public class Marina : Guardian, IGuardianAdditionalSkill
{
    protected override int minAttackCount => 1;
    protected override int maxAttackCount => 2;

    public int AdditionalSkillDamage => Data.Damage * 3;

    public override bool IsProximityUnit => true;

    [Space(15f)]
    [Header("Marina============================")]

    [Header("추가 스킬")]
    [SerializeField, Tooltip("추가 스킬 닻 프리팹")]
    private GameObject anchor;

    public MarinaAnchor test;

    [SerializeField, Tooltip("닻 돌리고 있을 시간")]
    private float timeToRotateAnchor;

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
        MoveToNearestEnemy();
        Weapon.SetAttackAnimator(AttackPatternCount);
    }


    void IGuardianAdditionalSkill.AdditionalSkill()
    {
        IGuardianAdditionalSkill additionalSkill = this;

        additionalSkill.AddtionalSkillAsync().Forget();
    }

    async UniTaskVoid IGuardianAdditionalSkill.AddtionalSkillAsync()
    {
        // TODO : 닻 스크립트만들고 로직 이관하기
        // 닻 축 자식으로 닻이 있는 구조
        var anchorAxis = Instantiate(test, transform.position, Quaternion.identity, transform);

        var anchor = anchorAxis.transform.GetChild(0);

        SetAnchorPositionAndRotate(anchor);

        var marinaAnchor = anchorAxis;

        await marinaAnchor.AdditionalSkillAsync(Data.Damage, timeToRotateAnchor, anchorRotationSpeed, anchorRotationValue);

        Destroy(anchorAxis);
    }

    // 마리나가 바라보는 방향을 기준으로 닻의 위치를 정함
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