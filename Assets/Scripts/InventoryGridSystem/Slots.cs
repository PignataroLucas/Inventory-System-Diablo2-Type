using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace InventoryGridSystem
{
    public class Slots : MonoBehaviour
    {
        public IntVector2 GridPos;
        public Text text;

        public GameObject storedItemObject;
        public IntVector2 StoredItemSize;
        public IntVector2 StoredItemStartPos;
        public GameItem storedItemClass;
        public bool isOccupied;

        public Slots(IntVector2 gridPos)
        {
            GridPos = gridPos;
        }

        private void Start()
        {
            text.text = GridPos.X + "," + GridPos.Y;
        }
    }
}
