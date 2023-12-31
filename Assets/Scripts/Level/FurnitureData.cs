using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FurnitureData{
    public string name;
    public List<int> dimensions;
    public List<string> constraints;
    public List<string> tags;
    public List<string> models;
    // public static FurnitureData CreateFromJSON(string jsonString)
    // {
    //     return JsonUtility.FromJson<FurnitureData>(jsonString);
    // }
    public override string ToString(){
        return name + string.Format(" ({0}, {1})", dimensions[0], dimensions[1]);
    }

    public bool HasTag(string tag){
        return tags.Contains(tag);
    }

    public bool HasConstraint(string constraint){
        return constraints.Contains(constraint);
    }
    public bool HasModel(string model){
        return models.Contains(model);
    }



}

        // "general": [
        //     "chair",
        //     "curtains_double",
        //     "door_double",
        //     "door",
        //     "drawer_small",
        //     "drawer_medium",
        //     "lamp",
        //     "light_cube",
        //     "light_stand",
        //     "trashcan_small",
        //     "trashcan"
        // ],