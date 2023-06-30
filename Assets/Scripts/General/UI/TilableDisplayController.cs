using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TilableDisplayController
{
    protected GameObject _unitPrefab;
    protected GameObject[] _units;

    public TilableDisplayController(GameObject unitPrefab)
    {
        _unitPrefab = unitPrefab;
    }

    public int GetAmmountOfUnits => _units.Length;

    public void UpdateDisplay(int value)
    {
        for(int i = _units.Length-1; i >= 0; i--) 
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
        _units[i].GetComponent<Image>().color = Color.white;
    }

    protected virtual void DrawIncativeTile(int i)
    {
        _units[i].GetComponent<Image>().color = Color.gray;
    }

    public void SetupDisplay(int displayLenght, GameObject UIObject)  
    {
        if (displayLenght < 0) { throw new ArgumentOutOfRangeException(); }

        if(_units!=null && _units.Length>0)
        {
            foreach (GameObject unit in _units)
            {
                GameObject.Destroy(unit);
            }
        }

        _units = new GameObject[displayLenght];
        
        for (int i = 0; i < displayLenght; i++)
        {
            _units[i] = Object.Instantiate<GameObject>(_unitPrefab, UIObject.transform);

            SetupUnit(i);
        }
        
    }

    protected virtual void SetupUnit(int i)
    {
        throw new NotImplementedException();
    }
}