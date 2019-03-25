using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowParent : MonoBehaviour
{
    [SerializeField] private float step = .005f;
    [SerializeField] private bool freezeRotation = false;
    [SerializeField] private bool freezePosition = false;

    private Vector3 startLocalPos, lastFramePos, lastDesiredPos, fromPos;
    private Quaternion startLocalRot, lastFrameRot, lastDesiredRot, fromRot;

    private float percent;
    private float constZ_Pos;

    void Start()
    {
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;

        lastFramePos = transform.position;
        lastFrameRot = transform.rotation;

        constZ_Pos = startLocalPos.z;
    }

    void Update()
    {
        Vector3 newDesiredPos = transform.parent.TransformPoint(startLocalPos);
        Quaternion newDesiredRot = transform.parent.rotation * startLocalRot;

        if (lastDesiredPos != newDesiredPos || lastDesiredRot != newDesiredRot)
        {
            percent = 0;

            lastDesiredPos = newDesiredPos;
            lastDesiredRot = newDesiredRot;

            fromPos = lastFramePos;
            fromRot = lastFrameRot;
        }

        if (percent <= 1)
        {
            percent += step;

            lastFramePos = Vector3.Lerp(fromPos, newDesiredPos, percent);            
            lastFrameRot = Quaternion.Lerp(fromRot, newDesiredRot, percent);

            Vector3 newLocalPos = transform.parent.InverseTransformPoint(lastFramePos);
            newLocalPos.z = constZ_Pos;

            if (!freezePosition) transform.localPosition = newLocalPos;
            if (!freezeRotation) transform.rotation = lastFrameRot;
        }
    }
}
