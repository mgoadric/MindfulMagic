using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrailComparer : MonoBehaviour {

    public GameObject leftsource;
    public GameObject leftmatch;
    public GameObject rightsource;
    public GameObject rightmatch;
    public GameObject model;
    public GameObject messages;
    public GameObject linePrefab;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            leftsource.GetComponent<TrailRecorder>().StopRecord();
            leftmatch.GetComponent<TrailRecorder>().StopRecord();

            TrailRecorder str = leftsource.GetComponent<TrailRecorder>();
            Spherical3[] sourceTrail = str.mytrail.ToArray();
            TrailRecorder mtr = leftmatch.GetComponent<TrailRecorder>();
            Spherical3[] matchTrail = mtr.mytrail.ToArray();

            Debug.Log("Comparing trails");
            float d = SphereUtility.Divergence(sourceTrail, matchTrail);
            Debug.Log("Divergence = " + d);

            messages.GetComponent<TextMeshPro>().text = "Score: " + d;

            Vector3[] sourceCarts = SphereUtility.SphericaToCartesian(sourceTrail);
            GameObject go = Instantiate(linePrefab);
            go.GetComponent<LineRenderer>().positionCount = sourceCarts.Length;
            go.GetComponent<LineRenderer>().SetPositions(sourceCarts);

        }
        else if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log("Advancing to Next Animation");
            model.GetComponent<Animator>().SetTrigger("magic");
            messages.GetComponent<TextMeshPro>().text = "Magic..";
            leftsource.GetComponent<TrailRecorder>().StartRecord();
            leftmatch.GetComponent<TrailRecorder>().StartRecord();

        }
        else if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Debug.Log("Advancing to Next Animation");
            model.GetComponent<Animator>().SetTrigger("wheelbarrow");
            messages.GetComponent<TextMeshPro>().text = "Wheelbarrow..";
            leftsource.GetComponent<TrailRecorder>().StartRecord();
            leftmatch.GetComponent<TrailRecorder>().StartRecord();

        }
        else if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("Advancing to Next Animation");
            model.GetComponent<Animator>().SetTrigger("strong");
            messages.GetComponent<TextMeshPro>().text = "JStrong..";
            leftsource.GetComponent<TrailRecorder>().StartRecord();
            leftmatch.GetComponent<TrailRecorder>().StartRecord();

        }
    }
}
