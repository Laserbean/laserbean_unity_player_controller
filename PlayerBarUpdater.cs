using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarUpdater : MonoBehaviour
{

    [SerializeField] Image hpbar;
    [SerializeField] Image staminabar;
    [SerializeField] Image poisebar;

    [SerializeField] Color poiseColorRefill;
    [SerializeField] Color poiseColor;
    private void Awake()
    {
        EventManager.AddListener<PlayerStatusInitializeEvent>(OnPlayerStatusInit);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerStatusInitializeEvent>(OnPlayerStatusInit);
        statusref.Poise.OnCurrentValueMin -= PoiseMin;
        statusref.Poise.OnCurrentValueMax -= PoiseMax;
    }

    StatusCC statusref;
    void OnPlayerStatusInit(PlayerStatusInitializeEvent eevent)
    {
        statusref = eevent.statuscc;

        statusref.Poise.OnCurrentValueMin += PoiseMin;
        statusref.Poise.OnCurrentValueMax += PoiseMax;
    }

    void PoiseMin()
    {
        if (poisebar == null) return;
        poisebar.color = poiseColorRefill;
        // Debug.Log("SetColor".DebugColor(poiseColor));
    }

    void PoiseMax()
    {
        if (poisebar == null) return;
        // Debug.Log("SetColor".DebugColor(poiseColor));
        poisebar.color = poiseColor;
    }


    void Update()
    {
        if (statusref == null) return;

        hpbar.fillAmount = statusref.Health.Value / statusref.Health.MaxValue;
        staminabar.fillAmount = statusref.Stamina.Value / statusref.Stamina.MaxValue;
        poisebar.fillAmount = statusref.Poise.Value / statusref.Poise.MaxValue;

    }
}
