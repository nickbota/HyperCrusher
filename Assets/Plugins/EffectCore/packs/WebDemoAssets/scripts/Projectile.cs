using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float disappearAfterTime = 3;
    private float disappearTimer = 0;

    private bool isActive = false;

  //  private MeshRenderer meshRenderer;

    private Vector3 direction;
    private Vector3 startPosition;

    private float movementSpeed = 1;

    private Vector3 targetPosition;

    private float distanceToTarget;
    private float movementValue;

	// Use this for initialization
	void Start ()
	{
        // We cache this for performance reasons, GetComponent is slow to do realtime
        // meshRenderer = GetComponent<MeshRenderer>(); 
    //    meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (isActive) // Only update stuff if we're alive
	    {
	        disappearTimer += Time.deltaTime; // Increase disappear timer
	        if (disappearTimer > disappearAfterTime) // If we're alive too long, get rekt
	        {
	            disappearTimer = 0; // Reset timer
	            isActive = false; // Is not active anymore
	    //        meshRenderer.enabled = false; // Disable meshrender so it's invisible
	        }

	        
            // 1/distanceToTarget is the calculation to move 1 unit per second, movementspeed defines how many units per second you want to move
	        movementValue += (1/distanceToTarget*movementSpeed) * Time.deltaTime;
	        if (movementValue > 1)
	        {
	            movementValue = 1;
                Explode();
	        }
	        Move();
	        
	    }
	}

    void Move()
    {
        // lerp goes from 0 to 1, 0 is startPosition, 1 is the targets position;
        transform.position = Vector3.Lerp(startPosition, targetPosition, movementValue);
    }

    void MoveWithoutTargetHit()
    {
        transform.position += direction.normalized * movementSpeed;
    }

    public void Fire(Vector3 target, Vector3 spawnPosition, Vector3 Direction, float speed)
    {
        if (isActive) // If we're active, just return so we don't execute any code
            return;
        
        isActive = true; // Set active
        disappearTimer = 0; // Reset timer just in case it's not reset
        transform.position = spawnPosition; // set spawn position
       // meshRenderer.enabled = true; // Enable meshrender so it's visible
        movementSpeed = speed; // Units per second
        direction = Direction.normalized; // Normalize the direction
        targetPosition = target; // Set target transform - Can be null for continous movement without target
        distanceToTarget = Vector3.Distance(targetPosition, transform.position);
        startPosition = spawnPosition;
        movementValue = 0;
    }

    // It gets here after it hits something
    void Explode() 
    {
        disappearTimer = 0; // Reset timer
        isActive = false; // Is not active anymore
     //   meshRenderer.enabled = false; // Disable meshrender so it's invisible
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}