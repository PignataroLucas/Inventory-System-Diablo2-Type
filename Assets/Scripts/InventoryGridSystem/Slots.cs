using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

namespace InventoryGridSystem
{
    public class Slots : MonoBehaviour
    {
        public IntVector2 GridPos;
        public Text text;

        public GameObject storedItemObject;
        [FormerlySerializedAs("StoredItemSize")] public IntVector2 storedItemSize;
        [FormerlySerializedAs("StoredItemStartPos")] public IntVector2 storedItemStartPos;
        public GameItem storedItemClass;
        public bool isOccupied;

        public Slots(IntVector2 gridPos)
        {
            GridPos = gridPos;
        }

        private void Start()
        {
            text.text = GridPos.x + "," + GridPos.y;
        }
    }
}
