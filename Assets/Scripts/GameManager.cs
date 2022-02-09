using System.Collections;
using System.Collections.Generic;

using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine;

using DG.Tweening;

public enum TypeTheme { Nature, City, Water };
public enum TypeBloc { None, Classic, Custom, Start, End };
public enum TypeCross { Line, Triple, Fullcross, Curve, EndLine };

[System.Serializable]
public class TileData {
  public TypeBloc type;
  public Cube cube;
  public List<TileData> neighbors;
  public List<MoveDirection> dirs;

  public TileData() {
    type = TypeBloc.None;
    neighbors = new List<TileData>();
    dirs = new List<MoveDirection>();
  }

  public void Set(TypeBloc newType) {
    type = newType;
  }
}

public class GameManager : MonoBehaviour {
  public static GameManager instance = null;

  [Header("Components")]
  public Transform trash;
  public MeshRenderer groundPlane;
  public CameraController cam;

  [Header("Prefabs")]
  public Cube fixCube;
  public Cube customCube;

  [Header("Theme")]
  public SOTheme[] themes;

  [Header("Custom")]
  public Color[] colors;

  [Header("Levels")]
  public SOLevel[] levels;

  private int level;
  private TileData[,] mapArray; /*Array multi: fist param => idLine, second => idCol */

  void Awake() {
    if (instance != null && instance != this) {
      Destroy(gameObject);
      return;
    } else {
      instance = this;
    }
    groundPlane.material = new Material(groundPlane.material);
    level = PlayerPrefs.GetInt("Level", 1);
  }

  void Start() {
    SetLevel();
  }

  void Clear() {
    foreach (Transform child in trash) {
      Destroy(child.gameObject);
    }
    cam.Reset();
  }

  void GenerateMap() {
    Tilemap map = levels[(level-1)%levels.Length].map;
    BoundsInt bounds = map.cellBounds;
    TileBase[] allTiles = map.GetTilesBlock(bounds);
    mapArray = new TileData[map.size.y, map.size.x];
    SOTheme theme = themes[Random.Range(0, themes.Length)];

    int x = 0, y = 0;
    foreach (TileBase tile in allTiles) {
      mapArray[y, x] = new TileData();
      if (tile != null) {
        cam.offsetMin.x = Mathf.Min(x, cam.offsetMin.x);
        cam.offsetMax.x = Mathf.Max(x, cam.offsetMin.x);
        cam.offsetMin.z = Mathf.Min(y, cam.offsetMin.z);
        cam.offsetMax.z = Mathf.Max(y, cam.offsetMin.z);

        // Debug.Log("line: " + y + " col: " + x + " " + tile.name);
        switch (tile.name) {
          case "ColorMappingSet_0":
            mapArray[y, x].Set(TypeBloc.Start);
            cam.CenterOn(new Vector3(x, 0, y));
            break;
          case "ColorMappingSet_1":
            mapArray[y, x].Set(TypeBloc.End);
            break;
          case "ColorMappingSet_2":
            mapArray[y, x].Set(TypeBloc.Classic);
            break;
          case "ColorMappingSet_3":
            mapArray[y, x].Set(TypeBloc.Custom);
            break;
        }
      }
      x++;
      if (x >= mapArray.GetLength(1)) {
        x = 0;
        y++;
      }
    }

    for (int l = 0; l < mapArray.GetLength(0); l++) {
      for (int c = 0; c < mapArray.GetLength(1); c++) {
        if (l-1 >= 0 && mapArray[l-1, c].type != TypeBloc.None) mapArray[l, c].dirs.Add(MoveDirection.Down);
        if (c-1 >= 0 && mapArray[l, c-1].type != TypeBloc.None) mapArray[l, c].dirs.Add(MoveDirection.Left);
        if (l+1 < mapArray.GetLength(0) && mapArray[l+1, c].type != TypeBloc.None) mapArray[l, c].dirs.Add(MoveDirection.Up);
        if (c+1 < mapArray.GetLength(1) && mapArray[l, c+1].type != TypeBloc.None) mapArray[l, c].dirs.Add(MoveDirection.Right);
        Vector3 pos = new Vector3(c, 0, l);
        if (mapArray[l, c].type != TypeBloc.None) {
          if (mapArray[l, c].type == TypeBloc.Custom) {
            mapArray[l, c].cube = Instantiate(customCube, pos, Quaternion.identity, trash);
          } else {
            mapArray[l, c].cube = Instantiate(fixCube, pos, Quaternion.identity, trash);
          }
          mapArray[l, c].cube.groundController.SetAboutDirections(mapArray[l, c].dirs , theme);
        }
      }
    }
  }

  void SetLevel() {
    Clear();
    GenerateMap();
    Color newColor = colors[Random.Range(0, colors.Length)];
    newColor.a = .75f;
    groundPlane.material.DOColor(newColor, "FogColor", 1).SetEase(Ease.Linear);
  }

//   public bool WayAchieved() {
//     /*TODO resolver*/
//     return false;
//   }
}
