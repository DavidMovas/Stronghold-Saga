using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    private float _currentValue;
    private float _value;

    private bool _isChanging;
    
    private void Update()
    {
        if (_isChanging)
        {
            if (_currentValue > _value)
            {
                _currentValue -= 0.0025f;
                
                ChangeFillAmount(_currentValue);
            }
            else
            {
                _isChanging = false;
            }
        }
    }
    public void ChangeValue(float value)
    {
        _value = value;

        if (value >= _currentValue)
        {
            _currentValue = value;
            ChangeFillAmount(_currentValue);
        }

        _isChanging = true;
    }

    private void ChangeFillAmount(float value)
    {
        _fillImage.fillAmount = value;
    }
}
