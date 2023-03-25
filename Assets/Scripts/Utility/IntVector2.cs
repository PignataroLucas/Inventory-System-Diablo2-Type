using UnityEngine;

/*

--This script defines an IntVector2 structure, which is a representation of a two-dimensional (2D) vector that uses 
  integers instead of floating point numbers as in Unity's Vector2 structure.--




*/
namespace Utility
{
    
    public struct IntVector2
    {

        public int X,Y;

        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IntVector2(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }

        public IntVector2(Vector2 n)
        {

            X = (int)n.x;
            Y = (int)n.y;
        }
        
        
        
    }
}
