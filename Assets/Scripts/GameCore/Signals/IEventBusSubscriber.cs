namespace GameCore
{
    public interface IEventBusSubscriber
    {
        
    }
    
    public interface IPlayerDied : IEventBusSubscriber
    {
        void OnPlayerDied();
    }
}