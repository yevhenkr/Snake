using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RZ
{
    [DisallowMultipleComponent]
    public class ToggleGroupExtended : ToggleGroup
    {
        public bool allowMultipleOn = true;
        public Toggle[] toggles;

        // new void Reset()
#pragma warning disable 0109
        new void Reset()
        {
            // base.Reset();
            allowSwitchOff = true;
            toggles = gameObject.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].group = null;
            }
        }
#pragma warning restore 0109

        List<Toggle> on = new List<Toggle>();
        void Update()
        {
            on.Clear();

            ToggleGroup g = allowMultipleOn ? null : this;
            for (int i = 0; i < toggles.Length; i++)
            {
                Toggle t = toggles[i];
                if (t.isOn) on.Add(t);
                t.group = g;
            }

            if (!allowSwitchOff)
            {
                if (on.Count == 0)
                {
                    toggles[0].isOn = true;
                    on.Add(toggles[0]);
                }
                if (on.Count == 1)
                {
                    on[0].group = this;
                }
            }
        }

        public Toggle[] GetTogglesOnAndEnabled() { return _GetToggles(true, true); }
        public Toggle[] GetTogglesOn() { return _GetToggles(true); }
        public Toggle[] GetTogglesOff() { return _GetToggles(false); }
        Toggle[] _GetToggles(bool isOn, bool onlyEnabled = false)
        {
            List<Toggle> l = new List<Toggle>();
            for (int i = 0; i < toggles.Length; i++)
            {
                Toggle t = toggles[i];
                if (isOn == t.isOn) l.Add(t);
            }
            return l.ToArray();
        }

        public void SetToggleOnSingle(Toggle toggle)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                Toggle t = toggles[i];
                t.isOn = t == toggle;
                // if(t.group!=null) t.group.NotifyToggleOn(t); // is not part of ToggleGroup - ERROR
            }
        }

        public void SetToggleOnSingle(string toggleName)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                Toggle t = toggles[i];
                t.isOn = t.name == toggleName;
                // if(t.group!=null) t.group.NotifyToggleOn(t); // is not part of ToggleGroup - ERROR
            }
        }
    }
}