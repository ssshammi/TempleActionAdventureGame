using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixLi;
using System.Linq;

public class PathRevealPulse : MonoBehaviour
{

    [Tooltip("Player can swpan at these location.")]
    public GameObject RevealObjectsContainer;
    private List<GameObject> RevealObjectGameObjects;

   // [Tooltip("USe refence of  AudioPlayer._Instance.Play(audioClip, IdTag.Audio.SoundEffect); ")]
   // public GameObject AudioPlayerPreb;


    [Tooltip("USe refence of audio clip")]
    public AudioClip pulseSound;


    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer._Instance.Play(pulseSound, IdTag.Audio.SoundEffect);
    }

    private void Awake()
    {
        RevealObjectGameObjects = new List<GameObject>();
        foreach (Transform FloathingObjects in this.transform)
        {
            // Is my ID (the parent) not the same as the component's gameObject
            //if (FloathingObjects.gameObject.GetInstanceID() != GetInstanceID())
            //{
                RevealObjectGameObjects.Add(FloathingObjects.gameObject);
           // }
        }
       Debug.Log(" "+RevealObjectGameObjects.Count);




       // this.transform.position = RevealObjectGameObjects[0].transform.position;
       //this.transform.rotation = Quaternion.Euler(0, SpawnPointsLoc[spwanLocation].transform.eulerAngles.y, 0);
    }
    
    


    // Update is called once per frame
    void Update()
    {
        // if player if on the object show it awlays 
    }
}
