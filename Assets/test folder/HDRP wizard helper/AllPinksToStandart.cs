using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AllPinksToStandart : MonoBehaviour
{
    [SerializeField] GameObject _GameObject;
    [SerializeField] Shader hidden;
    [SerializeField] Shader standard;

    [SerializeField] List<Material> AllMaterials = new List<Material>();
    [SerializeField] List<Material> PinkMaterials = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        hidden = _GameObject.GetComponent<Renderer>().material.shader;
        AllMaterials.Clear();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(Material).ToString().Replace("UnityEngine.", "")));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            Material asset = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
            if (asset != null)
            {
                AllMaterials.Add(asset);
            }
        }
        foreach (Material mat in AllMaterials)
        {
            if (mat.shader == hidden)
            {
                PinkMaterials.Add(mat);
                mat.shader = standard;
            }
        }
        Debug.Log("its done");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
