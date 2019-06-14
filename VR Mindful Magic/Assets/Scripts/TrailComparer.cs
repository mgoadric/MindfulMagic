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
    private GameObject go;
    public GameObject lineMatchPrefab;
    private GameObject go2;



    // Use this for initialization
    void Start () {
        go = Instantiate(linePrefab);
        go2 = Instantiate(lineMatchPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            leftsource.GetComponent<TrailRecorder>().StopRecord();
            leftmatch.GetComponent<TrailRecorder>().StopRecord();
            rightsource.GetComponent<TrailRecorder>().StopRecord();
            rightmatch.GetComponent<TrailRecorder>().StopRecord();

            TrailRecorder str = rightsource.GetComponent<TrailRecorder>();
            Spherical3[] sourceTrail = str.mytrail.ToArray();
            TrailRecorder mtr = leftmatch.GetComponent<TrailRecorder>();
            Spherical3[] matchTrail = mtr.mytrail.ToArray();

            Debug.Log("Comparing trails");
            float d = SphereUtility.Divergence(sourceTrail, matchTrail);
            Debug.Log("Divergence = " + d);

            messages.GetComponent<TextMeshPro>().text = "Score: " + d;

            Vector3[] sourceCarts = SphereUtility.SphericaToCartesianMirror(sourceTrail);
            go.GetComponent<LineRenderer>().positionCount = sourceCarts.Length;
            go.GetComponent<LineRenderer>().SetPositions(sourceCarts);

            Vector3[] matchCarts = SphereUtility.SphericaToCartesian(matchTrail);
            go2.GetComponent<LineRenderer>().positionCount = matchCarts.Length;
            go2.GetComponent<LineRenderer>().SetPositions(matchCarts);

        }
        else if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log("Advancing to Next Animation");
            model.GetComponent<Animator>().SetTrigger("magic");
            messages.GetComponent<TextMeshPro>().text = "Magic..";
            leftsource.GetComponent<TrailRecorder>().StartRecord();
            leftmatch.GetComponent<TrailRecorder>().StartRecord();
            rightsource.GetComponent<TrailRecorder>().StartRecord();
            rightmatch.GetComponent<TrailRecorder>().StartRecord();

        }
        else if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Debug.Log("Advancing to Next Animation");
            model.GetComponent<Animator>().SetTrigger("wheelbarrow");
            messages.GetComponent<TextMeshPro>().text = "Wheelbarrow..";
            leftsource.GetComponent<TrailRecorder>().StartRecord();
            leftmatch.GetComponent<TrailRecorder>().StartRecord();
            rightsource.GetComponent<TrailRecorder>().StartRecord();
            rightmatch.GetComponent<TrailRecorder>().StartRecord();

        }
        else if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("Advancing to Next Animation");
            model.GetComponent<Animator>().SetTrigger("strong");
            messages.GetComponent<TextMeshPro>().text = "JStrong..";
            leftsource.GetComponent<TrailRecorder>().StartRecord();
            leftmatch.GetComponent<TrailRecorder>().StartRecord();
            rightsource.GetComponent<TrailRecorder>().StartRecord();
            rightmatch.GetComponent<TrailRecorder>().StartRecord();

        }
    }
}
