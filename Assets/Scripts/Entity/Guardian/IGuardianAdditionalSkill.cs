using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuardianAdditionalSkill
{
    int AdditionalSkillDamage { get; }

    void AdditionalSkill();

    UniTaskVoid AddtionalSkillAsync();
}
