using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Sword : MonoBehaviour {

    private void Start()
    {
        
    }

    private void OnDetachedFromHand(Hand hand)
    {
        Destroy(gameObject);
    }
}
