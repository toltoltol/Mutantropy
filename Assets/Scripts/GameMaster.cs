using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public float PlayerMoveSpeedMultiplier = 5.0f;
    public float EnemyMoveSpeedMultiplier = 5.0f;

    private static GameMaster instance;

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

    public static float GetPlayerMoveSpeedMultiplier()
    {
        return instance.PlayerMoveSpeedMultiplier;
    }

    public static float GetEnemyMoveSpeedMultiplier()
    {
        return instance.EnemyMoveSpeedMultiplier;
    }
}
