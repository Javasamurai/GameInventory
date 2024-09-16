public class EventService
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
    public EventController<Item> OnItemPurchased;
    public EventController<Item> OnItemSold;
    
    EventService()
    {
        OnItemPurchased = new EventController<Item>();
        OnItemSold = new EventController<Item>();
    }
}
