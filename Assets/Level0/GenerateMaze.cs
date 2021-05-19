using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateMaze : MonoBehaviour
{

    const int N = 1, S = 2, E = 3, W = 4;
    [SerializeField]
    GameObject player, enemy, token;


    [SerializeField]
    GameObject verticalWall;
    [SerializeField]
    GameObject horizontalWall;

    [SerializeField]
    [Range(5, 100)]
    int width=5, height=5;

    [SerializeField]
    [Range(5, 100)]
    int wallSize=5;
    int[,] grid;

    GameObject[,] gridObjectsH, gridObjectsV;
    GameObject[] allObjectsInScene;

    float wallheight;

    private void Init()
    {
        height = width;
        wallheight = 4;

        verticalWall.transform.localScale = new Vector3(.1f, wallheight, wallSize);
        horizontalWall.transform.localScale = new Vector3(wallSize, wallheight, .1f);

        grid = new int[width, height];
        gridObjectsV = new GameObject[width + 1, height + 1];
        gridObjectsH = new GameObject[width + 1, height + 1];


        drawFullGrid();

        GameObject.Find("Ground").transform.localScale = new Vector3(((width+1)*wallSize)/10, 1, ((height+1)*wallSize)/10);
        player.transform.position = new Vector3(Random.Range(0, width) * wallSize - (width*wallSize)/2, 2, Random.Range(0, height) * wallSize - (width * wallSize) / 2);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemy, new Vector3(Random.Range(0, width) * wallSize - (width * wallSize) / 2, 2, Random.Range(0, height) * wallSize - (width * wallSize) / 2), Quaternion.identity);
            Instantiate(token, new Vector3(Random.Range(0, width) * wallSize - (width * wallSize) / 2, 2, Random.Range(0, height) * wallSize - (width * wallSize) / 2), Quaternion.identity);
            
        }
    }

    void drawFullGrid()
    {
        for (int i = 0; i < height+1; i++)
        {
            for (int j = 0; j < width+1; j++)
            {
                if(i<height)
                {
                    float vWallSize = verticalWall.transform.localScale.z;
                    float xOffset, zOffset;
                    xOffset = -(width*vWallSize)/2;
                    zOffset = -(height*vWallSize)/2;

                    gridObjectsV[j, i] = Instantiate(verticalWall, new Vector3(-vWallSize/2 + j*vWallSize + xOffset, wallSize/2, i *vWallSize + zOffset), Quaternion.identity);
                    gridObjectsV[j, i].SetActive(true);
                    gridObjectsV[j, i].name = "V" + i + j;

                }

                if(j<width)
                {
                    float hWallSize = horizontalWall.transform.localScale.x;
                    float xOffset, zOffset;
                    xOffset = -(width * hWallSize) / 2;
                    zOffset = -(height * hWallSize) / 2;

                    gridObjectsH[j, i] = Instantiate(horizontalWall, new Vector3(j * hWallSize+ xOffset , wallSize / 2, -hWallSize/ 2 + i * hWallSize+ zOffset), Quaternion.identity);
                    gridObjectsH[j, i].SetActive(true);
                    gridObjectsH[j, i].name = "H" + i + j;
                }
            }
        }

    }



    void generateMazeBinary()
    {
        for (int row = 0; row< height; row++) 
        {
            for (int col = 0; col< width; col++) 
            {
                float randomNumber = Random.Range(0, 100);

                int carvingDirection;
                if (randomNumber > 30) carvingDirection = N; else carvingDirection = E;
                if(col == width-1)
                {
                    if (row < height - 1)
                    {
                        carvingDirection = N;
                    }
                    else carvingDirection = W;
                }
                else if(row == height -1)
                {
                    if (col < width - 1) carvingDirection = E; else carvingDirection = -1;
                }

                grid[col, row] = carvingDirection;


            }
        }
    }

    void displayGrid()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {

                if (grid[col, row] == N) gridObjectsH[col, row + 1].SetActive(false);
                if (grid[col, row] == E) gridObjectsV[col+1, row].SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();

        generateMazeBinary();
        displayGrid();

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
