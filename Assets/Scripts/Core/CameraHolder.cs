using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 initRotation;

    private void Awake()
    {
        initRotation = transform.eulerAngles;
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);

        if (GameEvents.instance.gameWon.Value || GameEvents.instance.gameLost.Value) return;
        transform.eulerAngles = new Vector3(player.eulerAngles.x + initRotation.x, player.eulerAngles.y + initRotation.y, 0);
    }
}