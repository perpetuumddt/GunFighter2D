using Gunfighter.Runtime.Entity.Character.Player.Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.UI.HealthBar
{
    public class HealthBarUIController : MonoBehaviour
    {
        [FormerlySerializedAs("_heartPrefab")] [SerializeField] 
        private GameObject heartPrefab;
    
        [FormerlySerializedAs("_playerHealthController")] [SerializeField]
        private PlayerHealthController playerHealthController;

        private HealthBarController _healthBarController;
        private void Awake()
        {
            _healthBarController = new HealthBarController(heartPrefab,this.gameObject);
        }

        private void OnEnable()
        {
            playerHealthController.OnUpdateHealth += UpdateHealthBar;
        }

        private void OnDisable()
        {
            playerHealthController.OnUpdateHealth -= UpdateHealthBar;
        }

        public void UpdateHealthBar(int currentHealth)
        {
            _healthBarController.SetupDisplay(playerHealthController.MaxHealth);
            _healthBarController.UpdateDisplay(currentHealth);
        }
    }
}