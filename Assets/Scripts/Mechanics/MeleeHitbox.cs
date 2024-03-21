using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MeleeHitbox : MonoBehaviour
{
    UnitManager unitManager;
    public List<UnitManager> enemies;

    void Start()
    {
        unitManager = GetComponentInParent<UnitManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(unitManager.enemyTeam[unitManager.returnTeamAffliation]))
        {
            enemies.Add(other.gameObject.GetComponent<UnitManager>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemies.Remove(other.gameObject.GetComponent<UnitManager>());
    }

    public void OnHit()
    {
        foreach (UnitManager enemy in enemies)
        {
            enemy.currentTarget = unitManager.gameObject;
            enemy.health -= Random.Range(unitManager.unitDamageRange[0], unitManager.unitDamageRange[1]);//unitManager.damage;
        }
    }
}
