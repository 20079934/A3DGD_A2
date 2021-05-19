using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GenerateHeightMap : MonoBehaviour
{
    float[,] map;
    [SerializeField]
    [Range(10, 100)]
    int mapHeight, mapWidth;
    [SerializeField]
    [Range(0, 100)]
    float blockSize, blockHeight, frequency, scale;
    public GameObject minecraftBlock;

    [SerializeField]
    GameObject token, player;
    // Start is called before the first frame update
    void Start()
    {
        map = new float[mapWidth, mapHeight];
        minecraftBlock.transform.localScale = new Vector3(blockSize, blockHeight, blockSize);
        initArray();
        displayArray();
        spawnObjects();
    }

    void initArray()
    {
        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                float nx = i / mapWidth;
                float ny = i / mapHeight;
                map[i, j] = Mathf.PerlinNoise(i * 1.0f / frequency + 0.1f, j * 1.0f / frequency + 0 / 1f);
            }
        }
    }
    void displayArray()
    {
        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                GameObject t = (GameObject)(Instantiate(minecraftBlock, new Vector3(i * blockSize, Mathf.Round(map[i, j] * blockHeight * scale), j * blockSize), Quaternion.identity));
            }
        }
    }


    void spawnObjects()
    {
        int x = Random.Range(0, mapWidth);
        int y = Random.Range(0, mapHeight);
        Instantiate(player, new Vector3(x * blockSize, Mathf.Round(map[x, y] * blockHeight * scale) + 2, y * blockSize), Quaternion.identity).transform.Find("Cube").gameObject.SetActive(false); ;

        for (int i = 0; i < 4; i++) 
        {
            x = Random.Range(0, mapWidth);
            y = Random.Range(0, mapHeight);

            Instantiate(token, new Vector3(x*blockSize, Mathf.Round(map[x, y] * blockHeight * scale) + 1, y * blockSize), Quaternion.identity);
        }

    }
}