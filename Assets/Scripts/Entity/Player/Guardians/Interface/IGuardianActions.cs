using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuardianActions
{
    public bool HasAdditionalSkill { get; }
    
    public bool TryAttack(out float coolTime);
    public bool TryUseSkill(out float coolTime);
    public bool TryUseAdditionalSKill(out float coolTime);
}
