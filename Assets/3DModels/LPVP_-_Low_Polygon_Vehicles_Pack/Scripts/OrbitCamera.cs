using UnityEngine;
using System.Collections;
     
    [AddComponentMenu("Control/Orbit Camera")]
    public class OrbitCamera : MonoBehaviour
    {
    public Transform target;
	public bool autoRotateOn = false;
    public bool autoRotateReverse = false;
    public bool enableShiftRotate = false;
    public float autoRotateSpeed = 1f;
    float originalAutoRotateSpeed;
    public float autoRotateSpeedFast = 5f;
    float autoRotateValue = 1;
    public float distance = 1.5f;
    public float distanceSpeed = 20f;
    public float distanceMin = 1f;
    public float distanceMax = 3f;
	public float speed = 1;
	public float adjusmentX = 0;
	public float adjusmentY = 0;
	public float adjusmentZ = 0;

#if UNITY_ANDROID
    public float xSpeed = 1.0f;
    public float ySpeed = 1.0f;
#else
    public float xSpeed = 15.0f;
    public float ySpeed = 15.0f;
#endif
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
     
     
    public float smoothTime = 2f;
	public float autoTimer = 5f;
     
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
     
    float velocityX = 0.0f;
    float velocityY = 0.0f;
	bool faster;
	private bool rkeyActive;
	public bool collision = false;
    private Vector3 cameraRotation;
    private Vector3 cameraSyncRotation;

    public Camera cameraSync;
	public bool syncWithCamera;



void Start()
{
	rkeyActive = autoRotateOn;
	autoRotateValue = 1;
    Vector3 angles = transform.eulerAngles;
    rotationYAxis = angles.y;
    rotationXAxis = angles.x;

    // Debug.Log(rotationYAxis);
    // Debug.Log(rotationXAxis);

	originalAutoRotateSpeed = autoRotateSpeed;
    if (GetComponent<Rigidbody>())
    {
		GetComponent<Rigidbody>().freezeRotation = true;
    }

    cameraRotation = transform.eulerAngles;


}
	
	
private void Update()
{

	if (autoRotateOn)
        {
			velocityX += (autoRotateSpeed * autoRotateValue) * Time.deltaTime;
        }
	if (Input.GetKeyUp ("r") && autoRotateOn == false){
		if(enableShiftRotate  == true){
				autoRotateOn = true;
		}

		rkeyActive = true;
	}else if(Input.GetKeyUp ("r") && autoRotateOn == true){
		autoRotateOn = false;
		rkeyActive = false;
	}
	
	if (Input.GetKeyDown(KeyCode.LeftShift) && (!faster))
	{
		faster = true;
		autoRotateSpeed = autoRotateSpeedFast;
		if(enableShiftRotate  == true){
				autoRotateOn = true;
		}
	}

	
	if (Input.GetKeyUp(KeyCode.LeftShift) && (faster))
	{
		faster = false;
		autoRotateSpeed = originalAutoRotateSpeed;
		if (rkeyActive == false){
			autoRotateOn = false;
		}
	}
	
	if (autoRotateReverse == true)
	{
		autoRotateValue = -1;
    }
	else
	{
		autoRotateValue = 1;
	}

    if (target != null)
    {

     	cameraSyncRotation = cameraSync.GetComponent<Camera>().transform.eulerAngles;

    	if(syncWithCamera == true){

			if (this.GetComponent<Camera>().enabled == false){
				rotationYAxis = cameraSyncRotation.y;
				rotationXAxis = cameraSyncRotation.x;
				velocityX = 0.0f;
				velocityY = 0.0f;
			}

			if (Input.GetKeyDown(KeyCode.LeftBracket) || Input.GetKeyDown(KeyCode.RightBracket)){
				rotationYAxis = cameraSyncRotation.y;
				rotationXAxis = cameraSyncRotation.x;
				velocityX = 0.0f;
				velocityY = 0.0f;
			}

    	}else{
			if (this.GetComponent<Camera>().enabled == false){
				rotationYAxis = cameraRotation.y;
				rotationXAxis = cameraRotation.x;
				velocityX = 0.0f;
				velocityY = 0.0f;
			}

    	}

		if (Input.GetKeyDown(KeyCode.Space)){
			rotationYAxis = cameraRotation.y;
			rotationXAxis = cameraRotation.x;
			velocityX = 0.0f;
			velocityY = 0.0f;
		}



	}

}

	 
void LateUpdate()
{
    if (target != null)
    {
		if (Input.GetMouseButton(0))
		{
			velocityX += xSpeed * Input.GetAxis("Mouse X") * speed * 0.02f;
			velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
			
		}
		else if (Input.touchCount > 0 )
		{
		velocityX += xSpeed * Input.GetTouch(0).deltaPosition.x * speed * 0.02f;
		velocityY += ySpeed * Input.GetTouch(0).deltaPosition.y * 0.02f;
		}
		// if (Input.GetKeyUp (KeyCode.Space)){  // stop velocity
		// 	velocityX = 0.0f;
		// 	velocityY = 0.0f;
		// }
		
		rotationYAxis += velocityX;
		rotationXAxis -= velocityY;
     
		rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);

		Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
		Quaternion rotation = toRotation;


		distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * distanceSpeed, distanceMin, distanceMax);
     
		if (collision == true ){
				RaycastHit hit;
				if (Physics.Linecast(target.position, transform.position, out hit))
				{	
					distance -= hit.distance;
				}
		}
		Vector3 adjustmentPosition = new Vector3(adjusmentX, adjusmentY, adjusmentZ);

		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position + adjustmentPosition;

		transform.position = position;
		transform.rotation = rotation;
     
		velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
		velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);





	}else
	{
	    Debug.LogWarning("Orbit Camera - No Target Set");
	}





}



     
    public static float ClampAngle(float angle, float min, float max)
    {
		if (angle < -360F)
		angle += 360F;
		if (angle > 360F)
		angle -= 360F;
		return Mathf.Clamp(angle, min, max);
    }
	
	
}
     
     

