using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
  public static InputManager instance = null;

  private InputControls controls;

  public bool mainInputDown = false;

  void Awake() {
    if (instance != null && instance != this) {
      Destroy(gameObject);
      return;
    } else {
      instance = this;
    }
    controls = new InputControls();
    SetPhaseCallback();
  }

  void OnEnable() {
    controls.Player.Enable();
  }

  void OnDisable() {
    controls.Player.Disable();
  }

  void SetPhaseCallback() {
    controls.Player.Tap.performed += ctx => {
      if(tapEvent != null && !EventSystem.current.IsPointerOverGameObject()) tapEvent();
    };
    controls.Player.MainInput.started += ctx => mainInputDown = true;
    controls.Player.MainInput.canceled += ctx => mainInputDown = false;
  }

  public delegate void OnTapEvent();
  public static OnTapEvent tapEvent;
}
