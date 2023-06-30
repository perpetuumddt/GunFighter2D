using System;

namespace Interface.Character
{
    public interface ICharacterHealthController
    {
        int CurrentHealth { get; }
        event Action<bool> OnHealthZero;
        event Action<int> OnUpdateHealth;
        void UpdateHealth(int _currentHealth);
        void DestroyOnDeath();
        void ReplenishHealth(int health);
    }
}