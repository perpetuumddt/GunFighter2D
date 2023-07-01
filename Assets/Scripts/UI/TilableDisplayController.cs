using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Gunfighter.UI
{
    public abstract class TilableDisplayController
    {
        protected GameObject UnitPrefab;
        protected GameObject ParentGameObj;
        protected GameObject[] Units;

        public TilableDisplayController(GameObject unitPrefab, GameObject parentGameObj)
        {
            UnitPrefab = unitPrefab;
            ParentGameObj = parentGameObj;
        }

        public int GetAmmountOfUnits => Units.Length;

        public GameObject GetUnit(int i)
        {
            if (i < 0 || i >= Units.Length) throw new ArgumentOutOfRangeException();
            return Units[i];
        }

        public void UpdateDisplay(int value)
        {
            for(int i = Units.Length-1; i >= 0; i--) 
            {
                if(i>=value)
                {
                    DrawIncativeTile(i);
                }
                else
                {
                    DrawActiveTile(i);
                }
            }
        }

        protected virtual void DrawActiveTile(int i)
        {
            Units[i].GetComponent<Image>().color = Color.white;
        }

        protected virtual void DrawIncativeTile(int i)
        {
            Units[i].GetComponent<Image>().color = Color.gray;
        }

        public void SetupDisplay(int displayLenght)  
        {
            if (displayLenght < 0) { throw new ArgumentOutOfRangeException(); }

            if(Units!=null && Units.Length>0)
            {
                foreach (GameObject unit in Units)
                {
                    GameObject.Destroy(unit);
                }
            }

            Units = new GameObject[displayLenght];
        
            for (int i = 0; i < displayLenght; i++)
            {
                Units[i] = Object.Instantiate<GameObject>(UnitPrefab, ParentGameObj.transform);

                SetupUnit(i);
            }
            UpdateDisplay(displayLenght); // Update all tiles to draw them using DrawActiveTile.
        }

        protected abstract void SetupUnit(int i);
    }
}