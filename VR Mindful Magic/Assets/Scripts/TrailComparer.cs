using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailComparer : MonoBehaviour {

    public GameObject source;
    public GameObject match;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            TrailRecorder str = source.GetComponent<TrailRecorder>();
            Spherical3[] sourceTrail = str.mytrail.ToArray();
            TrailRecorder mtr = match.GetComponent<TrailRecorder>();
            Spherical3[] matchTrail = mtr.mytrail.ToArray();

            Debug.Log("Comparing trails");
            float d = SphereUtility.Divergence(sourceTrail, matchTrail);
            Debug.Log("Divergence = " + d);
        }
    }
}
