using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ModToolScripts
{
    public static void ChangeSelectedToDirt(bool hideObjects)
    {
        ModMapWrapper modMapWrapper = GameObject.FindFirstObjectByType<ModMapWrapper>();
        Transform parent = modMapWrapper.transform.Find("DirtObjects");
        Object[] selectedItems = Selection.objects;
        foreach (Object obj in selectedItems)
        {
            GameObject item = (GameObject)obj;
            item.transform.parent = parent;
            if (hideObjects)
            {
                SceneVisibilityManager.instance.Hide(item, true);
            }
        }
    }
    public static void ChangeSelectedToConcrete(bool hideObjects)
    {
        ModMapWrapper modMapWrapper = GameObject.FindFirstObjectByType<ModMapWrapper>();
        Transform parent = modMapWrapper.transform.Find("ConcreteObjects");
        Object[] selectedItems = Selection.objects;
        foreach (Object obj in selectedItems)
        {
            GameObject item = (GameObject)obj;
            item.transform.parent = parent;
            if (hideObjects)
            {
                SceneVisibilityManager.instance.Hide(item, true);
            }
        }
    }
    public static void ChangeSelectedToMetal(bool hideObjects)
    {
        ModMapWrapper modMapWrapper = GameObject.FindFirstObjectByType<ModMapWrapper>();
        Transform parent = modMapWrapper.transform.Find("MetalObjects");
        Object[] selectedItems = Selection.objects;
        foreach (Object obj in selectedItems)
        {
            GameObject item = (GameObject)obj;
            item.transform.parent = parent;
            if (hideObjects)
            {
                SceneVisibilityManager.instance.Hide(item, true);
            }
        }
    }
    public static void ChangeSelectedToWood(bool hideObjects)
    {
        ModMapWrapper modMapWrapper = GameObject.FindFirstObjectByType<ModMapWrapper>();
        Transform parent = modMapWrapper.transform.Find("WoodObjects");
        Object[] selectedItems = Selection.objects;
        foreach (Object obj in selectedItems)
        {
            GameObject item = (GameObject)obj;
            item.transform.parent = parent;
            if (hideObjects)
            {
                SceneVisibilityManager.instance.Hide(item, true);
            }
        }
    }
    public static void ChangeSelectedToGlass(bool hideObjects)
    {
        ModMapWrapper modMapWrapper = GameObject.FindFirstObjectByType<ModMapWrapper>();
        Transform parent = modMapWrapper.transform.Find("GlassObjects");
        Object[] selectedItems = Selection.objects;
        foreach (Object obj in selectedItems)
        {
            GameObject item = (GameObject)obj;
            item.transform.parent = parent;
            if (hideObjects)
            {
                SceneVisibilityManager.instance.Hide(item, true);
            }
        }
    }
    public static void ChangeToMaterialType(GameObject toChange, string type, bool hideObjects)
    {
        ModMapWrapper modMapWrapper = GameObject.FindFirstObjectByType<ModMapWrapper>();
        Dictionary<string, string> typeToMaterialParent = new Dictionary<string, string>();
        typeToMaterialParent["dirt"] = "DirtObjects";
        typeToMaterialParent["concrete"] = "ConcreteObjects";
        typeToMaterialParent["metal"] = "MetalObjects";
        typeToMaterialParent["wood"] = "WoodObjects";
        typeToMaterialParent["glass"] = "GlassObjects";
        Transform parent = modMapWrapper.transform.Find(typeToMaterialParent[type.ToLower()]);
        GameObject item = toChange;
        item.transform.parent = parent;
        if (hideObjects)
        {
            SceneVisibilityManager.instance.Hide(item, true);
        }
    }
    public static void SetAllWithSameMaterialAsSelected(Transform parent, string type, bool addMeshCollider, bool hideObjects)
    {
        GameObject[] materialObjs = Selection.gameObjects;
        foreach (GameObject obj in materialObjs)
        {
            MeshRenderer materialRenderer = obj.GetComponent<MeshRenderer>();
            Material material = null;
            foreach (Material mat in materialRenderer.sharedMaterials)
            {
                if (!mat.name.ToLower().Contains("toolsnodraw"))
                {
                    material = materialRenderer.sharedMaterial;
                    break;
                }
            }
            MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>();

            if (material != null)
            {
                foreach (MeshRenderer renderer in renderers)
                {
                    foreach (Material mat in renderer.sharedMaterials)
                    {
                        string matName = mat.name.ToLower();
                        if (matName == material.name.ToLower())
                        {
                            ChangeToMaterialType(renderer.gameObject, type, hideObjects);
                            if (addMeshCollider)
                            {
                                MeshCollider existingCollider = renderer.gameObject.GetComponent<MeshCollider>();
                                if (existingCollider == null)
                                {
                                    existingCollider = renderer.gameObject.AddComponent<MeshCollider>();
                                    MeshFilter meshFilter = renderer.gameObject.GetComponent<MeshFilter>();
                                    existingCollider.sharedMesh = meshFilter.sharedMesh;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public static void SetAllAccordingToMaterial(Transform parent, MapModdingTools.MaterialNamesForObjectTypes extraMats, bool addMeshCollider)
    {
        MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>();
        int metalCount = 0;
        int concreteCount = 0;
        int woodCount = 0;
        int glassCount = 0;
        int dirtCount = 0;
        foreach (MeshRenderer renderer in renderers)
        {
            metalCount = 0;
            concreteCount = 0;
            woodCount = 0;
            glassCount = 0;
            dirtCount = 0;
            string currentType = null;
            int currentMax = 0;
            foreach (Material mat in renderer.sharedMaterials)
            {
                string matName = mat.name.ToLower();
                if (matName.Contains("metal")) metalCount++;
                else
                {
                    if (extraMats.metalMaterialNames.Length > 0)
                    foreach (string extraMatName in extraMats.metalMaterialNames)
                    {
                        if (matName.Contains(extraMatName) && extraMatName != "")
                        {
                            metalCount++;
                            break;
                        }
                    }
                }
                if (matName.Contains("dirt") || matName.Contains("ground") || matName.Contains("grass") || matName.Contains("gravel") || matName.Contains("sand")) dirtCount++;
                else
                {
                    if (extraMats.dirtMaterialNames.Length > 0)
                    foreach (string extraMatName in extraMats.dirtMaterialNames)
                    {
                        if (matName.Contains(extraMatName) && extraMatName != "")
                        {
                            dirtCount++;
                            break;
                        }
                    }
                }
                if (matName.Contains("wood") || matName.Contains("tree")) woodCount++;
                else
                {
                    if (extraMats.woodMaterialNames.Length > 0)
                    foreach (string extraMatName in extraMats.woodMaterialNames)
                    {
                        if (matName.Contains(extraMatName) && extraMatName != "")
                        {
                            woodCount++;
                            break;
                        }
                    }
                }
                if (matName.Contains("glass") || matName.Contains("window")) glassCount++;
                else
                {
                    if (extraMats.glassMaterialNames.Length > 0)
                    foreach (string extraMatName in extraMats.glassMaterialNames)
                    {
                        if (matName.Contains(extraMatName) && extraMatName != "")
                        {
                            glassCount++;
                            break;
                        }
                    }
                }
                if (matName.Contains("concrete") || matName.Contains("stone") || matName.Contains("rock")) concreteCount++;
                else
                {
                    if (extraMats.concreteMaterialNames.Length > 0)
                    foreach (string extraMatName in extraMats.concreteMaterialNames)
                    {
                        if (matName.Contains(extraMatName) && extraMatName != "")
                        {
                            concreteCount++;
                            break;
                        }
                    }
                }
            }
            if (metalCount > currentMax)
            {
                currentType = "metal";
                currentMax = metalCount;
            }
            if (concreteCount > currentMax)
            {
                currentType = "concrete";
                currentMax = concreteCount;
            }
            if (woodCount > currentMax)
            {
                currentType = "wood";
                currentMax = woodCount;
            }
            if (glassCount > currentMax)
            {
                currentType = "glass";
                currentMax = glassCount;
            }
            if (dirtCount > currentMax)
            {
                currentType = "dirt";
                currentMax = dirtCount;
            }
            if (currentMax > 0)
            {
                ChangeToMaterialType(renderer.gameObject, currentType, true);
            }

            if (addMeshCollider)
            {
                MeshCollider existingCollider = renderer.gameObject.GetComponent<MeshCollider>();
                if (existingCollider == null)
                {
                    existingCollider = renderer.gameObject.AddComponent<MeshCollider>();
                    MeshFilter meshFilter = renderer.gameObject.GetComponent<MeshFilter>();
                    existingCollider.sharedMesh = meshFilter.sharedMesh;
                }
            }
        }
    }    
}
