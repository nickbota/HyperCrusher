using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectActor : MonoBehaviour {

    public Transform spawnLocator;

    [System.Serializable]
    public class projectile
    {
        public string name;
        public GameObject bombPrefab;
        public Transform bombPrefabTransform;
    }
    public projectile[] bombList;

    string FauxName;
    public Text UiText;

    public int bombType = 0;

    public float min, max;

    public bool MinorRotate;
    public bool MajorRotate = false;
    int seq = 0;


	// Use this for initialization
	void Start ()
    {
        UiText.text = "#" + (bombType + 1) + ". " + bombList[bombType].name.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Switch(-1);
        }
        if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.E))
        {
            Switch(1);
        }

	    if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
	}

    public void Switch(int value)
    {
        bombType += value;
        if (bombType <= 0)
        {
            bombType = bombList.Length + -1;
        }
        if (bombType >= bombList.Length)
        {
            bombType = 0;
        }

        UiText.text = "#" + (bombType+1) + ". " +  bombList[bombType].name.ToString();

    }

    public void Fire()
    {
        Instantiate(bombList[bombType].bombPrefab, bombList[bombType].bombPrefabTransform.position, bombList[bombType].bombPrefabTransform.rotation);
 

       /* if (Torque)
        {
            rocketInstance.AddTorque(spawnLocator.up * Random.Range(Tor_min, Tor_max));
        }*/
        if (MinorRotate)
        {
            RandomizeRotation();
        }
        if (MajorRotate)
        {
            Major_RandomizeRotation();
        }
    }


    void RandomizeRotation()
    {
        if (seq == 0)
        {
            seq++;
            transform.Rotate(0, 1, 0);
        }
      else if (seq == 1)
        {
            seq++;
            transform.Rotate(1, 1, 0);
        }
      else if (seq == 2)
        {
            seq++;
            transform.Rotate(1, -3, 0);
        }
      else if (seq == 3)
        {
            seq++;
            transform.Rotate(-2, 1, 0);
        }
       else if (seq == 4)
        {
            seq++;
            transform.Rotate(1, 1, 1);
        }
       else if (seq == 5)
        {
            seq = 0;
            transform.Rotate(-1, -1, -1);
        }
    }

    void Major_RandomizeRotation()
    {
        if (seq == 0)
        {
            seq++;
            transform.Rotate(0, 25, 0);
        }
        else if (seq == 1)
        {
            seq++;
            transform.Rotate(0, -50, 0);
        }
        else if (seq == 2)
        {
            seq++;
            transform.Rotate(0, 25, 0);
        }
        else if (seq == 3)
        {
            seq++;
            transform.Rotate(25, 0, 0);
        }
        else if (seq == 4)
        {
            seq++;
            transform.Rotate(-50, 0, 0);
        }
        else if (seq == 5)
        {
            seq = 0;
            transform.Rotate(25, 0, 0);
        }
    }
}
