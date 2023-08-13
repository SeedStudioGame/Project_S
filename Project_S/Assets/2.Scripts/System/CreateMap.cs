using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] private int Height = 10;
    [SerializeField] private int Width = 10;

    [Header("Perlin Noise")]
    [SerializeField] private float scale = 1f;
    [SerializeField] private float threshold = 0.5f;


    void Start()
    {
        // 타일 생성
        float halfSqrt3 = Mathf.Sqrt(3) * 0.5f;
        float checkOddLayer;

        for(int i = 0; i < Height; i++)
        {
            checkOddLayer = (i % 2) == 0 ? 0 : 0.5f;
            for(int j = 0; j < Width; j++)
            {
                GameObject tile = Instantiate(_tilePrefab, new Vector3(j - checkOddLayer, 0, i * halfSqrt3), Quaternion.identity);
                tile.transform.parent = this.transform;

                randomTile(tile);
            }
        }

    }

    void randomTile(GameObject tile)
    {
        float x = tile.transform.position.x / scale;
        float z = tile.transform.position.z / scale;

        float noise = Mathf.PerlinNoise(x, z);

        if(noise > threshold)
        {
            Destroy(tile);
        }
    }
}
