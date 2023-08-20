using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Feature = FurnitureFeature; // name of the furniture, x, y

public class LevelGenerator : MonoBehaviour
{
    public int MAX_LEVELS_PER_ROW = 5;
    public float PLOT_SIZE = 14;

    public List<LevelGene> population;


    [SerializeField] private Vector2Int _dimensions = new Vector2Int(10, 10);
    public Vector2Int Dimensions {
        get { return _dimensions; }
        set {
            _dimensions = value;
        }
    }

    [SerializeField] private int _numLevels = 3;
    public int NumLevels {
        get { return _numLevels; }
        set {
            _numLevels = value;
            TopLeftCenter = Vector3.zero;
            TopLeftCenter.y = -1;
            TopLeftCenter.x =
                (_numLevels <= MAX_LEVELS_PER_ROW) ? - ((_numLevels - 1) * PLOT_SIZE)/2.0f : -((MAX_LEVELS_PER_ROW - 1) * PLOT_SIZE) / 2.0f;
            // Debug.Log(TopLeftCenter);
        }
    }

    // [Serialized]


    public GameObject Level;
    public Vector3 TopLeftCenter;

    public void CreateLevel(Vector2Int dim, Vector3 pos){
        GameObject l = Instantiate(Level, pos, Quaternion.identity);
        Level gen_level = l.GetComponent<Level>();

        gen_level.Dimensions = dim;
        gen_level.GetComponent<Level>().Position = pos;

        gen_level.PlaceItemAtTile(0,0);
        // gen_level.PlaceItemAtTile(4,4);
        gen_level.PlaceItemAtTile(9,9);
        gen_level.PlaceItemAtTile(0,9);
        gen_level.PlaceItemAtTile(9,0);

        // Generation gets called here

        gen_level.GetComponent<Level>().Render();

    }

    public void CreateLevel(LevelGene gene, Vector3 pos){
        GameObject l = Instantiate(Level, pos, Quaternion.identity);
        Level gen_level = l.GetComponent<Level>();
        // gen_level.Dimensions = _dimensions;

        gen_level.Gene = gene;
        gen_level.GetComponent<Level>().Position = pos;

        // gen_level.PlaceItemAtTile(0,0);
        // // gen_level.PlaceItemAtTile(4,4);
        // gen_level.PlaceItemAtTile(9,9);
        // gen_level.PlaceItemAtTile(0,9);
        // gen_level.PlaceItemAtTile(9,0);

        // // Generation gets called here

        gen_level.GetComponent<Level>().RenderLevel();
    }



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_numLevels);
        NumLevels = _numLevels;  // Population Size
        population = new List<LevelGene>();

        LevelGene g = new LevelGene(Dimensions);

        // Feature feat1 = new Feature("bed_single", 0, 2);
        // Feature feat2 = new Feature("door", 0, 2);
        // Feature feat3 = new Feature("night_stand", 2, 2);
        // Feature feat4 = new Feature("drawer_small", 4, 0);

        // Debug.Log(feat1);
        // Debug.Log(feat2);
        // Debug.Log(feat3);
        // Debug.Log(feat4);

        // Debug.Log(feat1.OverlapsWith(feat1));
        // Debug.Log(feat1.OverlapsWith(feat2));
        // Debug.Log(feat1.OverlapsWith(feat3));
        // Debug.Log(feat1.OverlapsWith(feat4));

        // Debug.Log(ToString());

        // LevelGene try1 = TryPlaceObject(feat1);
        // Debug.Log(try1.ToString());

        // LevelGene try2 = try1.TryPlaceObject(feat2);
        // Debug.Log(try2.ToString());

        // LevelGene try3 = try2.TryPlaceObject(feat3);
        // Debug.Log(try3.ToString());

        // LevelGene try4 = try3.TryPlaceObject(feat4);
        // Debug.Log(try4.ToString());

        LevelGene randlevel = LevelGene.GenerateRandomLevelGene(Dimensions, 10);
        Debug.Log(randlevel.ToString());
        for (int i = 0; i < _numLevels; i++){
            population.Add(randlevel);
        }
        RenderPopulation();
    }

    // Update is called once per frame
    void Update()
    {

    }


    List<LevelGene> elitestSelection(){
        List<LevelGene> results = new List<LevelGene>();
        // Sort from Biggest to smallest
        List<LevelGene> randPop = new List<LevelGene>();
        // randPop = populations.OrderBy(x => x.count).ToList();
        for(int i = 0; i < population.Count % 2; i++){
            results.Add(population[i]);
        }
        return results;
    }

    List<LevelGene> tourneySelection(){
        List<LevelGene> results = new List<LevelGene>();
        List<LevelGene> randPop = new List<LevelGene>();
        // randPop = populations.OrderBy(x=> Random.Shared.Next()).ToList();
        for(int i = 0; population.Count < results.Count % 2; i++){
            LevelGene contestantA = randPop[i];
            LevelGene contestantB = randPop[population.Count - i];
            if (contestantB.Fitness() > contestantA.Fitness()){
                results.Add(contestantB);
            }
            else{
                results.Add(contestantA);
            }
        }
        return results;
    }

    List<LevelGene> generateSuccessors(){ // Octavio
        List<LevelGene> results = new List<LevelGene>();
        List<LevelGene> selectList = elitestSelection();
        selectList.AddRange(tourneySelection());
        for(int i = 0; i < selectList.Count % 2; i++){
            LevelGene parentFirst = selectList[i];
            LevelGene parentSecond = selectList[-(i+1)];
            results.AddRange(parentFirst.GenerateChildren(parentSecond));
            // results.Add(parentSecond.generateChildren);
        }
        return results;
    }


    void Render(){
        int count = 0;
        Vector3 plot_pos = TopLeftCenter;
        for (int k = 0; k < _numLevels; k++){
            if (count == 5){
                Debug.Log("HELLO");
                Debug.Log(_numLevels - k );
                plot_pos.x = (_numLevels - k  < MAX_LEVELS_PER_ROW) ? -(((_numLevels - k - 1) * PLOT_SIZE)/2.0f)  : TopLeftCenter.x;
                plot_pos.z += PLOT_SIZE;
                count = 0;
            }

            CreateLevel(Dimensions, plot_pos);
            plot_pos.x += PLOT_SIZE;
            count += 1;
        }
    }

    void RenderPopulation(){
        int count = 0;
        Vector3 plot_pos = TopLeftCenter;
        for (int k = 0; k < _numLevels; k++){
            if (count == 5){
                Debug.Log(_numLevels - k );
                plot_pos.x = (_numLevels - k  < MAX_LEVELS_PER_ROW) ? -(((_numLevels - k - 1) * PLOT_SIZE)/2.0f)  : TopLeftCenter.x;
                plot_pos.z += PLOT_SIZE;
                count = 0;
            }

            CreateLevel(population[k], plot_pos);
            plot_pos.x += PLOT_SIZE;
            count += 1;
        }
    }


}

