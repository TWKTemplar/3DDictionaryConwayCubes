using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryConwayCubesPlayModeControler : MonoBehaviour
{
    public DictionaryConwayCubes dictionaryConwayCubes;
    public float MaxTimer = 3;
    public float tempTimer = 0;
    private void OnValidate()
    {
        if (dictionaryConwayCubes == false) dictionaryConwayCubes = FindObjectOfType<DictionaryConwayCubes>();
    }
    void Start()
    {
        tempTimer = MaxTimer;
        dictionaryConwayCubes.ClearDictionary();
        dictionaryConwayCubes.SpawnRandom();
    }

    void Update()
    {
        tempTimer -= Time.deltaTime;
        if(MaxTimer <= 0)
        {
            tempTimer = MaxTimer;
            dictionaryConwayCubes.Step();
        }
    }
}
