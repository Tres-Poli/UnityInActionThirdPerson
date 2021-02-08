using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public void Initialize()
        {
            Health = 50;
            MaxHealth = 50;

            Status = ManagerStatus.Started;
            Debug.Log("Player manager started...");
        }

        public void ChangeHealth(int value)
        {
            Health += value;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            else if (Health <= 0)
            {
                Health = 0;
            }

            Debug.Log($"Player's health changed to {Health}/{MaxHealth}");
        }
    }
}