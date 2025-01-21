using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////// INFORME QU'IL PEUT ETRE ENDOMMAGE //////////////////////////////////////////////////////////////////////////

public interface IDamageable
{
    void SetDamage(int damage, IDamageable attacker);
}