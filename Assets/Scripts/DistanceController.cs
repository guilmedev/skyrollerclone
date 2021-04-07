using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceController : MonoBehaviour
{

    [SerializeField]
    Transform myobj;
    [SerializeField]
    Transform myTarget;

    [SerializeField]
    float distaceMagnitude;

    [SerializeField]
    Slider mySlider;


    Vector3 totalDistance;

    // Start is called before the first frame update
    void Start()
    {

        totalDistance   = myTarget.position -  myobj.position;

        if(mySlider)
            mySlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(myobj != null && myTarget != null)
        {
            Vector3 _distaceMagnitude = Vector3.Lerp (myobj.position , myTarget.position, Time.deltaTime );
            distaceMagnitude = (int) _distaceMagnitude.magnitude / totalDistance.z ;

            if (mySlider)
                mySlider.value = distaceMagnitude * 100;
        }
    }
}
