using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUnitCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI teamACounter;
    [SerializeField] TextMeshProUGUI teamBCounter;
    [SerializeField] GameObject teamAVictoryScreen;
    [SerializeField] GameObject teamBVictoryScreen;

    int teamACount;
    int teamBCount;

    void Start()
    {
        teamACount = GameObject.FindGameObjectsWithTag("TeamA").Length;
        teamBCount = GameObject.FindGameObjectsWithTag("TeamB").Length;

        teamACounter.text = teamACount.ToString();
        teamBCounter.text = teamBCount.ToString();
    }

    public void UpdateCount(int increment, int teamAffliation)
    {
        switch (teamAffliation)
        {
            case 0:
                teamACount += increment;
                teamACounter.text = teamACount.ToString();
                break;
            case 1:
                teamBCount += increment;
                teamBCounter.text = teamBCount.ToString();
                break;
        }

        if (teamACount <= 0)
        {
            teamBVictoryScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        if (teamBCount <= 0)
        {
            teamAVictoryScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
