using UnityEngine;
using DG.Tweening;

public class FingerAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform[] destinations;
    [SerializeField] private float speed;
    private RectTransform rect;
    private bool movingLeft;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        int destination = System.Convert.ToInt32(movingLeft);
        rect.DOMoveX(destinations[destination].position.x, 
            Vector2.Distance(rect.position, destinations[destination].position) / speed).SetEase(Ease.OutSine)
            .OnComplete(()=>
            {
                movingLeft = !movingLeft;
                MoveToDestination();
            });
    }
}