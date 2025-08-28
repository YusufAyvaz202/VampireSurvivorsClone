namespace Misc
{
    public enum PlayerState
    {
        Idle,
        Running,
    }
    
    public enum EnemyType
    {
        FlyingEye,
        Goblin,
    }
    
    public enum GameState
    {
        WaitingToStart,
        Playing,
        Paused,
        GameOver,
    }
    
    public enum PrizeType
    {
        Gun,
        Health,
        Speed,
    }

    public enum GunType
    {
        Axe,
        MagicGun,
        SwordGun
    }
}