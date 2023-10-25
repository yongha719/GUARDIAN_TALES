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

        MoveToNearestEnemy();
        Weapon.SetAttackAnimator(AttackPatternCount);
    }


    void IGuardianAdditionalSkill.AdditionalSkill()
    {
        IGuardianAdditionalSkill additionalSkill = this;

        additionalSkill.AddtionalSkillAsync().Forget();

        print("Excute AdditionalSkill Method");
    }

    async UniTaskVoid IGuardianAdditionalSkill.AddtionalSkillAsync()
    {
        // TODO : 닻 스크립트만들고 로직 이관하기
        // 닻 축 자식으로 닻이 있는 구조
        var anchorAxis = Instantiate(this.anchor, transform.position, Quaternion.identity, transform);
        print($"생성 : {anchorAxis.name}");

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

        Move(nearestEnemy.transform.position, 0.1f);

        await UniTask.Delay(100);

        print($"닻으로 공격한 적 : {nearestEnemy.name}");
        nearestEnemy.Data.HpData -= AdditionalSkillDamage;

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