using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereUtility : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Spherical3 Mirror(Spherical3 inS)
    {
        Spherical3 m = new Spherical3();
        m.r = inS.r;
        m.p = -inS.p;
        m.e = inS.e;
        return m;
    }

    public static Spherical3 Rotate(Spherical3 inS, float deltaRad)
    {
        Spherical3 m = new Spherical3();
        m.r = inS.r;
        m.p = inS.p + deltaRad;
        m.e = inS.e;
        return m;
    }

    //https://blog.nobel-joergensen.com/2010/10/22/spherical-coordinates-in-unity/
    public static void SphericalToCartesian(Spherical3 sphereCoords, out Vector3 outCart)
    {
        float a = sphereCoords.r * Mathf.Cos(sphereCoords.e);
        outCart.x = a * Mathf.Cos(sphereCoords.p);
        outCart.y = sphereCoords.r * Mathf.Sin(sphereCoords.e);
        outCart.z = a * Mathf.Sin(sphereCoords.p);
    }


    public static void CartesianToSpherical(Vector3 cartCoords, out Spherical3 sphereCoords)
    {
        if (cartCoords.x == 0)
            cartCoords.x = Mathf.Epsilon;
        sphereCoords.r = Mathf.Sqrt((cartCoords.x * cartCoords.x)
                        + (cartCoords.y * cartCoords.y)
                        + (cartCoords.z * cartCoords.z));
        sphereCoords.p = Mathf.Atan(cartCoords.z / cartCoords.x);
        if (cartCoords.x < 0)
            sphereCoords.p += Mathf.PI;
        sphereCoords.e = Mathf.Asin(cartCoords.y / sphereCoords.r);
    }

    public static float Divergence(Spherical3[] source, Spherical3[] match)
    {
        Vector3 swhere = new Vector3();
        Vector3 mwhere = new Vector3();
        float total = 0.0f;
        for (int i = 0; i < source.Length; i++)
        {
            SphericalToCartesian(source[i], out swhere);
            SphericalToCartesian(Mirror(match[i]), out mwhere);
            Debug.Log(swhere);
            Debug.Log(mwhere);
            Debug.Log(i + ": " + (swhere - mwhere).magnitude);
            total += (swhere - mwhere).magnitude;
        }
        return total / source.Length;
    }
}

public struct Spherical3
{
    public float r;
    public float p;
    public float e;
}
