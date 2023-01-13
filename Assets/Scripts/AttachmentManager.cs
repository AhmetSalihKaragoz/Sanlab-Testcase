using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentManager : MonoBehaviour
{
    [SerializeField] private GameObject attachedPiston;

    void TurnRendersOn(GameObject attachedPart,Vector3 pos,bool isParent)
    {
        var instance = Instantiate(attachedPiston, pos, Quaternion.identity,transform);
        if (isParent)
        {
            
        }
    }
    
}
