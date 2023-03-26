using UnityEngine;

namespace Item
{
    public class CreateItemManager : MonoBehaviour
    {
        public bool willAddToList = false;
        


        public void RandomItem()
        {
            if (InventoryItemInteraction.selectedItem == null)
            {
                GameItem newItem = new GameItem();
                GameItem.SetItemValues(newItem,Random.Range(0,3),Random.Range(1,101),Random.Range(0,4));
                SpawnOrAddItem(newItem);
            }    
        }
        private void SpawnOrAddItem(GameItem item)
        {
            if (willAddToList == false)
            {
                //IMPLEMENTAR!!!!!!---
                //GameObject itemObject = itemEquipPool.GetObject();
            }
        }
        
    }
}
