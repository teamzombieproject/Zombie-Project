using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyDrop : MonoBehaviour
{
    public GameManager GM;

    private void Start()
    {
        //Connor Fettes added code to work with an arrow that points at SupplyDrop when it's off screen here
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OffScreenPoint>().pointTransform = gameObject.transform;
        //code done
        if (GM == null)
        {
             GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RandomizedLoot();

                for (int i = GM.mineStock; i > 0; i--)
                {
                    Instantiate(GM.mineDrop, GM.currentSupplyDropSpawn.transform.position, Quaternion.identity);
                }
                for (int i = GM.bearTrapStock; i > 0; i--)
                {
                    Instantiate(GM.bearTrapDrop, GM.currentSupplyDropSpawn.transform.position, Quaternion.identity);
                }
                for (int i = GM.barricadeStock; i > 0; i--)
                {
                    Instantiate(GM.barricadeDrop, GM.currentSupplyDropSpawn.transform.position, Quaternion.identity);
                }
                if (GM.wave >= 2)
                {
                    for (int i = GM.machineGunTurretStock; i > 0; i--)
                    {
                        Instantiate(GM.machineGunTurretDrop, GM.currentSupplyDropSpawn.transform.position, Quaternion.identity);
                    }
                    for (int i = GM.javelinRocketTurretStock; i > 0; i--)
                    {
                        Instantiate(GM.javelinRocketTurretDrop, GM.currentSupplyDropSpawn.transform.position, Quaternion.identity);
                    }
                }
            Destroy(gameObject);
        }
    }

    public void RandomizedLoot()
    {
        int JRTrandom = RandomValue(
            new RandomVaribles(0, 1, .95f),
            new RandomVaribles(1, 2, .05f));
        int MGTrandom =RandomValue(
            new RandomVaribles(0, 1, .95f),
            new RandomVaribles(1, 2, .05f));
        int Mrandom = RandomValue(
            new RandomVaribles(0, 0, .05f),
            new RandomVaribles(1, 1, .85f),
            new RandomVaribles(1, 2, .1f));
        int BTrandom = RandomValue(
            new RandomVaribles(0, 0, .05f),
            new RandomVaribles(1, 1, .85f),
            new RandomVaribles(1, 2, .1f));
        int Brandom = RandomValue(
            new RandomVaribles(0, 1, .3f),
            new RandomVaribles(2, 2, .7f));

        GM.javelinRocketTurretStock = JRTrandom;
        GM.machineGunTurretStock = MGTrandom;
        GM.mineStock = Mrandom;
        GM.bearTrapStock = BTrandom;
        GM.barricadeStock = Brandom;
    }

    struct RandomVaribles
    {
        private int minValue;
        private int maxValue;
        public float probability;

        public RandomVaribles(int minValue, int maxValue, float probability)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.probability = probability;
        }

        public int GetValue() { return Random.Range(minValue, maxValue + 1); }
    }

    int RandomValue(params RandomVaribles[] varibles)
    {
        float randomness = Random.value;
        float currentProbability = 0;
        foreach (var varible in varibles)
        {
            currentProbability += varible.probability;
            if (randomness <= currentProbability)
                return varible.GetValue();
        }

        return 1;
    }
}
