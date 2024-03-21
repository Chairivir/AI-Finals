using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Ranged Default Stats")]
    public float rangedHealth = 40.0f;
    public float rangedDamage = 10.0f;
    public float rangedSpeed = 1.5f;
    public float rangedRange = 25.0f;
    [Header("Warrior Default Stats")]
    public float warriorHealth = 75.0f;
    public float warriorDamage = 30.0f;
    public float warriorSpeed = 0.75f;
    public float warriorRange = 1.1f;
    [Header("Rusher Default Stats")]
    public float rusherHealth = 30.0f;
    public float rusherDamage = 15.0f;
    public float rusherSpeed = 3.0f;
    public float rusherRange = 1.1f;
    [Header("Brawler Default Stats")]
    public float brawlerHealth = 45.0f;
    public float brawlerDamage = 20.0f;
    public float brawlerSpeed = 2.0f;
    public float brawlerRange = 1.1f;
    [Header("Mischellaneous")]
    [Tooltip("The damage will be between unit damage plus and minus the damage range")]
    public float damageRange = 5.0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
