using UniRx;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 maxPosition;
    [SerializeField] private Vector3 winPosition;
    private float progress;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);
        GameEvents.instance.playerSize.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (!GameEvents.instance.gameWon.Value && !GameEvents.instance.gameLost.Value)
                {
                    float progress = (float)(value - 1) / 40;
                    Vector3 currentPos = Vector3.Lerp(minPosition, maxPosition, progress);
                    transform.DOLocalMove(currentPos, 1);
                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    transform.DOLocalMove(winPosition, 1).SetDelay(0.5f);
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }
}