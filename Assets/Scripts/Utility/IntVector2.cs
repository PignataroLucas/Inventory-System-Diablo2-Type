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

        public override string ToString()
        {
            return ("(" + X + ", " + Y + ")");
        }
        
        public static IntVector2 One
        {
        get { return new IntVector2(1, 1); }
        }
        public static IntVector2 OneNeg
        {
            get { return new IntVector2(-1, -1); }
        }
        public static IntVector2 Zero
        {
            get{ return new IntVector2(0, 0); }
        }
        public static IntVector2 Up
        {
            get { return new IntVector2(0, 1); }
        }
        public static IntVector2 Down
        {
            get { return new IntVector2(0, -1); }
        }
        public static IntVector2 Left
        {
            get { return new IntVector2(-1, 0); }
        }
        public static IntVector2 Right
        {
            get { return new IntVector2(1, 0); }
        }
        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X + b.X, a.X + b.Y);
        }
        public static IntVector2 operator +(IntVector2 a, int b)
        {
            return new IntVector2(a.X + b, a.Y + b);
        }
        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X - b.X, a.Y - b.Y);
        }
        public static IntVector2 operator -(IntVector2 a, int b)
        {
            return new IntVector2(a.X - b, a.Y - b);
        }
        public static IntVector2 operator *(IntVector2 a, int b)
        {
            return new IntVector2(a.X * b, a.Y * b);
        }
        public static IntVector2 operator /(IntVector2 a, int b)
        {
            return new IntVector2(a.X / b, a.Y / b);
        }
        public static int Area(IntVector2 a)
        {
            return (a.X * a.Y);
        }
        public static float Slope(IntVector2 a)
        {
            return ((float)a.Y / (float)a.X);
        }
        public static void Swap(ref IntVector2 a)
        {
            int temp = a.X;
            a.X = a.Y;
            a.Y = temp;
        }
        public static Vector2 Vector2(IntVector2 a)
        {
            return new Vector2(a.X, a.X);
        }
        public static Vector3 Vector3(IntVector2 a)
        {
            return new Vector3(a.X, a.Y);
        }
        public static bool operator ==(IntVector2 a, IntVector2 b)
        {
            if ((a.X == b.X) && (a.Y == b.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(IntVector2 a, IntVector2 b)
        {
            if ((a.X != b.X) || (a.Y != b.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool OrGreater(IntVector2 a, IntVector2 b)
        {
            if (a.X > b.X || a.Y > b.Y)
            {
                return true;
            }
            return false;
        }
        public static bool OrGreater(IntVector2 a, int b)
        {
            if (a.X > b || a.Y > b)
            {
                return true;
            }
            return false;
        }
        public static bool OrLesser(IntVector2 a, IntVector2 b)
        {
            if (a.X < b.X || a.Y < b.Y)
            {
                return true;
            }
            return false;
        }
        public static bool OrLesser(IntVector2 a, int b)
        {
            if (a.X < b || a.Y < b)
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object o)
        {
           return true;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
