using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyAI> enemiesList;

    private void Start()
    {
        FillInTheList();
    }

    private void FillInTheList()
    {
        enemiesList = new List<EnemyAI>(FindObjectsOfType<EnemyAI>());
    }

    public List<EnemyAI> GetList()
    {
        return enemiesList;
    }
}
