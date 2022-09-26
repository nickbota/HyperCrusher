using UniRx;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerSize : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private TextMeshPro sizeText;
    private float currentSize = 1;

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
                float size = 1 + (value - 1) * 0.1f;

                if (size != currentSize)
                {
                    sizeText.text = value.ToString();
                    sizeText.transform.parent.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.25f);
                    transform.GetChild(0).DOScale(new Vector3(size, size, size), 0.5f).SetEase(Ease.OutBack);
                    currentSize = size;
                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    sizeText.transform.parent.DOScale(Vector3.zero, 0.25f);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    sizeText.transform.parent.DOScale(Vector3.zero, 0.25f);
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }
}