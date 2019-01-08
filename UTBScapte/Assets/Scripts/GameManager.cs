using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public MapManager mapManager;
    public Teleport teleport;
    public Unit startUnit;

    public TeamManager teamManager;

    /*
     * WEAPONS
     */
    public ItemPackage LongbowItemPackage;
    public ItemPackage Sword;

    [EnumFlags]
    public Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags;

    public GameObject Menu;
    public Button actionButton;

    private HexGrid grid;
    private Unit currentUnit;
    private Player player;
    private Hand hand;

    private GameObject instantiatedWeapon;

    /*
     * Classes
     */
    public List<CharacterClasses> Classes;

    void Start() {
        grid = mapManager.InitMap();
        player = Player.instance;

        // Init all teams with members and turn index
        teamManager.Init(Classes);

        SetCurrentUnit(teamManager.activeTeam.units[0]);
        hand = player.leftHand;
    }

    private void Update()
    {   
        if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(player.rightHand.handType)) //Toggle menu
        {
            Menu.SetActive(!Menu.activeSelf);
        }
    }

    public void EndTurn()
    {
        teamManager.nextActiveTeam();
        SetCurrentUnit(teamManager.activeTeam.getCurrentUnit());
        Menu.SetActive(false);
    }

    public void PreviousUnit()
    {
        SetCurrentUnit(teamManager.activeTeam.PreviousUnit());
    }

    public void NextUnit()
    {
        SetCurrentUnit(teamManager.activeTeam.NextUnit());
    }

    public void Action()
    {

        actionButton.interactable = false;
        currentUnit.actionPossible = false;
        
        switch(currentUnit.classe.Name)
        {
            case ClasseTypes.ARCHER:
                SetupArcherAction();
                break;

            case ClasseTypes.WARRIOR:
                SetupWarriorAction();
                break;
        }

        Menu.SetActive(false);
    }

    private void SetupArcherAction()
    {
        GameObject spawnedItem = GameObject.Instantiate(LongbowItemPackage.itemPrefab);
        spawnedItem.SetActive(true);
        hand.AttachObject(spawnedItem, GrabTypes.Scripted, attachmentFlags);
        GameObject otherHandObjectToAttach = GameObject.Instantiate(LongbowItemPackage.otherHandItemPrefab);
        otherHandObjectToAttach.SetActive(true);
        hand.otherHand.AttachObject(otherHandObjectToAttach, GrabTypes.Scripted, attachmentFlags);
    }

    private void SetupWarriorAction()
    {
        GameObject spawnedItem = GameObject.Instantiate(Sword.itemPrefab);
        spawnedItem.SetActive(true);
        hand.otherHand.AttachObject(spawnedItem, GrabTypes.Scripted, attachmentFlags);

        instantiatedWeapon = spawnedItem;
    }

    private void SetCurrentUnit(Unit pUnit)
    {
        //Reactivate the previous unit
        if (currentUnit != null)
        {
            currentUnit.gameObject.SetActive(true);

            // Unamred unit
            switch (currentUnit.classe.Name)
            {
                case ClasseTypes.ARCHER:
                    UnarmedArcher();
                    break;

                case ClasseTypes.WARRIOR:
                    UnarmedWarrior();
                    break;
            }
        }

        currentUnit = pUnit;
        currentUnit.gameObject.SetActive(false);
        teleport.setCurrentUnit(currentUnit);

        //Place player at the right position
        Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
        player.trackingOriginTransform.position = pUnit.Location.transform.position + playerFeetOffset;

        actionButton.interactable = currentUnit.actionPossible;

        /*if (itemPackage.packageType == ItemPackage.ItemPackageType.OneHanded)
        {
            RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand);
            RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand);
            RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand.otherHand);
        }

        // if we're trying to spawn a two-handed item, remove one and two-handed items from both hands
        if (itemPackage.packageType == ItemPackage.ItemPackageType.TwoHanded)
        {
            RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand);
            RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand.otherHand);
            RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand);
            RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand.otherHand);
        }*/
    }

    public void UnarmedArcher()
    {
        RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand);
        RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand.otherHand);
        RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand);
        RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand.otherHand);
    }

    public void UnarmedWarrior()
    {
        RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand);
        RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand.otherHand);
        RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand);
        RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand.otherHand);

        Destroy(instantiatedWeapon);
        instantiatedWeapon = null;
    }

    private void RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType packageType, Hand hand)
    {
        for (int i = 0; i < hand.AttachedObjects.Count; i++)
        {
            ItemPackageReference packageReference = hand.AttachedObjects[i].attachedObject.GetComponent<ItemPackageReference>();
            if (packageReference != null)
            {
                if (packageReference.itemPackage.packageType == packageType)
                {
                    GameObject detachedItem = hand.AttachedObjects[i].attachedObject;
                    hand.DetachObject(detachedItem);
                }
            }
        }
    }
}
