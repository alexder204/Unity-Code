using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem
{
    public class KeyItemController : MonoBehaviour
    {
        [SerializeField] private bool redDoor = false;
        [SerializeField] private bool redKey = false;

        public bool doorLocked = true;

        [SerializeField] private KeyInventory keyInventory = null;

        private DoorController doorObject;

        private void Start()
        {
            if (redDoor)
            {
                doorObject = GetComponent<DoorController>();
            }
        }

        public void ObjectInteraction()
        {
            if (redDoor)
            {
                doorObject.PlayLockedAnimation();
                doorLocked = true;
            }
            else if (redKey)
            {
                keyInventory.hasRedKey = true;
                doorLocked = false;
                gameObject.SetActive(false);
            }
        }
    }
}
