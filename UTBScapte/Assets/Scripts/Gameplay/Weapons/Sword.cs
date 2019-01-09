using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Sword : MonoBehaviour {

    const float QTESequenceDelay = 1f;

    public Unit unit;

    public GameObject cellChooserPrefab;
    public QTE qtePrefab;

    private GameObject cellChooser;
    private QTE qte;

    private int valideQTE = 0;

    private void Start()
    {
        cellChooser = Instantiate(cellChooserPrefab, unit.transform.position + (1.5f * Vector3.up), Quaternion.identity, null);
    }

    private void OnDetachedFromHand(Hand hand)
    {
        DeletePrefab();
    }

    public void DeletePrefab()
    {
        Destroy(cellChooser);
        Destroy(qte);
        Destroy(gameObject);
    }

    public void DirectionChoosed(HexDirection dir)
    {
        cellChooser.SetActive(false);
        HexCell cell = unit.Location.GetNeighbor(dir);
        if (cell != null && cell.Unit != null)
        {
            Vector3 direction = cell.Unit.transform.position - unit.transform.position;
            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            qte = Instantiate(qtePrefab, unit.transform.position + Vector3.up, Quaternion.AngleAxis(angle, Vector3.up), null);
            StartCoroutine(QTESequences(cell.Unit));
        }
    }

    public void ValidateQTE()
    {
        ++valideQTE;
    }

    IEnumerator QTESequences(Unit pTarget)
    {
        qte.RandomActive();
        yield return new WaitForSeconds(QTESequenceDelay);
        qte.RandomActive();
        yield return new WaitForSeconds(QTESequenceDelay);
        qte.RandomActive();
        yield return new WaitForSeconds(QTESequenceDelay);
        qte.gameObject.SetActive(false);
        pTarget.TakeDamage(Mathf.RoundToInt(unit.getDamage() * (valideQTE * 1f) / 6));
    }
}
