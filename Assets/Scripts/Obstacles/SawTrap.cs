using UnityEngine;
using DG.Tweening;

public class SawTrap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform sawObject;
    [SerializeField] private Transform[] edges;

    [Header("Speed Values")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float delayTime;
    [SerializeField] private float movementSpeed;
    private int currentEdge;

    private void Awake()
    {
        int randEdge = Random.Range(0, edges.Length);
        sawObject.transform.position = edges[randEdge].position;
        currentEdge = System.Convert.ToInt32(!System.Convert.ToBoolean(randEdge));
        MoveToEdge();
    }
    private void Update()
    {
        sawObject.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
    }

    private void MoveToEdge()
    {
        sawObject.DOMove(edges[currentEdge].position, 1 / movementSpeed).SetDelay(delayTime).SetEase(Ease.Linear)
        .OnComplete(()=>
        {
            ChangeDestination();
            MoveToEdge();
        });
    }
    private void ChangeDestination()
    {
        bool edgeBool = System.Convert.ToBoolean(currentEdge);
        currentEdge = System.Convert.ToInt32(!edgeBool);
    }
}