using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class particleColorChangerMaster : MonoBehaviour {

    [System.Serializable]
    public class colorChange
    {
        public string Name;
        public ParticleSystem[] colored_ParticleSystem;
        public Gradient Gradient_custom; 
    }
    public float Speed_custom = 1;
    public colorChange[] colorChangeList;

    public bool applyChanges = false;
    public bool Keep_applyChanges = false;

    void Start()
    {

    }

    void Update()
     {  
        if (applyChanges || Keep_applyChanges)
        {
            for (int i = 0; i < colorChangeList.Length; i++)
            {
                for (int a = 0; a < colorChangeList[i].colored_ParticleSystem.Length; a++)
                {
                    var col = colorChangeList[i].colored_ParticleSystem[a].colorOverLifetime;
                    col.color = colorChangeList[i].Gradient_custom;
                    colorChangeList[i].colored_ParticleSystem[a].playbackSpeed = Speed_custom;
                }
            }
            applyChanges = false;
        }
     }
  
}
