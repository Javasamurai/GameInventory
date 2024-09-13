using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService : MonoBehaviour
{
    public static EventService instance;

    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }
    public static EventController OnItemAdded;
    public static EventController OnItemRemoved;
    public static EventController OnItemPurchase;
    EventService()
    {
        OnItemAdded = new EventController();
        OnItemRemoved = new EventController();
        OnItemPurchase = new EventController();
    }
}
