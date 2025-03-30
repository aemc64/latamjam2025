using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputType
{
    ArrowDown,
    ArrowUp,
    ArrowLeft,
    ArrowRight
}

public class InputImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Sprite> _inputIcons;

    public void SetInputIcon(InputType input)
    {
        _image.sprite = _inputIcons[(int)input];
    }
}
