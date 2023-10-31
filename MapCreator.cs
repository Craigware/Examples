using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [Header("Layers Related To Height")]
    public List<Texture2D> layers = new List<Texture2D>();

    [Header("Objects Related To Colors")]
    public List<GameObjectColorClass> objects = new List<GameObjectColorClass>();

    void Start()
    {
        if (layers.Count != 0) {
            GenerateMap();
        }
    }
    
    public void GenerateMap(){
        for (int worldHeight = 0; worldHeight < layers.Count; worldHeight ++){
            Texture2D _currentLayer = layers[worldHeight];
            GameObject _currentContainer = new GameObject("Layer " + worldHeight);
            _currentContainer.transform.SetParent(transform);
            _currentContainer.name = "Layer " + worldHeight;
            for (int mapHeight = 0; mapHeight < _currentLayer.height; mapHeight ++){
                for (int mapWidth = 0; mapWidth < _currentLayer.width; mapWidth ++) {
                    for (int _currentObj = 0; _currentObj < objects.Count; _currentObj ++){
                        if (_currentLayer.GetPixel(mapWidth,mapHeight) == objects[_currentObj].tileColor) {
                            GameObject worldObject = GameObject.Instantiate(objects[_currentObj].tileObject);
                            worldObject.transform.position = new Vector3(mapWidth,worldHeight,mapHeight);
                            worldObject.transform.Rotate(objects[_currentObj].tileRotation, Space.Self);
                            worldObject.transform.SetParent(_currentContainer.transform);
                        }
                    }
                }
            }
        }
    }
    public void DeleteMap(){
        Debug.Log(transform.childCount);
        for (int _currentChild = 0; _currentChild < transform.childCount; _currentChild++){
            GameObject.DestroyImmediate(transform.GetChild(_currentChild).gameObject);
        }
    }
}
