using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Animator _animator;

    public void ChangeText(string value, bool anim)
    {
        _valueText.text = value;
        
        if(anim) _animator.SetTrigger("OnValueChange");
    }
}
