using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace InventoryGridSystem
{
    public class Slots : MonoBehaviour
    {
        public IntVector2 GridPos;
        public TMP_Text text;

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
            text.text = GridPos.X + "," + GridPos.Y;
        }
    }
}
