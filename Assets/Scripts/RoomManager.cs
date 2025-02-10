using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static int currentRoomNumber = 1;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadNextRoom()
    {
        currentRoomNumber++;
        SceneManager.LoadScene("Room" + currentRoomNumber);
    }
}
