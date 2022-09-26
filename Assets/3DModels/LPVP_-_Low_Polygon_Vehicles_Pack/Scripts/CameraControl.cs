using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class CameraControl : MonoBehaviour
{
    // Object Script
    public float speed;

    public float shiftPressed;

    private float tempShift;

    private Vector3 cameraRotation;

    private Vector3 move;

    [UnityEngine.Tooltip("Is camera following any object? if False can't use WASD")]
    public bool followCenter;

    [UnityEngine.Tooltip("Control Scroll Speed")]
    [UnityEngine.Range(0f, 20f)]
    public float scrollSpeed;

     //Attribute
    [UnityEngine.Space(10)]
    public float heightMainStart;

    public float heightMainUpdate;

    private bool heightMainBreak;

    //var heightSecondStart: float = 10.0; 
    public float heightSecondUpdate;

    private bool heightSecondBreak;

    [UnityEngine.Space(5)]
    public float heightMin;

    public float heightMax;

    [UnityEngine.Space(20)]
    public Camera mainCamera;

    public GameObject mainCameraObj;

    public Camera secondCamera;

    public GameObject secondCameraObj;

    [UnityEngine.Space(10)]
    public Transform mainCameraTransform;

    [UnityEngine.Space(10)]
    public Transform targetObjectTransform;

    private Vector3 targetObjectTransformVector;

    private Vector3 defaultCameraPosition;

    private Vector3 defaultCameraRotation;

    private Vector3 updatedCamera;

    [UnityEngine.Space(10)]
    public Transform moveToLocation1;

    public Transform moveToLocation2;

    // public var moveToLocation3 : Transform;
    // public var moveToLocation4 : Transform;
    // public var moveToLocation5 : Transform;
    // public var moveToLocation6 : Transform;
    // public var moveToLocation7 : Transform;
    // public var moveToLocation8 : Transform;
    // public var moveToLocation9 : Transform;
    // public var moveToLocation0 : Transform;
    // var defaultMaterial : Shader;
    public virtual void Start()//}
    {
        this.mainCamera.enabled = true;
        this.secondCamera.enabled = false;
        this.mainCameraObj.SetActive(true);
        this.secondCameraObj.SetActive(false);
        this.mainCameraTransform.localPosition = new Vector3(0, 0, 0);
        this.mainCamera.orthographicSize = this.heightMainStart;
        this.secondCamera.fieldOfView = this.heightMainStart;
        this.targetObjectTransformVector = this.targetObjectTransform.position;
        //if(mainCamera.enabled == true){
        this.defaultCameraPosition = this.mainCameraTransform.transform.position;
        this.defaultCameraRotation = this.mainCameraTransform.transform.eulerAngles;

        {
            float _2 = this.defaultCameraRotation.y;
            Vector3 _3 = this.mainCameraTransform.eulerAngles;
            _3.y = _2;
            this.mainCameraTransform.eulerAngles = _3;
        }
        this.tempShift = this.speed;
    }

    /* 	if (Input.GetKeyDown(KeyCode.LeftBracket )){
 		mainCameraTransform.eulerAngles.y = updatedCamera.y + -90;
 		//Debug.Log('Pressed -90');
 		Debug.Log(mainCameraTransform.eulerAngles.y);
 	}
 	if (Input.GetKeyDown(KeyCode.RightBracket )){
 		mainCameraTransform.eulerAngles.y = updatedCamera.y + 90;
 		//Debug.Log('Pressed +90');
  		Debug.Log(mainCameraTransform.eulerAngles.y);
 	}*/    public virtual void Update()///////////////////////
    {
        if (Input.GetKeyDown(KeyCode.Tab) && (this.mainCamera.enabled == true))
        {
            //Debug.Log('Main Camera Enabled');
            this.mainCamera.enabled = false;
            this.secondCamera.enabled = true;
            this.mainCameraObj.SetActive(false);
            this.secondCameraObj.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab) && (this.mainCamera.enabled == false))
            {
                //Debug.Log('Main Camera Disabled');
                this.mainCamera.enabled = true;
                this.secondCamera.enabled = false;
                this.mainCameraObj.SetActive(true);
                this.secondCameraObj.SetActive(false);
            }
        }
        //mainCamera.orthographicSize += Mathf.Round((Input.GetAxis("Mouse ScrollWheel") * scrollSpeed) * 10)/10;
        this.mainCamera.orthographicSize = this.mainCamera.orthographicSize + ((Mathf.Round(-Input.GetAxis("Mouse ScrollWheel") * 10) / 10) * this.scrollSpeed);
        this.mainCamera.orthographicSize = Mathf.Clamp(this.mainCamera.orthographicSize, this.heightMin, this.heightMax);
        this.secondCamera.fieldOfView = this.secondCamera.fieldOfView + ((Mathf.Round(-Input.GetAxis("Mouse ScrollWheel") * 10) / 15) * this.scrollSpeed);
        this.secondCamera.fieldOfView = Mathf.Clamp(this.secondCamera.fieldOfView, this.heightMin, this.heightMax);
        this.heightMainUpdate = this.mainCamera.orthographicSize;
        this.heightSecondUpdate = this.secondCamera.fieldOfView;
        // if(Input.GetKeyDown(KeyCode.Q)){
        // 	mainCamera.SetReplacementShader(defaultMaterial, "RenderType");
        // }
        this.cameraRotation.y = this.mainCameraTransform.eulerAngles.y;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.speed = this.shiftPressed;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                this.speed = this.tempShift;
            }
        }
        if (this.followCenter == true) // Is camera following any object?
        {
            if ((this.cameraRotation.y >= 0) && (this.cameraRotation.y <= 89))
            {
                this.move = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
            }
            else
            {
                if ((this.cameraRotation.y >= 90) && (this.cameraRotation.y <= 179))
                {
                    this.move = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
                }
                else
                {
                    if ((this.cameraRotation.y >= 180) && (this.cameraRotation.y <= 269))
                    {
                        this.move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    }
                    else
                    {
                        if ((this.cameraRotation.y >= 270) && (this.cameraRotation.y <= 360))
                        {
                            this.move = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Space)) // reset position
            {
                //Debug.Log('Pressed');
                //targetObjectTransform.position = Vector3(0,0,0);
                this.targetObjectTransform.position = this.targetObjectTransformVector;
                this.mainCameraTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) // reset position
            {
                this.targetObjectTransform.position = this.moveToLocation1.position;
            }
            // mainCameraTransform.eulerAngles =  Vector3(0,0,0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) // reset position
            {
                this.targetObjectTransform.position = this.moveToLocation2.position;
            }
        }
        // mainCameraTransform.eulerAngles =  Vector3(0,0,0);
        this.targetObjectTransform.position = this.targetObjectTransform.position + ((this.move * this.speed) * Time.deltaTime);
        this.mainCameraTransform.position = this.targetObjectTransform.position + this.defaultCameraPosition;
        this.updatedCamera.y = this.mainCameraTransform.eulerAngles.y;
        ///////////////////////
        if ((this.mainCameraTransform.eulerAngles.y == 0) && (this.cameraRotation.y <= 89)) // Value 0
        {
            if (Input.GetKeyDown(KeyCode.LeftBracket)) // Negatve 90
            {
                this.mainCameraTransform.eulerAngles = new Vector3(0, 270, 0);
            }
            //Debug.Log(mainCameraTransform.eulerAngles.y);
            if (Input.GetKeyDown(KeyCode.RightBracket)) // Positive 90
            {
                this.mainCameraTransform.eulerAngles = new Vector3(0, 90, 0);
            }
        }
        else
        {
            //Debug.Log(mainCameraTransform.eulerAngles.y);
            if ((this.mainCameraTransform.eulerAngles.y == 90) && (this.cameraRotation.y <= 179)) // Value 90
            {
                if (Input.GetKeyDown(KeyCode.LeftBracket))
                {
                    this.mainCameraTransform.eulerAngles = new Vector3(0, 0, 0);
                }
                //Debug.Log(mainCameraTransform.eulerAngles.y);
                if (Input.GetKeyDown(KeyCode.RightBracket))
                {
                    this.mainCameraTransform.eulerAngles = new Vector3(0, 180, 0);
                }
            }
            else
            {
                //Debug.Log(mainCameraTransform.eulerAngles.y);
                if ((this.mainCameraTransform.eulerAngles.y >= 180) && (this.mainCameraTransform.eulerAngles.y <= 269)) // Value 180
                {
                    if (Input.GetKeyDown(KeyCode.LeftBracket))
                    {
                        this.mainCameraTransform.eulerAngles = new Vector3(0, 90, 0);
                    }
                    //Debug.Log(mainCameraTransform.eulerAngles.y);
                    if (Input.GetKeyDown(KeyCode.RightBracket))
                    {
                        this.mainCameraTransform.eulerAngles = new Vector3(0, 270, 0);
                    }
                }
                else
                {
                    //Debug.Log(mainCameraTransform.eulerAngles.y);
                    if ((this.mainCameraTransform.eulerAngles.y == 270) && (this.mainCameraTransform.eulerAngles.y <= 360)) // Value -90 / 270
                    {
                        if (Input.GetKeyDown(KeyCode.LeftBracket)) // Negative 180
                        {
                            this.mainCameraTransform.eulerAngles = new Vector3(0, 180, 0);
                        }
                        //Debug.Log(mainCameraTransform.eulerAngles.y);
                        if (Input.GetKeyDown(KeyCode.RightBracket)) // Value 0
                        {
                            this.mainCameraTransform.eulerAngles = new Vector3(0, 0, 0);
                        }
                    }
                }
            }
        }
    }

    public CameraControl()
    {
        this.speed = 10f;
        this.shiftPressed = 20f;
        this.tempShift = 1f;
        this.scrollSpeed = 5f;
        this.heightMainStart = 15f;
        this.heightMin = 5f;
        this.heightMax = 15f;
    }

}