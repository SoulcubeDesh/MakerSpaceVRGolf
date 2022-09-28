using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class myPoseScript : BasePoseProvider
{
    public override bool TryGetPoseFromProvider(out Pose output)
    {
        output = new Pose(transform.position, transform.rotation);
        return gameObject.activeInHierarchy;
    }
}
