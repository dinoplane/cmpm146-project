using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public const float TILE_SIZE = 1.0f;

    public float TOTAL_WIDTH;
    public float TOTAL_HEIGHT;
    public Vector3 TopLeftCenter;


    public GameObject Toilet;


    [SerializeField] private Vector2 _dimensions = new Vector2(10, 10);
    public Vector2 Dimensions {
        get { return _dimensions; }
        set {
            _dimensions = value;
            TOTAL_WIDTH = _dimensions.x * TILE_SIZE;
            TOTAL_HEIGHT = _dimensions.y * TILE_SIZE;
            Grid = new int[(int) _dimensions.y, (int) _dimensions.x];
            TopLeftCenter = _position - new Vector3((TOTAL_WIDTH - TILE_SIZE) / 2.0f, 0, (TOTAL_HEIGHT - TILE_SIZE) / 2.0f);
        }
    }

    [SerializeField] private Vector3 _position = new Vector3(0, 0, 0);
    public Vector3 Position {
        get { return _position; }
        set {
            _position = value;
            TopLeftCenter = _position - new Vector3((TOTAL_WIDTH - TILE_SIZE) / 2.0f, 0, (TOTAL_HEIGHT - TILE_SIZE) / 2.0f);
        }
    }

    public int[,] Grid;

    // Start is called before the first frame update
    void Start()
    {
        Dimensions = _dimensions;
        Position = _position;

        for (int j = 0; j < Dimensions.y; j++){
            for (int i = 0; i < Dimensions.x; i++){
                Grid[j, i] = 1;
            }
        }
        Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckTileEmpty(int x, int y){
        return Grid[y, x] == 0f;
    }

    bool PlaceItemAtTile(int x, int y){
        if (Grid[y, x] == 0){
            Grid[y, x] = 1;
            return true;
        } else {
            return false;
        }
    }

    bool RemoveItemAtTile(int x, int y){
        if (Grid[y, x] == 1){
            Grid[y, x] = 0;
            return true;
        } else {
            return false;
        }
    }

    void Render(){
        Vector3 tile_pos = TopLeftCenter;
        for (int j = 0; j < Dimensions.y; j++){
            for (int i = 0; i < Dimensions.x; i++){
                if (!CheckTileEmpty(i, j)){
                    Instantiate(Toilet, tile_pos, Quaternion.identity);
                }
                tile_pos.x += TILE_SIZE;
            }
            tile_pos.x = TopLeftCenter.x;
            tile_pos.z += TILE_SIZE;
        }
    }

}
