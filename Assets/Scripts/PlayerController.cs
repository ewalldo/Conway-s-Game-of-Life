using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cubePrefab;

    private Vector3 spawnPos = new Vector3(0, 0, 0);
    private int gridSize;

    private GameObject[,] cells;
    private int[,] cellsValue;
    private int[,] nextCellsValue;

    private bool runGameOfLife;

    // Start is called before the first frame update
    void Start()
    {
        gridSize = 32;

        cells = new GameObject[gridSize, gridSize];
        cellsValue = new int[gridSize, gridSize];
        nextCellsValue = new int[gridSize, gridSize];

        runGameOfLife = false;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                cells[i, j] = Instantiate(cubePrefab, spawnPos + new Vector3(i * 1.05f, j * 1.05f, 0), cubePrefab.transform.rotation);
            }
        }

        InvokeRepeating("UpdateGameOfLife", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Pause the game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (runGameOfLife)
                runGameOfLife = false;
            else
                runGameOfLife = true;
        }

        //Update cells value based on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            UpdateCellsValue();
        }

        //Finish simulation
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //Create the next generation of game of life
    void UpdateGameOfLife()
    {
        UpdateCellsValue();

        if (runGameOfLife)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    //Check number of neighbors and create next generation
                    int nAlive = numberNeighborAlive(i, j);

                    if (cellsValue[i, j] == 0 && nAlive == 3)
                        nextCellsValue[i, j] = 1;
                    else if (cellsValue[i, j] == 0 && nAlive != 3)
                        nextCellsValue[i, j] = 0;
                    else if (cellsValue[i, j] == 1 && (nAlive < 2 || nAlive > 3))
                        nextCellsValue[i, j] = 0;
                    else if (cellsValue[i, j] == 1 && (nAlive == 2 || nAlive == 3))
                        nextCellsValue[i, j] = 1;
                }
            }

            //Update cells color
            cellsValue = nextCellsValue.Clone() as int[,];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    cells[i, j].GetComponent<CubeBehaviour>().ColorValue = cellsValue[i, j];
                }
            }
        }
    }


    //Check number of neighbors alive
    //Params: i: row index of cell, j: col index of cell
    private int numberNeighborAlive(int i, int j)
    {
        int nAlive = 0;

        //Check top left corner
        if (i == 0 && j == 0)
        {
            nAlive += cellsValue[i + 1, j];
            nAlive += cellsValue[i, j + 1];
            nAlive += cellsValue[i + 1, j + 1];
        }
        //Check bottom right corner
        else if (i == gridSize - 1 && j == gridSize - 1)
        {
            nAlive += cellsValue[i - 1, j];
            nAlive += cellsValue[i, j - 1];
            nAlive += cellsValue[i - 1, j - 1];
        }
        //Check top right corner
        else if (i == 0 && j == gridSize - 1)
        {
            nAlive += cellsValue[i, j - 1];
            nAlive += cellsValue[i + 1, j - 1];
            nAlive += cellsValue[i + 1, j];
        }
        //Check bottom left corner
        else if (i == gridSize - 1 && j == 0)
        {
            nAlive += cellsValue[i, j + 1];
            nAlive += cellsValue[i - 1, j + 1];
            nAlive += cellsValue[i - 1, j];
        }
        //Check top row
        else if (i == 0)
        {
            nAlive += cellsValue[i, j - 1];
            nAlive += cellsValue[i, j + 1];
            for (int k = -1; k < 2; k++)
            {
                nAlive += cellsValue[i + 1, j + k];
            }
        }
        //Check leftmost column
        else if (j == 0)
        {
            nAlive += cellsValue[i - 1, j];
            nAlive += cellsValue[i + 1, j];
            for (int k = -1; k < 2; k++)
            {
                nAlive += cellsValue[i + k, j + 1];
            }
        }
        //Check bottom row
        else if (i == gridSize - 1)
        {
            nAlive += cellsValue[i, j - 1];
            nAlive += cellsValue[i, j + 1];
            for (int k = -1; k < 2; k++)
            {
                nAlive += cellsValue[i - 1, j + k];
            }
        }
        //Check rightmost column
        else if (j == gridSize - 1)
        {
            nAlive += cellsValue[i - 1, j];
            nAlive += cellsValue[i + 1, j];
            for (int k = -1; k < 2; k++)
            {
                nAlive += cellsValue[i + k, j - 1];
            }
        }
        //Check all the other positions
        else
        {
            for (int k = -1; k < 2; k++)
            {
                nAlive += cellsValue[i - k, j - 1];
                nAlive += cellsValue[i - k, j + 1];
            }
            nAlive += cellsValue[i - 1, j];
            nAlive += cellsValue[i + 1, j];
        }

        return nAlive;
    }

    //Update cells array
    private void UpdateCellsValue()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                cellsValue[i, j] = cells[i, j].GetComponent<CubeBehaviour>().ColorValue;
            }
        }
    }
}
