using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRecorder : MonoBehaviour {

    public GameObject origin;
    public Stack<Spherical3> mytrail;

    private Vector3 startPos;
    private Spherical3 startSph;
    private Spherical3 sc;

    public bool randomize = false;

    public void StartRecord()
    {
        mytrail.Clear();
        startPos = transform.position;
        startSph = new Spherical3();
        SphereUtility.CartesianToSpherical(startPos - origin.transform.position, out startSph);
        mytrail.Push(startSph);
        StartCoroutine("Recording");
    }

    public void StopRecord()
    {
        StopCoroutine("Recording");
    }

    // Use this for initialization
    void Start () {
        mytrail = new Stack<Spherical3>();
        sc = new Spherical3();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("space") || OVRInput.GetDown(OVRInput.Button.One))
        {
            StartRecord();
              
        } else if (Input.GetKeyDown("s"))
        {
            StopRecord();
        }
    }

    IEnumerator Recording()
    {
        while (true)
        {
            Vector3 where = transform.position - origin.transform.position;

            if (randomize)
            {
                where.x += Random.Range(-0.5f, 0.5f);
                where.y += Random.Range(-0.5f, 0.5f);
                where.z += Random.Range(-0.5f, 0.5f);
            }
            SphereUtility.CartesianToSpherical(where, out sc);
            mytrail.Push(sc);

            Debug.Log(mytrail.Count);

            yield return new WaitForSeconds(0.1f); 
        }
    }
}
