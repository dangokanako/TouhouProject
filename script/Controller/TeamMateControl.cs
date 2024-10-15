using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMateControl : MonoBehaviour
{

    public static TeamMateControl instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); return;
        }
        else
        {
            instance = this;
        }

        TeamMateControl.instance.teamMates.Add(GetComponentInChildren<Marisa_TeamMateClass>(true));

    }


    public List<TeamBaseClass> teamMates = new List<TeamBaseClass>();


    /// <summary>
    /// 激活队友
    /// </summary>
    /// <param name="teamMate"></param>
    public void ActiveTeamMate(int id)
    {
        if (TeamMateControl.instance.teamMates[id] != null)
        {
            TeamMateControl.instance.teamMates[id].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Team mate with id " + id + " is null");
        }
    }
}
