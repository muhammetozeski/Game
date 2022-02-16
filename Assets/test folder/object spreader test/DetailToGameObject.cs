using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DetailToGameObject : MonoBehaviour
{
    public Terrain terrain;

    
    [HideInInspector]
    public string[] GameObjects;

    public GameObject ChoosenGameObject;
    public int ChoosenGameObjectIndex;

    public void assignGameObject(int index)
    {
        ChoosenGameObject = terrain.terrainData.detailPrototypes[index].prototype;
    }
    public void detectGameObjects()
    {
        int lenght = terrain.terrainData.detailPrototypes.Length;
        GameObjects = new string[lenght];
        for (int i = 0; i < lenght; i++)
        {
            GameObjects[i] = terrain.terrainData.detailPrototypes[i].prototype.name;
        }
    }

    public void ConvertDetailsToGameObjects()
    {
        DetailedObjectInstance[] Details;
        Details = DetailedObjectInstance.ExportObjects(terrain, ChoosenGameObjectIndex);
        GameObject Parent = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        Parent.name = "Parent of details:" + GameObjects[ChoosenGameObjectIndex];
        for (int i = 0; i < Details.Length; i++)
        {
            GameObject objectedDetail = PrefabUtility.InstantiatePrefab(Details[i].Prefab) as GameObject;
            objectedDetail.transform.parent = Parent.transform;
            objectedDetail.transform.position = Details[i].Position;
            objectedDetail.transform.eulerAngles = Details[i].Rotation;
            objectedDetail.transform.localScale = Details[i].Scale;
        }

        //this is weir but necessary:
        Parent.transform.position += new Vector3(1.89f, 67.5f, 1.26f);
    }

    public void ConvertAllDetailsToGameObjects()
    {
        for (int i = 0; i < GameObjects.Length; i++)
        {
            ChoosenGameObjectIndex = i;
            ConvertDetailsToGameObjects();
        }
    }
}

public class DetailedObjectInstance
{
    public GameObject Prefab;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public static DetailedObjectInstance[] ExportObjects(Terrain terrain, int wantedDetailIndex)
    {

        List<DetailedObjectInstance> output = new List<DetailedObjectInstance>();

        TerrainData data = terrain.terrainData;
        if (terrain.detailObjectDensity != 0)
        {

            int detailWidth = data.detailWidth;
            int detailHeight = data.detailHeight;


            float delatilWToTerrainW = data.size.x / detailWidth;
            float delatilHToTerrainW = data.size.z / detailHeight;

            Vector3 mapPosition = terrain.transform.position;

            bool doDentisy = false;
            float targetDentisty = 0;
            if (terrain.detailObjectDensity != 1)
            {
                targetDentisty = (1 / (1f - terrain.detailObjectDensity));
                doDentisy = true;
            }


            float currentDentity = 0;

            DetailPrototype[] details = data.detailPrototypes;
            int i = wantedDetailIndex;
                GameObject Prefab = details[i].prototype;

                float minWidth = details[i].minWidth;
                float maxWidth = details[i].maxWidth;

                float minHeight = details[i].minHeight;
                float maxHeight = details[i].maxHeight;
            
                int[,] map = data.GetDetailLayer(0, 0, data.detailWidth, data.detailHeight, i);

                List<Vector3> grasses = new List<Vector3>();
                for (var y = 0; y < data.detailHeight; y++)
                {
                    for (var x = 0; x < data.detailWidth; x++)
                    {
                        if (map[x, y] > 0)
                        {
                            currentDentity += 1f;


                            bool pass = false;
                            if (!doDentisy)
                                pass = true;
                            else
                                pass = currentDentity < targetDentisty;

                            if (pass)
                            {
                                float _z = (x * delatilWToTerrainW) + mapPosition.z;
                                float _x = (y * delatilHToTerrainW) + mapPosition.x;
                                float _y = terrain.SampleHeight(new Vector3(_x, 0, _z));
                                grasses.Add(new Vector3(
                                    _x,
                                    _y,
                                    _z
                                    ));
                            }
                            else
                            {
                                currentDentity -= targetDentisty;
                            }

                        }
                    }
                }

                foreach (var item in grasses)
                {
                    DetailedObjectInstance e = new DetailedObjectInstance();
                    e.Prefab = Prefab;

                    e.Position = item;
                    e.Rotation = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
                    e.Scale = new Vector3(UnityEngine.Random.Range(minWidth, maxWidth), UnityEngine.Random.Range(minHeight, maxHeight), UnityEngine.Random.Range(minWidth, maxWidth));

                    output.Add(e);
                }
            
        }


        return output.ToArray();
    }
}
