using FGT.Prototypes.DamagePopup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int _totalHealth = 100;

    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private Slider _healthBar;

    private int _currentHealth;

    private bool _isDied;


    public bool Dead() => _isDied;

    private void Start()
    {
        _currentHealth = _totalHealth;
        UpdateHealthUI();
    }


    private void UpdateHealthUI()
    {
        _healthText.text = _currentHealth + "/" + _totalHealth;

        _healthBar.maxValue = _totalHealth;
        _healthBar.value = _currentHealth;
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;

        string temp = "+ " + amount;
        DamagePopup.Create($"{temp}", Vector3.up, transform, Color.red, 20);

        _currentHealth += amount;

        if (_currentHealth > _totalHealth)
        {
            _currentHealth = _totalHealth;
        }

        UpdateHealthUI();

    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;

        string temp = "- " + amount;
        DamagePopup.Create($"{temp}", Vector3.up, transform, Color.red, 20);

        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isDied = true;
        }

        UpdateHealthUI();

    }
}
