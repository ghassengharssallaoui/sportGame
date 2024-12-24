using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsManager : MonoBehaviour
{
    public static TeamsManager Instance { get; private set; }

    [SerializeField]
    private Team[] teams;

    public int PlayerOneIndex { get; set; } = 0;
    public int PlayerTwoIndex { get; set; } = 0;

    public Team[] Teams => teams;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }

    public Team GetPlayerOneTeam()
    {
        return teams[PlayerOneIndex];
    }

    public Team GetPlayerTwoTeam()
    {
        return teams[PlayerTwoIndex];
    }
}
