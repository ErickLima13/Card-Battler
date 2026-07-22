using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class BossView : MonoBehaviour
{
    [SerializeField] private Transform _poisonCounterPosition;

    [SerializeField] private TextMeshProUGUI _poisonText;

    [SerializeField] private StatusManager _statusManager;

    [SerializeField] private GameObject _poisonPrefab;

    [SerializeField] private Transform _poisonHitPosition;

    public GameObject _poisonCounter;

    private bool hasCounter;


    private void Start()
    {
        UpdateStatusCounter();
    }

    public void UpdateStatusCounter()
    {
        var temp = _statusManager.ActiveEffects;

        if (temp.Count > 0)
        {
            foreach (var active in temp)
            {
                _poisonText.text = active.Stacks.ToString();
            }
        }
        else
        {
            _poisonText.text = "";
        }

        bool hasPoison = _statusManager.ActiveEffects.Count > 0;

        UpdatePoisonCounter(hasPoison);

    }

    public async UniTask ApplyPoisonVfx()
    {
        GameObject temp = Instantiate(_poisonPrefab, _poisonHitPosition);
        Destroy(temp, 1f);

        if (_poisonCounter == null)
        {
            _poisonCounter = Instantiate(_poisonPrefab, _poisonCounterPosition);
        }
        else
        {
            _poisonCounter.SetActive(true);
        }
    }

    public void UpdatePoisonCounter(bool active)
    {
        if (_poisonCounter == null && active)
        {
            _poisonCounter = Instantiate(_poisonPrefab, _poisonCounterPosition);
        }

        if (_poisonCounter != null)
        {
            _poisonCounter.SetActive(active);
        }
    }


    private void OnEnable()
    {
        _statusManager.OnStatusesChanged += UpdateStatusCounter;
    }

    private void OnDisable()
    {
        _statusManager.OnStatusesChanged -= UpdateStatusCounter;

    }
}
