using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    #region
    public static GameHandler instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    int level;
    public TextMeshProUGUI sunAmountText;
    public TextMeshProUGUI levelText;
    [HideInInspector]public int sunAmount = 0;
    public GameObject sun;

    public GameObject[] cards;
    public Plant[] pickedSeeds;

    [HideInInspector] public Transform placeSpot;
    [HideInInspector] public List<GameObject> zombiePos;
    [HideInInspector] public List<GameObject> plantPos;
    [HideInInspector] public bool placing = false;
    [HideInInspector] public GameObject canvas;

    public GameObject seedSelectorGO;
    public Image progressImage;
    public GameObject deadUI;
    [HideInInspector]public bool dead = false;
    [HideInInspector] public bool won = false;
    public int maxSeeds = 6;

    public GameObject nextLevelPlant;

    int nonSeedLevels = 0;
    [HideInInspector] public bool started = true;

    SelectCard[] selectCards;
    SelectCard[] pickedCards;

    public bool conveyorBelt = false;

    public void Start()
    {
        canvas = GameObject.Find("Canvas");

        Time.timeScale = 1;
        level = SceneManager.GetActiveScene().buildIndex;
        nonSeedLevels = level / 5;
        levelText.text = "Level " + (((level - 1) / 10) + 1 )+ "-" +  level % 10;
        // if(level - nonSeedLevels > maxSeeds && !conveyorBelt)
        // {
        //     pickedSeeds = new Plant[maxSeeds];
        //     if(seedSelectorGO != null)
        //     {
        //         seedSelectorGO.SetActive(true);
        //     }
        //     started = false;

        //     selectCards = GameObject.Find("Seed Selector/Options").GetComponentsInChildren<SelectCard>();
        //     pickedCards = GameObject.Find("Seed Selector/Selected Cards").GetComponentsInChildren<SelectCard>();

        //     for (int i = 0; i < selectCards.Length; i++)
        //     {
        //         selectCards[i].index = i;
        //         if (i < level - nonSeedLevels)
        //         {
        //             selectCards[i].gameObject.SetActive(true);
        //             selectCards[i].transform.Find("Plant Sprite").GetComponent<SpriteRenderer>().sprite = AllPlants.instance.allPlants[i].sprite;
        //             selectCards[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = AllPlants.instance.allPlants[i].cost.ToString();
        //         }
        //         else
        //         {
        //             selectCards[i].gameObject.SetActive(false);
        //         }
        //     }

        //     for(int i = 0; i < pickedCards.Length; i++)
        //     {
        //         pickedCards[i].gameObject.SetActive(false);
        //     }
        // }

        if(started)
        {
            //adds seeds to picked seeds
            pickedSeeds = new Plant[level - nonSeedLevels];
            for (int i = 0; i < level - nonSeedLevels; i++)
            {
                pickedSeeds[i] = AllPlants.instance.allPlants[i];
            }

            //TODO REMOVE THIS LATER! HERE FOR JUST DEBUGGING
            // pickedSeeds = new Plant[AllPlants.instance.allPlants.Count];
            // for (int i = 0; i < AllPlants.instance.allPlants.Count; i++)
            // {
            //     pickedSeeds[i] = AllPlants.instance.allPlants[i];
            // }

            StartSettup();
        }
    }

    public void SelectedCard(int i)
    {
        bool newCard = true;
        for (int j = 0; j < pickedSeeds.Length; j++)
        {
            if(pickedSeeds[j] == AllPlants.instance.allPlants[i])
            {
                newCard = false;
            }
        }

        if(newCard)
        {
            for (int j = 0; j < pickedSeeds.Length; j++)
            {
                if (pickedSeeds[j] == null)
                {
                    pickedSeeds[j] = AllPlants.instance.allPlants[i];
                    pickedCards[j].gameObject.SetActive(true);
                    pickedCards[j].transform.Find("Plant Sprite").GetComponent<SpriteRenderer>().sprite = AllPlants.instance.allPlants[i].sprite;
                    pickedCards[j].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = AllPlants.instance.allPlants[i].cost.ToString();
                    pickedCards[j].index = i;
                    return;
                }
            }
        }
    }
    public void DeselectedCard(int i)
    {
        for (int j = 0; j < pickedSeeds.Length; j++)
        {
            if (pickedSeeds[j] == AllPlants.instance.allPlants[i])
            {
                pickedSeeds[j] = null;
                pickedCards[j].gameObject.SetActive(false);
                return;
            }
        }
    }
    public void StartSettup()
    {
        started = true;
        //seeds
        for (int i = 0; i < cards.Length; i++)
        {
            if (i < pickedSeeds.Length)
            {
                cards[i].SetActive(true);
                cards[i].transform.Find("Plant Sprite").GetComponent<Image>().sprite = pickedSeeds[i].sprite;
                cards[i].GetComponentInChildren<TextMeshProUGUI>().text = pickedSeeds[i].cost.ToString();
                cards[i].GetComponent<Card>().plant = pickedSeeds[i];
            }
            else
            {
                cards[i].SetActive(false);
            }
        }
        if(seedSelectorGO != null)
        {
            seedSelectorGO.SetActive(false);
        }
    }

    public void FinishSeedSelect()
    {
        bool ready = true;
        foreach(Plant p in pickedSeeds)
        {
            if(p == null)
            {
                ready = false;
            }
        }

        if(ready)
        {
            StartSettup();
        }
    }
    public void AddSun(int i)
    {
        sunAmount += i;
        sunAmountText.text = sunAmount.ToString();
    }
    public void RemoveSun(int i)
    {
        sunAmount -= i;
        sunAmountText.text = sunAmount.ToString();
    }

    public void Update()
    {
        if(started)
        {
            CheckForDeadZombies();
            CheckForDeadPlants();
        }
    }

    public void placePlant(Vector3 pos , Plant plant)
    {
        RemoveSun(plant.cost);
        GameObject g = Instantiate(plant.plantGameObject, pos, Quaternion.identity);
        g.transform.parent = placeSpot;
        plantPos.Add(g);
    }
    public void CheckForDeadZombies()
    {
        for(int i = 0;i < zombiePos.Count; i++)
        {
            if(zombiePos[i] ==  null)
            {
                zombiePos.RemoveAt(i);
                i--;
            }
        }
    }

    public void CheckForDeadPlants()
    {
        for (int i = 0; i < plantPos.Count; i++)
        {
            if (plantPos[i] == null)
            {
                plantPos.RemoveAt(i);
                i--;
            }
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void WinLevel()
    {
        if(Level.instance.otherReward == null)
        {
            GameObject go = Instantiate(nextLevelPlant, canvas.transform);

            go.transform.Find("Plant Sprite").GetComponent<Image>().sprite = AllPlants.instance.allPlants[level - nonSeedLevels].sprite;
            go.GetComponentInChildren<TextMeshProUGUI>().text = AllPlants.instance.allPlants[level - nonSeedLevels].cost.ToString();

            Vector3 pos = go.transform.position;
            pos.z = -1;
            go.transform.position = pos;
        }
        else
        {
            GameObject go = Instantiate(Level.instance.otherReward, canvas.transform);
            Vector3 pos = go.transform.position;
            pos.z = -1;
            go.transform.position = pos;
        }
        
        won = true;
    }

    public bool OpenSpot()
    {
        if (placeSpot == null)
            return false;

        return placeSpot.GetComponentsInChildren<Transform>().Length <= 1;
    }

    public void RemovePlant()
    {
        if(placeSpot != null)
        {
            foreach(GameObject g in plantPos)
            {
                if((Vector3)g.transform.position == (Vector3)placeSpot.position)
                {
                    Destroy(g);
                }
            }
        }
    }
}
