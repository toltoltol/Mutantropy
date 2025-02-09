using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public float PlayerMoveSpeedMultiplier = 5.0f;
    public float EnemyMoveSpeedMultiplier = 5.0f;

    private static GameMaster instance;

    //public GameObject walls;
    public GameObject ClosedGates;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        int enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesCount <= 0)
        {
            //Destroy(walls);
            Destroy(ClosedGates);
        }
            
    }
    

    public static float GetPlayerMoveSpeedMultiplier()
    {
        return instance.PlayerMoveSpeedMultiplier;
    }

    public static float GetEnemyMoveSpeedMultiplier()
    {
        return instance.EnemyMoveSpeedMultiplier;
    }
}
