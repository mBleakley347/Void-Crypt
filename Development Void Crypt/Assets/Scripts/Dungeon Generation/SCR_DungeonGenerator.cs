using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generates the dungeon from the dungeonSections
/// 
/// list of possible dungeon sections
/// list of open placements
/// spawn items
/// spawn traps
/// spawn puzzles
/// spawn begining and end
/// </summary>
public class SCR_DungeonGenerator : MonoBehaviour
{
    public float size;
    bool setUp = false;

    [Header("spawnable items")]
    public List<SCR_DungeonSections> currentModules;

    public GameObject[] moduleCenter;
    public GameObject[] moduleRoom;
    public GameObject[] moduleHallway;
    public GameObject[] moduleWall;

    public GameObject[] items;

    public GameObject player;

    public GameObject Begining;
    public GameObject Ending;

    [Header("Generator Values")]
    public int complexity;
    public int difficulty;

    public Vector2 startLocation;

    public List<Vector2> filled;
    public OpenAreas[,] blockLimit;
    public List<Vector2> empty;
    public List<GameObject> itemSpots;

    public void Start()
    {
        currentModules = new List<SCR_DungeonSections>();
        filled = new List<Vector2>();
        
        startLocation.x = Random.Range(0, complexity + 5);
        startLocation.y = Random.Range(0, complexity + 5);
        empty.Add(startLocation);
    }

    // Update is called once per frame
    public void Update()
    {
        if (!setUp)
        {
            blockLimit = new OpenAreas[complexity + 5, complexity + 5];
            for (int i = 0; i < complexity + 4; i++)
            {
                for (int j = 0; j < complexity + 4; j++)
                {
                    blockLimit[i, j] = new OpenAreas();
                    blockLimit[i, j].Set(false);
                }
            }
            setUp = true;
        }
        if (empty.Count > 0)
        {
            PlanDungeon();
        }
        
        if (itemSpots.Count > 0)
        {
            SpawnItems();
        }
    }

    public void PlanDungeon()
    {
        if (filled.Count >= complexity + 5)
        {
            foreach (Vector2 item in empty)
            {
                blockLimit[(int)item.x, (int)item.y].seen = true;
            }
            empty.Clear();
            SpawnDungeon();
            return;
        }
        int section = Random.Range(0, empty.Count);
        if (blockLimit[(int)empty[section].x, (int)empty[section].y] == null) return;
        if (blockLimit[(int)empty[section].x, (int)empty[section].y].seen)
        {
            empty.RemoveAt(section);
            return;
        }
        switch (Random.Range(0, 5))
        {
            case 0:
                if ((int)empty[section].x + 1 >= blockLimit.GetLength(0)) return;
                if (blockLimit[(int)empty[section].x + 1, (int)empty[section].y].seen) return;
                blockLimit[(int)empty[section].x, (int)empty[section].y].up = true;
                blockLimit[(int)empty[section].x + 1, (int)empty[section].y].down = true;

                empty.Add(new Vector2((int)empty[section].x + 1, (int)empty[section].y));
                break;
            case 1:

                if ((int)empty[section].x - 1 < 0) return;
                if (blockLimit[(int)empty[section].x - 1, (int)empty[section].y].seen) return;
                blockLimit[(int)empty[section].x, (int)empty[section].y].down = true;
                blockLimit[(int)empty[section].x - 1, (int)empty[section].y].up = true;

                empty.Add(new Vector2((int)empty[section].x - 1, (int)empty[section].y));
                break;
            case 2:
                if ((int)empty[section].y + 1 >= blockLimit.GetLength(1)) return;
                if (blockLimit[(int)empty[section].x, (int)empty[section].y + 1].seen) return;
                blockLimit[(int)empty[section].x, (int)empty[section].y].right = true;
                blockLimit[(int)empty[section].x, (int)empty[section].y + 1].left = true;

                empty.Add(new Vector2((int)empty[section].x, (int)empty[section].y + 1));
                break;
            case 3:
                if ((int)empty[section].y - 1 < 0) return;
                if (blockLimit[(int)empty[section].x, (int)empty[section].y - 1].seen) return;
                blockLimit[(int)empty[section].x, (int)empty[section].y].left = true;
                blockLimit[(int)empty[section].x, (int)empty[section].y - 1].right = true;

                empty.Add(new Vector2((int)empty[section].x, (int)empty[section].y - 1));
                break;
            case 4:
                break;
            default:
                break;
        }
        if (Random.Range(0,100) > complexity * 10)
        {
            blockLimit[(int)empty[section].x, (int)empty[section].y].seen = true;
            filled.Add(empty[section]);
            empty.RemoveAt(section);
        }
        if (empty.Count <= 0)
        {
            SpawnDungeon();
        }
    }

    public void SpawnDungeon()
    {
        GameObject newArea;
        for (int i = 0; i < complexity + 4; i++)
        {
            for (int j = 0; j < complexity + 4; j++)
            {               
                if (!blockLimit[i, j].seen) continue;
                List<GameObject> temp = new List<GameObject>();
                temp.Clear();
                int count = 0;
                if (blockLimit[i, j].up)
                {
                    count++;
                    temp.Add(SpawnSide(true, 90,i,j));
                }
                else
                {
                    temp.Add(SpawnSide(false, 90,i,j));
                }
                if (blockLimit[i, j].down)
                {
                    count++;
                    temp.Add(SpawnSide(true, 270, i, j));
                }
                else
                {
                    temp.Add(SpawnSide(false, 270, i, j));
                }
                if (blockLimit[i, j].right)
                {
                    count++;
                    temp.Add(SpawnSide(true, 0, i, j));
                }
                else
                {
                    temp.Add(SpawnSide(false, 0, i, j));
                }
                if (blockLimit[i, j].left)
                {
                    count++;
                    temp.Add(SpawnSide(true, 180, i, j));
                }
                else
                {
                    temp.Add(SpawnSide(false, 180, i, j));
                }
                if (count > 1)
                {
                    newArea = Instantiate(moduleCenter[Random.Range(0,moduleCenter.Length)], new Vector3(i * size, 0, j * size), Quaternion.Euler(-90, 0, 0));
                    currentModules.Add(newArea.GetComponent<SCR_DungeonSections>());
                } else if (count <= 1)
                {
                    newArea = Instantiate(moduleRoom[Random.Range(0, moduleRoom.Length)], new Vector3(i * size, 0, j * size), Quaternion.Euler(-90, 0, 0));
                    currentModules.Add(newArea.GetComponent<SCR_DungeonSections>());
                    foreach (GameObject item in temp)
                    {
                            item.transform.position += -item.transform.up * 2;
                    }
                }
                temp.Clear();
            }
        }
        SpawnPlayer();
    }

    public GameObject SpawnSide(bool hallway, float rotation, int i, int j)
    {
        if (hallway)
        {
            return Instantiate(moduleHallway[Random.Range(0, moduleHallway.Length)], new Vector3(i * size, 0, j * size), Quaternion.Euler(-90, rotation, 0)); 
        }
        else
        {
            return Instantiate(moduleWall[Random.Range(0, moduleWall.Length)], new Vector3(i * size, 0, j * size), Quaternion.Euler(-90, rotation, 0));
        }
    }
    public void SpawnItems()
    {
        
    }

    public void SpawnPlayer()
    {
        Instantiate(player, new Vector3(startLocation.x * size, 1, startLocation.y * size), Quaternion.Euler(Vector3.zero));
    }
}
