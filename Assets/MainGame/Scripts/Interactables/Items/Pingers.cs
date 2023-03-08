using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pingers : MonoBehaviour, IInteractable
{
    [SerializeField] string prompt;
    [SerializeField] ItemData itemData;
    [SerializeField] PlayerController playerController;
    public string InteractionPrompt => prompt;
    private bool hasPingers;
    private bool hasQuestItem;
    public bool CanInteract => canInteract;

    private bool canInteract;

    private void Start()
    {
        canInteract = true;
        hasPingers = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("hasPingers")).value;

        hasQuestItem = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("hasQuest1Item")).value;
    }
    private void Update()
    {

    }
    public bool Interact(Interactor interactor)
    {
        hasPingers = true;
        hasQuestItem = true;
        InventoryManager.Instance.AddItem(itemData);
        Ink.Runtime.Object obj = new Ink.Runtime.BoolValue(hasPingers);
        DialogueManager.GetInstance().SetVariableState("hasPingers", obj);

        Ink.Runtime.Object obj2 = new Ink.Runtime.BoolValue(hasQuestItem = true);
        DialogueManager.GetInstance().SetVariableState("hasQuest1Item", obj2);

        StartCoroutine(itemPickedUp());
        //Destroy(gameObject);
        return true;
    }
    IEnumerator itemPickedUp()
    {
        yield return new WaitForSeconds(0.2f);
        playerController.EnableMovement();
        gameObject.SetActive(false);
    }
}
