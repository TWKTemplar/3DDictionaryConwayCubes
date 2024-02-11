using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryConwayCubesPlayModeControler : MonoBehaviour
{
    public DictionaryConwayCubes dictionaryConwayCubes;
    public bool RunTimer = false;
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
        if (RunTimer == false) return;
        tempTimer -= Time.deltaTime;
        if(tempTimer <= 0)
        {
            tempTimer = MaxTimer;
            dictionaryConwayCubes.Step();
        }
    }
}
