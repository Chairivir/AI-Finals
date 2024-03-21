using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    public MeleeHitbox hitbox;

    void hit()
    {
        hitbox.OnHit();
    }
}
