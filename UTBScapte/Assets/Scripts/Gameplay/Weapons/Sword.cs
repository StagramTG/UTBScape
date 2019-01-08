using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Sword : MonoBehaviour {

    public Unit unit;

    public GameObject cellChooser;

    private void Start()
    {
        cellChooser = Instantiate(cellChooser, unit.transform.position + Vector3.up, Quaternion.identity, null);
    }

    private void OnDetachedFromHand(Hand hand)
    {
        Destroy(cellChooser);
        Destroy(gameObject);
    }

    public void DirectionChoosed(HexDirection dir)
    {
        cellChooser.SetActive(false);
        HexCell cell = unit.Location.GetNeighbor(dir);
        if (cell != null && cell.Unit != null)
            cell.Unit.TakeDamage(unit.getDamage());
    }
}
