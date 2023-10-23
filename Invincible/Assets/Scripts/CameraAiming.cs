using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAiming : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public float zoonSpeed = 5f;
    public float startFOV;
    public float zoomedOutFOV;

    public float offSetStart;
    public float offSetEnd;

    private float targetFOV;
    private float targetOffSet;
    private bool isZoomingOut;


    void Start()
    {
        targetFOV = startFOV;
        targetOffSet = offSetStart;
    }

    // Update is called once per frame
    void Update()
    {
        isZoomingOut = Input.GetKey(KeyCode.W);

        targetFOV = isZoomingOut ? zoomedOutFOV : startFOV;
        targetOffSet = isZoomingOut ? offSetEnd : offSetStart;

        float newFOV = Mathf.Lerp(freeLookCam.m_Orbits[1].m_Radius, targetFOV, Time.deltaTime * zoonSpeed);
        float newOffSet = Mathf.Lerp(freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.x, targetOffSet, Time.deltaTime * zoonSpeed);

        CinemachineComposer composer = freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        composer.m_TrackedObjectOffset.x = newOffSet;
        freeLookCam.m_Orbits[1].m_Radius = newFOV;


    }

}
