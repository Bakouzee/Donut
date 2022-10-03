using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour {

    public enum CardinalDirection {
        NORTH,
        SOUTH,
        EAST,
        WEST,
        SOUTH_EAST,
        SOUTH_WEST,
        NORTH_EAST,
        NORTH_WEST
        
    }

    public static Vector2 ConvertDirectionToVector(CardinalDirection direction) {
        switch (direction) {
            
            case CardinalDirection.NORTH:
                return new Vector2(0f,1f);

            case CardinalDirection.SOUTH:
                return new Vector2(0f,-1f);
            
            case CardinalDirection.EAST:
                return new Vector2(1f,0f);
            
            case CardinalDirection.WEST:
                return new Vector2(-1f,0f);
             
            case CardinalDirection.SOUTH_EAST:
                return new Vector2(1f,-1f);
             
            case CardinalDirection.SOUTH_WEST:
                return new Vector2(-1f,-1f);
             
            case CardinalDirection.NORTH_EAST:
                return new Vector2(1f,1f);
             
            case CardinalDirection.NORTH_WEST:
                return new Vector2(1f,-1f);
            
            default:
                return new Vector2(0, -1f);
            
        }
    }
    
}
