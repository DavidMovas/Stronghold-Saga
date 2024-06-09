using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Animator _animator;

    public void ChangeText(string value)
    {
        _valueText.text = value;
        
        _animator.SetTrigger("OnValueChange");
    }
}
