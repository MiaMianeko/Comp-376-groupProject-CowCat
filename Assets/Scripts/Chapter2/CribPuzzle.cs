using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CribPuzzle : MonoBehaviour
{
    [SerializeField] Crib crib1;
    [SerializeField] Crib crib2;
    [SerializeField] Crib crib3;
    [SerializeField] Crib crib4;
    [SerializeField] Crib crib5;
    [SerializeField] Crib crib6;
    [SerializeField] Crib crib7;

    HospitalManager manager;

    public bool solved;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<HospitalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (crib1.correctDoll && crib2.correctDoll && crib3.correctDoll && crib4.correctDoll && crib5.correctDoll && crib6.correctDoll && crib7.correctDoll)
        {
            solved = true;
            manager.dollPuzzleSolved = true;
        }
    }
}
