using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatusDisplay : MonoBehaviour
{
    StatusCC statusController; 
    // Start is called before the first frame update
    void Start()
    {
        statusController = GetComponent<StatusCC>(); 
        EventManager.TriggerEvent(new PlayerStatusChangeEvent(statusController)); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class PlayerStatusChangeEvent {
    // public StatusValue Health; 
    // public StatusValue Stamina; 
    // public StatusValue Poise; 

    public StatusCC statuscc;

    public PlayerStatusChangeEvent(StatusCC _statuscc) {
        // Health = _statuscc.Health; 
        // Stamina = _statuscc.Stamina; 
        // Poise = _statuscc.Poise; 

        statuscc = _statuscc; 
    }
}
