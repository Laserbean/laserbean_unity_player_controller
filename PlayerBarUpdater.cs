using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarUpdater : MonoBehaviour
{

    [SerializeField] Image hpbar;
    [SerializeField] Image staminabar;
    [SerializeField] Image poisebar;

    private void Awake() {
        EventManager.AddListener<PlayerStatusChangeEvent>(OnPlayerStatusChange);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<PlayerStatusChangeEvent>(OnPlayerStatusChange);
    }

    StatusCC statusref; 
    void OnPlayerStatusChange(PlayerStatusChangeEvent eevent) {

        statusref = eevent.statuscc; 
    }

    void Update()
    {
        if (statusref == null)return; 

        hpbar.fillAmount = statusref.Health.Value / statusref.Health.MaxValue; 
        staminabar.fillAmount = statusref.Stamina.Value / statusref.Stamina.MaxValue; 
        poisebar.fillAmount = statusref.Poise.Value / statusref.Poise.MaxValue; 
        
    }
}
