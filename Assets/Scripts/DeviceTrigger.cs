using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _targets;

    public bool RequireKey;

    private void OnTriggerEnter(Collider other)
    {
        if (RequireKey && Managers.Inventory.EquippedItem != "key")
        {
            return;
        }

        foreach(var trigger in _targets)
        {
            trigger.SendMessage("Activate");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var trigger in _targets)
        {
            trigger.SendMessage("Deactivate");
        }
    }
}
