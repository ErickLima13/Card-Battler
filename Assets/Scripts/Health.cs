using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _totalHealth = 100;

    private int _currentHealth;


    public bool IsAlive { get; private set; }

    private void Start()
    {
        _currentHealth = _totalHealth;
    }


    public void HealDamage(int amount)
    {
        if (amount <= 0) return;

        _currentHealth += amount;

        if(_currentHealth  > _totalHealth)
        {
            _currentHealth = _totalHealth;
        }

    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;

        _currentHealth -= amount;

        if(_currentHealth <= 0)
        {
            IsAlive = true;
        }

        print(_currentHealth + " boss life");
    }
}
