using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void GameStart()
    {
        Time.timeScale = 1;
        GameEvents.instance.gameStarted.SetValueAndForceNotify(true);
    }
}