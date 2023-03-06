using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEnergyPuzzle : MonoBehaviour, InteractableHubObjectInterface
{
    public void LoadEnigm()
    {
        SceneManager.LoadScene("EnergyPuzzle");
    }
}
