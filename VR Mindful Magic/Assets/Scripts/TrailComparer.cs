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

    private bool motion;
    private bool notidle;

    IEnumerator StartMotion()
    {
        yield return new WaitForSeconds(0.11f);
        leftsource.GetComponent<TrailRecorder>().StartRecord();
        leftmatch.GetComponent<TrailRecorder>().StartRecord();
        rightsource.GetComponent<TrailRecorder>().StartRecord();
        rightmatch.GetComponent<TrailRecorder>().StartRecord();
        motion = true;
    }

    // Use this for initialization
    void Start () {
        go = Instantiate(linePrefab);
        go2 = Instantiate(lineMatchPrefab);
        motion = false;
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (string s in new string[]{ "Idling", "Magic", "Victory", "Open", "Wheelbarrow", "Strong"})
        //{
        //    if (model.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(s))
        //    {
        //        messages.GetComponent<TextMeshPro>().text = motion + ":" + s;
        //    }
        //}

        if (motion && model.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idling"))
        {
            motion = false;
            leftsource.GetComponent<TrailRecorder>().StopRecord();
            leftmatch.GetComponent<TrailRecorder>().StopRecord();
            rightsource.GetComponent<TrailRecorder>().StopRecord();
            rightmatch.GetComponent<TrailRecorder>().StopRecord();

            TrailRecorder str = rightsource.GetComponent<TrailRecorder>();
            Spherical3[] sourceTrail = str.mytrail.ToArray();
            TrailRecorder mtr = leftmatch.GetComponent<TrailRecorder>();
            Spherical3[] matchTrail = mtr.mytrail.ToArray();

            Debug.Log("Comparing left trails");
            float d = SphereUtility.Divergence(sourceTrail, matchTrail);

            str = leftsource.GetComponent<TrailRecorder>();
            sourceTrail = str.mytrail.ToArray();
            mtr = rightmatch.GetComponent<TrailRecorder>();
            matchTrail = mtr.mytrail.ToArray();

            Debug.Log("Comparing right trails");
            d += SphereUtility.Divergence(sourceTrail, matchTrail);
            d /= 2;
            Debug.Log("Divergence = " + d);

            messages.GetComponent<TextMeshPro>().text = "Score: " + (1 - Mathf.Min(d, 1));

            Vector3[] sourceCarts = SphereUtility.SphericaToCartesianMirror(sourceTrail);
            go.GetComponent<LineRenderer>().positionCount = sourceCarts.Length;
            go.GetComponent<LineRenderer>().SetPositions(sourceCarts);

            Vector3[] matchCarts = SphereUtility.SphericaToCartesian(matchTrail);
            go2.GetComponent<LineRenderer>().positionCount = matchCarts.Length;
            go2.GetComponent<LineRenderer>().SetPositions(matchCarts);

        }
        else if (OVRInput.GetDown(OVRInput.Button.Any))
        {
            Debug.Log("Advancing to Next Animation");

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                model.GetComponent<Animator>().SetTrigger("victory");
                StartCoroutine(StartMotion());
                messages.GetComponent<TextMeshPro>().text = "Victory..";
            }
            else if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                model.GetComponent<Animator>().SetTrigger("wheelbarrow");
                StartCoroutine(StartMotion());
                messages.GetComponent<TextMeshPro>().text = "Wheelbarrow..";
            }
            else if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                model.GetComponent<Animator>().SetTrigger("strong");
                StartCoroutine(StartMotion());
                messages.GetComponent<TextMeshPro>().text = "Strong..";
            }
            else if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                model.GetComponent<Animator>().SetTrigger("lift");
                StartCoroutine(StartMotion());
                messages.GetComponent<TextMeshPro>().text = "Lift..";
            }
        }
    }
}
