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
      
    }

    private void Awake()
    {
        RevealObjectGameObjects = new List<GameObject>();
        foreach (Transform FloathingObjects in this.transform)
        {
            // Is my ID (the parent) not the same as the component's gameObject
            //if (FloathingObjects.gameObject.GetInstanceID() != GetInstanceID())
            //{
            FloathingObjects.gameObject.GetComponent<MeshRenderer>().enabled = false;
            RevealObjectGameObjects.Add(FloathingObjects.gameObject);
            
            // }
        }
       Debug.Log(" "+RevealObjectGameObjects.Count);




       // this.transform.position = RevealObjectGameObjects[0].transform.position;
       //this.transform.rotation = Quaternion.Euler(0, SpawnPointsLoc[spwanLocation].transform.eulerAngles.y, 0);
    }

    public void revealPath()
    {
        int floatIndx = 0;
        AudioPlayer._Instance.Play(pulseSound, IdTag.Audio.SoundEffect);
        foreach (GameObject floatingRocks in RevealObjectGameObjects) {
            StartCoroutine(RevealObject(floatingRocks, 0.2f * floatIndx)); // maybe add sin wave?
         
                    floatIndx++;
              
        }

    }

    private IEnumerator RevealObject(GameObject floatingRocks, float delay )
    {

        yield return new WaitForSeconds(seconds: delay);

        floatingRocks.GetComponent<MeshRenderer>().enabled = true;
    }



    public void hidePath()
    {

        foreach (GameObject floatingRocks in RevealObjectGameObjects)
        {
            floatingRocks.GetComponent<MeshRenderer>().enabled = false;
        }
        // Hide the path and also stop the sound.
        // AudioPlayer._Instance.Play(pulseSound, IdTag.Audio.SoundEffect);
    }


    // Update is called once per frame
    void Update()
    {
        // if player if on the object show it awlays 
    }
}
