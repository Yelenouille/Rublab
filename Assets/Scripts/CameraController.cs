using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  public Vector3 offset;
  public Vector3 offsetMin;
  public Vector3 offsetMax;

  public void Reset() {
    offsetMin = Vector3.zero;
    offsetMax = Vector3.zero;
  }

  public void CenterOn(Vector3 target) {
    // baseTarget = target;
    transform.position = target + offset;
  }

  // private Vector3 baseTarget;
  // public void Update() {
  //   transform.position = baseTarget + offset;
  // }

  void Update() {
    if (!MenuManager.instance.mainMenu.activeInHierarchy) {
    }
  }
}
