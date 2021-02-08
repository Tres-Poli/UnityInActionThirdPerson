using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(InventoryManager))]
    public class Managers : MonoBehaviour
    {
        private List<IGameManager> _startSequence;

        public static PlayerManager Player;
        public static InventoryManager Inventory;

        void Awake()
        {
            Player = GetComponent<PlayerManager>();
            Inventory = GetComponent<InventoryManager>();

            _startSequence = new List<IGameManager>();
            _startSequence.Add(Player);
            _startSequence.Add(Inventory);

            StartCoroutine(StartupManagers());
        }

        private IEnumerator StartupManagers()
        {
            foreach (var manager in _startSequence)
            {
                manager.Initialize();
            }

            yield return null;

            var numModules = _startSequence.Count;
            var numReady = 0;

            while (numReady < numModules)
            {
                var lastReady = numReady;

                foreach (var module in _startSequence)
                {
                    if (module.Status == ManagerStatus.Started)
                    {
                        numReady++;
                    }
                }

                if (lastReady < numReady)
                {
                    Debug.Log($"Progress: {numReady} / {numModules}");
                }

                yield return null;
            }

            Debug.Log("All managers started!");
        }
    }
}