using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FurnitureFeature{

    public static FurnitureLibrary furnitureLibrary = null;

    public string name;
    public string color;
    public Vector2Int position; // this is the top left tile position (0,0) is top left and (n, n) bottom right
    public List<int> dimensions;
    public float orientation;

    static public bool operator ==(FurnitureFeature item1, FurnitureFeature item2){
        if (object.ReferenceEquals(null, item1)){
            return object.ReferenceEquals(null, item2);
        } else if (object.ReferenceEquals(null, item2)){
            return object.ReferenceEquals(null, item1);
        } else return item1.name == item2.name &&
                        item1.position == item2.position &&
                        item1.dimensions[0] == item2.dimensions[0] &&
                        item1.dimensions[1] == item2.dimensions[1] &&
                        item1.orientation == item2.orientation;
    }


    static public bool operator !=(FurnitureFeature item1, FurnitureFeature item2){
        if (object.ReferenceEquals(null, item1)){
            return !object.ReferenceEquals(null, item2);
        } else if (object.ReferenceEquals(null, item2)){
            return !object.ReferenceEquals(null, item1);
        } else return item1.name != item2.name ||
                item1.position != item2.position ||
                item1.dimensions[0] != item2.dimensions[0] ||
                item1.dimensions[1] != item2.dimensions[1] ||
                item1.orientation != item2.orientation;
    }

    // Position is the top left tile of furniture

    public FurnitureFeature(string name, int x,  int z, float orientation=0, string color = null){
        this.name = name;
        this.color = color;
        this.position = new Vector2Int(x, z);
        this.orientation = orientation;

        List<int> dims = furnitureLibrary.GetFurnitureDimensions(name);
        this.dimensions = new List<int>() {
                dims[0],
                dims[1]
            };
    }

    public FurnitureFeature(FurnitureData featData, int x, int z, float orientation=0, string color = null)
    {
        this.name = featData.name;
        this.color = color;
        this.position = new Vector2Int(x, z);
        this.orientation = orientation;

        this.dimensions = new List<int>() {
                featData.dimensions[0],
                featData.dimensions[1],
        };
    }

    public FurnitureFeature(FurnitureFeature feat){
        this.name = feat.name;
        this.color = feat.color;
        this.position = new Vector2Int(feat.position.x, feat.position.y);
        this.orientation = feat.orientation;
        this.dimensions = new List<int>() {
            feat.dimensions[0],
            feat.dimensions[1]
        };
    }


   public override bool Equals(System.Object obj)
   {
      //Check for null and compare run-time types.
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else {
         FurnitureFeature other = (FurnitureFeature) obj;
         return this == other;
      }
   }

   public override int GetHashCode() // Change this
   {
      return ((int )position.x << 2) ^ (int)position.y;
   }

    // Define everything based off top left corner
    public bool OverlapsWith(FurnitureFeature other){
        // Simple AABB check
        Vector2Int my_tl = this.position;
        Vector2Int my_br = this.position + new Vector2Int(this.dimensions[0], this.dimensions[1]);
        Vector2Int other_tl = other.position;
        Vector2Int other_br = other.position + new Vector2Int(other.dimensions[0], other.dimensions[1]);

        // Debug.Log(string.Format("{0} vs {1}", name, other.name));
        // Debug.Log(string.Format("{0} < {1}", my_tl.x, other_br.x));
        // Debug.Log(string.Format("{0} > {1}", my_br.x, other_tl.x));
        // Debug.Log(string.Format("{0} < {1}", my_tl.y, other_br.y));
        // Debug.Log(string.Format("{0} > {1}", my_br.y, other_tl.y));



        return my_tl.x  < other_br.x &&
                my_br.x > other_tl.x &&
                my_tl.y  < other_br.y &&
                my_br.y > other_tl.y
                ;
    }

    public float GetArea(){
        return dimensions[0] * dimensions[1];
    }

    public bool HasTag(string tag){
        return furnitureLibrary.GetFurnitureTags(name).Contains(tag);
    }

    public bool HasConstraint(string constraint){
        return furnitureLibrary.GetFurnitureConstraints(name).Contains(constraint);
    }

    public bool HasModel(string model) => furnitureLibrary.GetFurnitureModels(name).Contains(model);


    public override string ToString(){
        List<int> furn_dims = this.dimensions;
        int dim_x = (orientation == 0 || orientation == 180) ? furn_dims[0] : furn_dims[1];
        int dim_y = (orientation == 0 || orientation == 180) ? furn_dims[1] : furn_dims[0];
        return string.Format("({0} TL: {1} - BR: {2}), O: {3}", name,
                 this.position,
                 this.position + new Vector2Int(dim_x - 1, dim_y - 1),
                 this.orientation);
    }
}