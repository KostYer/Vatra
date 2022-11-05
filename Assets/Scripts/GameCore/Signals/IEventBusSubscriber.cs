namespace GameCore
{
    public interface IEventBusSubscriber
    {
        
    }
    
    public interface ILevelStart : IEventBusSubscriber
    {
        void OnLevelStart();
    }
    
    public interface ILevelEnd : IEventBusSubscriber
    {
        void OnLevelEnd();
    }
    
    public interface IPlayerDied : IEventBusSubscriber
    {
        void OnPlayerDied();
    }
}