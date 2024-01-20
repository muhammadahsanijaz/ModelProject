using MoonKart;
using UnityEditor;
using UnityEngine;

public class EditorMenuItemsHandler : Editor
{
    
    
    [MenuItem("MoonKart/SelectVehicle")]
    static void SelectMainVehicle()
    {
        Selection.activeObject = Resources.Load<GameObject>("DB/Karts/Moon_Kart_1");
    }
    
    [MenuItem("MoonKart/CarSetting")]
    static void SelectCarSetting()
    {
        Selection.activeObject = Resources.Load<CarSettings>("Settings/CarSettings");
    }

    [MenuItem("MoonKart/CardsLibrary")]
    static void SelectCardLibrary()
    {
        //Selection.activeObject = Resources.Load<CardsLibrary>("Settings/CardsLibrary");
    }

    [MenuItem("MoonKart/ToogleGizmo")]
    static void ToggleGizmo()
    {
      //  StaticReferenceManager.ShowGizmo = !StaticReferenceManager.ShowGizmo;
    }

    [MenuItem("MoonKart/ClearAllData")]
    static void ClearAllData()
    {
       // Resources.Load<CardsLibrary>("Settings/CardsLibrary").ResetAllCardStatestAll();
    }

    [MenuItem("MoonKart/SelectEditorScript")]
    static void SelectMainThisScript()
    {
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/_Project/Scripts/Editor/EditorMenuItemsHandler.cs");
    }
    
    
    
    
}
