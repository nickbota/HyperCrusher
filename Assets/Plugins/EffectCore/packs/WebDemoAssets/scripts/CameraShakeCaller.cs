using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeCaller : MonoBehaviour {

    public float ProjectileShakeDuration = 0.08f;
    public float ProjectileShakeAmount = 0.1f;


    // Use this for initialization
    void Start ()
    {
        CameraShake.shakeDuration = ProjectileShakeDuration;
        CameraShake.shakeAmount = ProjectileShakeAmount;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
