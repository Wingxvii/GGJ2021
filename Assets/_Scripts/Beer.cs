using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    public int amount = 1;
    public bool empty = false;

    public bool Chug() {
        if (!empty) {
            amount -= 1;
            if (amount == 0) { 
                empty = true;
                //TEMP EMPTY CODE
                this.gameObject.SetActive(false);
            }
            return true;
        }

        return false;
    }
}
