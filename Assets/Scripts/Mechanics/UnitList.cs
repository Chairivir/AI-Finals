using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    public enum Team
    {
        TeamA,
        TeamB
    }

    public Team teamToList;

    public List<GameObject> unitsInATeam;

    void Start()
    {
        unitsInATeam.AddRange(GameObject.FindGameObjectsWithTag(teamToList.ToString()));
    }
}
