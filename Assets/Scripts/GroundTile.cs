using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class GroundTile : MonoBehaviour {
  public TypeCross typeGround;
  public Transform container;

  public void SetAboutDirections(List<MoveDirection> dirs, SOTheme theme) {
    GameObject ground;
    switch (dirs.Count) {

      case 1:
      ground = Instantiate(theme.endLine, container);
        switch (dirs[0]) {
          case MoveDirection.Up:
            container.RotateAround(transform.position, Vector3.up, 90);
            break;
          case MoveDirection.Down:
            container.RotateAround(transform.position, Vector3.up, 180);
            break;
          case MoveDirection.Right:
            container.RotateAround(transform.position, Vector3.up, 270);
            break;
        }
        break;

      case 2:
        if (dirs.Contains(MoveDirection.Right) && dirs.Contains(MoveDirection.Left)) {
          ground = Instantiate(theme.line, container);
        }
        if (dirs.Contains(MoveDirection.Up) && dirs.Contains(MoveDirection.Down)) {
          ground = Instantiate(theme.line, container);
          container.RotateAround(transform.position, Vector3.up, 90);
        }
        if (dirs.Contains(MoveDirection.Left) && dirs.Contains(MoveDirection.Down)) {
          ground = Instantiate(theme.curve, container);
        }
        if (dirs.Contains(MoveDirection.Left) && dirs.Contains(MoveDirection.Up)) {
          container.RotateAround(transform.position, Vector3.up, 90);
          ground = Instantiate(theme.curve, container);
        }
        if (dirs.Contains(MoveDirection.Up) && dirs.Contains(MoveDirection.Right)) {
          container.RotateAround(transform.position, Vector3.up, 180);
          ground = Instantiate(theme.curve, container);
        }
        if (dirs.Contains(MoveDirection.Right) && dirs.Contains(MoveDirection.Down)) {
          ground = Instantiate(theme.curve, container);
          container.RotateAround(transform.position, Vector3.up, 270);
        }
        break;

      case 3:
        ground = Instantiate(theme.triple, container);
        if (dirs.Contains(MoveDirection.Right) && dirs.Contains(MoveDirection.Left) && dirs.Contains(MoveDirection.Up)) container.RotateAround(transform.position, Vector3.up, 90);
        if (dirs.Contains(MoveDirection.Down) && dirs.Contains(MoveDirection.Right) && dirs.Contains(MoveDirection.Up)) container.RotateAround(transform.position, Vector3.up, 180);
        if (dirs.Contains(MoveDirection.Down) && dirs.Contains(MoveDirection.Left) && dirs.Contains(MoveDirection.Right)) container.RotateAround(transform.position, Vector3.up, 270);
        break;

      case 4:
        Instantiate(theme.fullcross, container);
        break;
    }

  }

}
