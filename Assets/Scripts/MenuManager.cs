using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class MenuManager : MonoBehaviour {
  public static MenuManager instance = null;

  public GameObject mainMenu;

  void Awake() {
    if (instance != null && instance != this) {
      Destroy(gameObject);
      return;
    } else {
      instance = this;
    }
  }

}
