using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "game/ physics properties Container")]
public class PhysicsPropertyScriptable : ScriptableObject
{
    [SerializeField] List<PropertyData> propertyDatas;

    public PropertyData GetPropertyData(PhysicsProperty property)
    {
        var datas = propertyDatas.Where(e => e.physicsProperty == property).ToArray();
        if (datas.Length > 0) return datas[0];
        return new PropertyData() { physicsProperty = PhysicsProperty.None };
    }

}

[System.Serializable]
public struct PropertyData
{
    public PhysicsProperty physicsProperty;
    public RectTransform ValuePrefab;

}
