using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTreads : MonoBehaviour
{
    public GameObject tankTread;
    //
    public Transform treadLeft;
    public Transform treadRight;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.parent);
    }

    // Update is called once per frame
    void Update() {
        SpawnTreads();
    }

    void SpawnTreads()
    {
        GameObject tLeft = (GameObject)Instantiate(tankTread, treadLeft.position, transform.rotation * tankTread.transform.rotation);
        GameObject tRight = (GameObject)Instantiate(tankTread, treadRight.position, transform.rotation * tankTread.transform.rotation);

        Destroy(tLeft, 2f);
        Destroy(tRight, 2f);
    }
}
