using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Finish by Thursday


using Feature = System.Tuple<string, float, float>; // name of the furniture, x, y

// Represents an individual inside the population
public class LevelGene {

    //static // What would the json load in?

    List<Feature> features;

    bool isValid;

    LevelGene(){
        features = new List<Feature>();
    }

    static void LoadAllFurniture(){ // Arrian
        // JsonUtility"../TileData/FurnitureData.json"
    }

    static LevelGene GenerateRandomLevelGene(){ // Arrian
        return new LevelGene();
    }

    static LevelGene GenerateEmptyLevelData(){ // Angela
        LevelGene emptyLevel = new LevelGene();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                emptyLevel.features.Add(new Feature("empty", i, j));
            }
        }
        return emptyLevel;
    }


    Dictionary<string, float> Metrics(){ // THE MEAT (HOw would we define this???? Someone research) Angela
        // Balance
        float balance = 0.0f;

        // Harmony
        float harmony = 0.0f;

        // Emphasis
        float emphasis = 0.0f;

        // Contrast
        float contrast = 0.0f;

        // Scale and Proportion
        float scale = 0.0f;

        // Deatils
        float details = 0.0f;

        // Rhythm
        float rhythm = 0.0f;

        var metrics = new Dictionary<string, float>(){
            {"Balance", balance},
            {"Harmony", harmony},
            {"Emphasis", emphasis},
            {"Contrast", contrast},
            {"Scale", scale},
            {"Details", details},
            {"Rhythm", rhythm}
        };

        return metrics;
    }

    float Fitness(){
        var tileMetrics = Metrics();
        float fitness = 0.0f;

        // How heavily each category should affect overall fitness
        float balance = 0.0f;
        float harmony = 0.0f;
        float emphasis = 0.0f;
        float contrast = 0.0f;
        float scale = 0.0f;
        float details = 0.0f;
        float rhythm = 0.0f;

        return fitness;
    }

    bool ValidateSelf(){ // n^2 complexity
        return true;
    }

    bool ValidateFeatureAddition(Feature feat, bool allowDuplicates){ // Can I add this item into myself? O(n) complexity // THE MEAT // Arrian
        // go through every piece of furniture in the features
        // validate with the rest of the
        return false;
    }

    LevelGene Mutate(){ // A mutate function // Angela
        // Either
        int actions = Random.Range(0, 4);

        switch (actions)
        {
        // Modifies the placement of an item
            case 0:
                break;

        // Adds a new item
            case 1:
                break;

        // Removes an item
            case 2:
                break;

            // Do nothing
            default:
                break;
        }
        return new LevelGene();
    }

    LevelGene PlaceObject(){ // Alan
        return new LevelGene();
    }

    LevelGene RemoveObject(){ // Octavio
        return new LevelGene();
    }

    List<LevelGene> GenerateChildren(LevelGene other) { // Alan
        // crossover


        // validation is needed
        // Consider feasible and infeasible population?
        return new List<LevelGene>();

    }

}