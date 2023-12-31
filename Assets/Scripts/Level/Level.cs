
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using Feature = FurnitureFeature; // name of the furniture, x, y
public class Level : MonoBehaviour
{
    public const float TILE_SIZE = 1.0f;

    public float TOTAL_WIDTH;
    public float TOTAL_HEIGHT;
    public Vector3 TopLeftCenter;

    public int[,] Grid;

    public GameObject Toilet;

    public List<GameObject> renderedObjects;
    public List<List<string>> availableColors;
    public List<string> selectedColors;

    public LevelGene gene = null;

    public float fitness = 0;

    public LevelGene Gene {
        get { return gene; }
        set {
            gene = value;
            Dimensions = gene.dimensions;
            Grid = value.Grid;
            fitness = value.Fitness();
        }
    }


    [SerializeField] private Vector2Int _dimensions = new Vector2Int(10, 10);
    public Vector2Int Dimensions {
        get { return _dimensions; }
        set {
            _dimensions = value;
            this.gameObject.transform.localScale = new Vector3(value.x, 1, value.y);
            TOTAL_WIDTH = _dimensions.x * TILE_SIZE;
            TOTAL_HEIGHT = _dimensions.y * TILE_SIZE;
            Grid = new int[(int) _dimensions.y, (int) _dimensions.x];
            TopLeftCenter = _position + new Vector3(-(TOTAL_WIDTH - TILE_SIZE) / 2.0f, 0, (TOTAL_HEIGHT - TILE_SIZE) / 2.0f);
        }
    }

    [SerializeField] private Vector3 _position = new Vector3(0, 0, 0);
    public Vector3 Position {
        get { return _position; }
        set {
            _position = value;
            TopLeftCenter = _position + new Vector3(-(TOTAL_WIDTH - TILE_SIZE) / 2.0f, 0, (TOTAL_HEIGHT - TILE_SIZE) / 2.0f);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Dimensions = _dimensions;
        Position = _position;
        renderedObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckTileEmpty(int x, int y){
        return Grid[y, x] == 0f;
    }



    public bool PlaceItemAtTile(int x, int y){
        if (Grid[y, x] == 0){
            Grid[y, x] = 1;
            return true;
        } else {
            return false;
        }
    }

    public bool RemoveItemAtTile(int x, int y){
        if (Grid[y, x] == 1){
            Grid[y, x] = 0;
            return true;
        } else {
            return false;
        }
    }




    public void RenderObject(Vector3 tlpos, Feature feature, List<string> selectedColors){
        List<int> furn_dims = feature.dimensions;

        int dim_x = (feature.orientation == 0 || feature.orientation == 180) ? furn_dims[0] : furn_dims[1];
        int dim_y = (feature.orientation == 0 || feature.orientation == 180) ? furn_dims[1] : furn_dims[0];

        Vector3 centerTile = tlpos + TILE_SIZE * new Vector3(dim_x - 1, 1, -(dim_y - 1)) / 2;
        GameObject furniture = LevelGene.furnitureLibrary.GetPrefab(feature.name);
        // Instantiate(furniture, centerTile, furniture.transform.rotation);
        // tlpos.y = 2;
        // Instantiate(Toilet, tlpos, Quaternion.identity);

        // centerTile.y = 2;
        // Instantiate(furniture, centerTile + furniture.transform.position, furniture.transform.rotation);


        GameObject spawned = Instantiate(furniture, centerTile + furniture.transform.position, furniture.transform.rotation);
        spawned.transform.RotateAround(centerTile, Vector3.up, feature.orientation);
        spawned.transform.SetParent(this.gameObject.transform);
        // Debug.Log();
        // Instantiate(Toilet, spawned.transform.GetComponent<Renderer>().bounds.center, Quaternion.identity);

        // Create a temporary material
        //spawned.GetComponent<MeshRenderer>().material.color = Color.red;

        // Or use a material already made?
        // Debug.Log(selectedColors.Count);
        Material newMat;
        // int randomSelection = Random.Range(0, 10);
        // if (randomSelection > 4) {
        //     newMat = Resources.Load(selectedColors[0], typeof(Material)) as Material;
        // } else if (randomSelection > 3) {
        //     newMat = Resources.Load(selectedColors[1], typeof(Material)) as Material;
        // } else {
        //     newMat = Resources.Load(selectedColors[2], typeof(Material)) as Material;
        // }
        newMat = Resources.Load(selectedColors[Random.Range(0, selectedColors.Count)], typeof(Material)) as Material;
        spawned.GetComponent<MeshRenderer>().material = newMat;

        // This causes a NullReferenceException error
        // spawned.AddComponent<FurnitureColor>();

        renderedObjects.Add(spawned);
        // apply material to spawned object

        // Instantiate(furniture, centerTile, furniture.transform.rotation * Quaternion.Euler(0, feature.orientation, 0)).transform.position += furniture.transform.position;


    }

    public void Render(){
        Vector3 tile_pos = TopLeftCenter;
        // Debug.Log(tile_pos);
        for (int j = 0; j < Dimensions.y; j++){
            tile_pos.x = TopLeftCenter.x;
            for (int i = 0; i < Dimensions.x; i++){
                if (!CheckTileEmpty(i, j)){
                    renderedObjects.Add(Instantiate(Toilet, tile_pos, Quaternion.identity));
                    // Debug.Log(tile_pos);
                }
                tile_pos.x += TILE_SIZE;

            }
            tile_pos.z -= TILE_SIZE;
        }
    }

    public void RenderLevel(){
        // Debug.Log(gene.ToString());

        string ret = "";
        for (int j = 0; j < Dimensions.y; j++){
            for (int i = 0; i < Dimensions.x; i++){
                ret += (gene.Grid[j, i] != 0) ? string.Format(" [{0}] ", gene.Grid[j, i]) : " [-] ";
            }
            ret += "\n";
        }
        // Debug.Log(ret);

        availableColors = new List<List<string>>
        {
            // Source: https://www.mydomaine.com/interior-color-schemes-5187778
            new List<string> { "greenMoss", "orange", "white" },
            new List<string> { "blueSky", "orangeTan" },
            new List<string> { "greenDark", "redCandy" },
            new List<string> { "blueNavy", "pink", "orange" },
            new List<string> { "greenDark", "greyLight" },
            new List<string> { "blueNavy", "white" },
            new List<string> { "pinkPale", "greenBlue" },
            new List<string> { "blueSky", "greenEmerald" },
            new List<string> { "blueNavy", "blueBlack", "yellow" },
            new List<string> { "blueNavy", "greenGrass" },
            new List<string> { "greyGreen", "white", "blackDark" },
            new List<string> { "blackDark", "red" },
            new List<string> { "orangeTan", "blueNavy", "grey" },
            new List<string> { "pinkBlush", "blackDark" },
            new List<string> { "yellow", "blackGrey" },
            new List<string> { "greyLight", "orange" },
            new List<string> { "greenMoss", "orange", "white" },
            new List<string> { "white", "redPastel", "orange" },
            new List<string> { "blueSky", "blackGrey", "orangeTan" },
            new List<string> { "greenEmerald", "orange" },
            new List<string> { "blackDark", "white" }
        };
        selectedColors = availableColors[UnityEngine.Random.Range(0, availableColors.Count)];
        // Debug.Log(selectedColors.Count);

        Vector3 tile_pos = TopLeftCenter;

        foreach (Feature feature in gene.features){
            // Calculate top left tile position
            // Debug.Log(feature.position);
            Vector3 tlfurnpos= tile_pos +  new Vector3(TILE_SIZE * feature.position.x, 0, -TILE_SIZE * feature.position.y);
            // Debug.Log(tlfurnpos);

            RenderObject(tlfurnpos, feature, selectedColors);
        }


        // for (int j = 0; j < Dimensions.y; j++){
        //     tile_pos.x = TopLeftCenter.x;
        //     for (int i = 0; i < Dimensions.x; i++){
        //         if (!CheckTileEmpty(i, j)){
        //             GameObject deb = Instantiate(Toilet, tile_pos, Quaternion.identity);
        //             renderedObjects.Add(deb);
        //             deb.transform.SetParent(this.gameObject.transform);
        //         }
        //         tile_pos.x += TILE_SIZE;
        //     }
        //     tile_pos.z -= TILE_SIZE;
        // }
        // Debug.Log(renderedObjects.Count);
    }

    // public void DestroyLevel(){
    //     Debug.Log(renderedObjects.Count);
    //     Debug.Log("HALLO");
    //     Destroy(renderedObjects[renderedObjects.Count - 1]);
    //     renderedObjects.RemoveAt(renderedObjects.Count - 1);
    //     // while (renderedObjects.Count > 0){
    //     //     Debug.Log(renderedObjects.Count);
    //     //     Destroy(renderedObjects[renderedObjects.Count - 1]);
    //     //     renderedObjects.RemoveAt(renderedObjects.Count - 1);
    //     // }
    // }

}
