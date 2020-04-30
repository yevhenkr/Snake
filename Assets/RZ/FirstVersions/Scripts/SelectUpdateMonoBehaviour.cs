using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RZ
{
    // public class SelectUpdateMonoBehaviour:MonoBehaviourExtended
    public class SelectUpdateMonoBehaviour : MonoBehaviour
    {
        public UpdateMethod updateMethod = UpdateMethod.Update;
        public enum UpdateMethod
        {
            FixedUpdate,
            Update,
            UpdateAndFixedUpdate
        }

        public SelectUpdateMonoBehaviour()
        {
            updateMethod = UpdateMethod.UpdateAndFixedUpdate;
        }

        public SelectUpdateMonoBehaviour(UpdateMethod um)
        {
            updateMethod = um;
        }

        public bool IsUpdateToUse()
        {
            return updateMethod == UpdateMethod.Update ||
                updateMethod == UpdateMethod.UpdateAndFixedUpdate;
        }

        public bool IsFixedUpdateToUse()
        {
            return updateMethod == UpdateMethod.Update ||
                updateMethod == UpdateMethod.UpdateAndFixedUpdate;
        }
    }
}