using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void PlayParticle(int _index, Vector3 _position)
    {
        ParticleSystem part = transform.GetChild(_index).GetComponent<ParticleSystem>();
        transform.GetChild(_index).position = _position;
        part.Clear();
        part.Play();
    }
}