using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Listener : MonoBehaviour
{
    private Button _button;
    private void Awake() {
        Messenger.AddListener(GameEvent.BUTTON_CLICKED, OnButtonClicked);
    }
    private void Start() {
        _button = GetComponent<Button>();
    }
    private void OnDestroy() {
        Messenger.RemoveListener(GameEvent.BUTTON_CLICKED, OnButtonClicked);
    }
    private void OnButtonClicked() {
        _button.image.color = Color.cyan;
    }
}
