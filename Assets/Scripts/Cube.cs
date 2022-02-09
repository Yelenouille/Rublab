using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

  public bool fix;
  public Outline outline;
  public GroundTile groundController;
  // private List<Transform> faces;

  void Awake() {
    // faces = new List<Transform>(outline.gameObject.GetComponentsInChildren<Transform>());
  }

  public void OnPointerDown(PointerEventData eventData) {
    Selected();
  }

  public void OnPointerUp(PointerEventData eventData) {
    Unselected();
  }

  void Selected() {
    outline.enabled = true;
    // StartCoroutine(Pivot());
  }

  void Unselected() {
    outline.enabled = false;
  }

  // IEnumerator Pivot() {
  //   while (outline.enabled && !GameManager.instance.WayAchieved()) {
  //     yield return null;
  //   }
  //   if (outline.enabled) Unselected();
  // }

}
