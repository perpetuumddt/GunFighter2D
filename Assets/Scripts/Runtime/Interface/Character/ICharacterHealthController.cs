using System;

namespace Gunfighter.Runtime.Interface.Character
{
    public interface ICharacterHealthController
    {
        int CurrentHealth { get; }
        event Action<bool> OnHealthZero;
        event Action<int> OnUpdateHealth;
        void UpdateHealth(int currentHealth);
        void DestroyOnDeath();
        void ReplenishHealth(int health);
    }
}