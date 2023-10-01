using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] PlayerStateMachineComponent player;
    [SerializeField] GameObject Blocky;

    private void Start()
    {
        if (player.WeaponEnabled)
        {
            Blocky.SetActive(false);
        } else Blocky.SetActive(true);

        player.OnWeaponEnable.AddListener(() =>
        {
            Blocky.SetActive(false);
        });

    }



}
