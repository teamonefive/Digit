using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Broadcaster : MonoBehaviour
{
    public void BroadcastButtonClick() {
        Messenger.Broadcast(GameEvent.BUTTON_CLICKED);
    }
}
