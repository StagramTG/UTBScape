using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Sword : MonoBehaviour {

    public Unit unit;

    private CellChooser cellChooser;

    private void Start()
    {
        cellChooser = gameObject.AddComponent<CellChooser>();
    }

    private void OnDetachedFromHand(Hand hand)
    {
        Destroy(gameObject);
    }
}
