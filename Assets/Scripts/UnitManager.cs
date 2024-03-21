using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum Unit_Types
{
    Ranged,
    Warrior,
    Rusher,
    Brawler
}

public enum Team
{
    TeamA,
    TeamB
}

public enum TeamCount
{
    TeamACount,
    TeamBCount
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class UnitManager : MonoBehaviour
{
    public string[] enemyTeam = new string[2] {"TeamA", "TeamB"};
    public Dictionary<Unit_Types,int> unitIndices = new Dictionary<Unit_Types, int>()
    {
        {Unit_Types.Ranged, 0},
        {Unit_Types.Warrior, 1},
        {Unit_Types.Rusher, 2},
        {Unit_Types.Brawler, 3}
    };

    [Header("Battle")]
    public TeamCount enemyTeamCount;
    public GameObject currentTarget;

    [Header("Unit Information")]
    public float maxHealth;
    public float health;
    public float damage;
    public float[] unitDamageRange = new float[] {0,0};
    public float speed;
    public float range;
    public Team teamAffliation;
    public int returnTeamAffliation;
    public Unit_Types unitType;

    [Header("State Machine")]
    public string currentState;

    [Header("References")]
    public List<GameObject> unitModels;
    public Animator animator;
    public NavMeshAgent agent;

    void Start()
    {
        Action<Unit_Types> activateUnit = (Unit_Types unitType) =>
        {
            GameObject unitToActivate = unitModels[unitIndices[unitType]];

            foreach (GameObject unit in unitModels)
            {
                if (unit != unitToActivate)
                    unit.SetActive(false);
            }

            unitToActivate.SetActive(true);
            animator = unitToActivate.GetComponent<Animator>();
        };

        // Set NavMesh Agent
        agent = GetComponent<NavMeshAgent>();

        // Set Unit's Properties
        SetUnitProperties();

        activateUnit(unitType);
    }

    void LateUpdate()
    {
        // Check if animator has a reference
        if (animator == null)
        {
            currentState = "Animator reference not set";
            return;
        }

        AnimationClip currentClip = GetCurrentAnimatorClip(animator, 0);

        // Check if there's animation
        if (currentClip == null)
        {
            currentState = "No animation playing";
            return;
        }

        currentState = currentClip.name;
    }

    void SetUnitProperties()
    {
        returnTeamAffliation = (int)teamAffliation;

        switch(unitType)
        {
            case Unit_Types.Ranged:
                health = GameManager.Instance.rangedHealth;
                damage = GameManager.Instance.rangedDamage;
                speed = GameManager.Instance.rangedSpeed;
                range = GameManager.Instance.rangedRange;
                break;
            case Unit_Types.Warrior:
                health = GameManager.Instance.warriorHealth;
                damage = GameManager.Instance.warriorDamage;
                speed = GameManager.Instance.warriorSpeed;
                range = GameManager.Instance.warriorRange;
                break;
            case Unit_Types.Rusher:
                health = GameManager.Instance.rusherHealth;
                damage = GameManager.Instance.rusherDamage;
                speed = GameManager.Instance.rusherSpeed;
                range = GameManager.Instance.rusherRange;
                break;
            case Unit_Types.Brawler:
                health = GameManager.Instance.brawlerHealth;
                damage = GameManager.Instance.brawlerDamage;
                speed = GameManager.Instance.brawlerSpeed;
                range = GameManager.Instance.brawlerRange;
                break;
        }

        unitDamageRange[0] = damage - GameManager.Instance.damageRange;
        unitDamageRange[1] = damage + GameManager.Instance.damageRange;
        maxHealth = health;
        agent.speed = speed;
        agent.stoppingDistance = range;
    }

    private AnimationClip GetCurrentAnimatorClip(Animator anim, int layer)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(layer);
        return anim.GetCurrentAnimatorClipInfo(layer)[0].clip;
    }
}
