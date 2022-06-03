using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts.ScriptableObjects.Resources.Item;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects.Resources;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Player.Test
{
    public class LootImitation : MonoBehaviour
    {
        [SerializeField] private InputAction test;
        [SerializeField] private List<Resource> possibleDrops;
        [SerializeField] private ResourceInventory resourceInventory;

        private void Awake() => test.Enable();
        private void OnEnable() => test.started += RandGen;
        private void OnDisable() => test.started -= RandGen;

        private void RandGen(InputAction.CallbackContext context)
        {
            var randomNumber = Random.Range(0, possibleDrops.Count);
            var randomAmount = Random.Range(1, 20);
            var isSuccess = resourceInventory.AddItem(possibleDrops[randomNumber], randomAmount, out var amountLeft);
            if(!isSuccess) Debug.Log(amountLeft + " extra " + possibleDrops[randomNumber].name + " were sent to trove)");
        }
    }
}
