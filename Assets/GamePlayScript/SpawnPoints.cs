using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [Tooltip("Player can swpan at these location.")]
    public GameObject SpwanPointContainer;
    private Transform[] SpawnPointsLoc;

    [Tooltip("Player can swpan at these location.")]
    public int spwanLocation =1;
    // Start is called before the first frame update

    private void Awake()
    {
        SpawnPointsLoc = SpwanPointContainer.GetComponentsInChildren<Transform>();

        this.transform.position = SpawnPointsLoc[spwanLocation].transform.position;
        this.transform.rotation = Quaternion.Euler(0, SpawnPointsLoc[spwanLocation].transform.eulerAngles.y, 0);
    }

   
}
