using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDirection : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirectionChooser"))
        {
            HexDirection direction = other.GetComponent<CellChooserDirection>().direction;
            transform.parent.GetComponent<Sword>().DirectionChoosed(direction);
        }
        if (other.CompareTag("QTE"))
        {
            other.gameObject.SetActive(false);
            transform.parent.GetComponent<Sword>().ValidateQTE();
        }
    }
}
