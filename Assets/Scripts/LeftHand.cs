using UnityEngine;

public class LeftHand : Hand
{
    private string _hand;
    protected override void Hold()
    {
        hand = "I am the left hand";
        base.Hold();
    }
}
