using System;
using UnityEngine;
public abstract class Hand : MonoBehaviour
{
    protected string hand;
    private void Start()
    {
        Hold();
    }
    
    protected virtual void Hold()
    {
        Debug.Log(hand);
    }
    
}
