using UnityEngine;
using Utility;

namespace Item
{
    public class CreateItemManager : MonoBehaviour
    {
        public bool willAddToList = false;
        public ObjectPoolManager poolManager;


        public void RandomItem()
        {
            if (InventoryItemInteraction.selectedItem == null)
            {
                GameItem newItem = new GameItem();
                GameItem.SetItemValues(newItem,Random.Range(0,5),Random.Range(1,101),Random.Range(1,5));
                SpawnOrAddItem(newItem);
            }    
        }
        private void SpawnOrAddItem(GameItem item)
        {
            if (willAddToList == false)
            {
                GameObject itemObject = poolManager.GetObject();
                itemObject.GetComponent<InventoryItemInteraction>().SetItemObject(item);
                InventoryItemInteraction.SetSelectableItem(itemObject);
            }
            else
            {
                //IMPLEMENTAR!!!!
                
                Debug.LogWarning("Item Added to List");
            }
        }
        
    }
}
