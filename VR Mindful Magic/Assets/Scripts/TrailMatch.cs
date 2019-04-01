using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMatch : MonoBehaviour {

    public GameObject toTrack;
    public GameObject origin;
    private TrailRenderer tr;
    private Vector3 startPos;
    private Vector3 trStartPos;
    
    // Use this for initialization
    void Start () {
        tr = toTrack.GetComponent<TrailRenderer>();
        trStartPos = tr.transform.position;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update () {
        Spherical3 sc = new Spherical3();

        Vector3 where = tr.transform.position - origin.transform.position;
        SphereUtility.CartesianToSpherical(where, out sc);

        // reflecting
        sc.p *= -1;

        Vector3 nwhere = new Vector3();
        SphereUtility.SphericalToCartesian(sc, out nwhere);

        transform.position = startPos + nwhere;
    }



}
