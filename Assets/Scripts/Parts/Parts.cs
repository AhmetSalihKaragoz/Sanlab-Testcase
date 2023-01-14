using System;
using UnityEngine;
public abstract class Parts : MonoBehaviour
{
    [SerializeField] protected GameObject triggerPoint;
    [SerializeField] protected GameObject myAttachedSelf;

    protected bool isAttached;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerPoint"))
        {
            MontageChain();
        }
        
    }

    public abstract void MontageChain();

    protected abstract void TurnOnDrag();
    protected abstract void TurnOffDrag();
}
