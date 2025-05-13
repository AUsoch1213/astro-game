using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class NillBuildSite : BuildSite
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}
