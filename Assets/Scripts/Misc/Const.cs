namespace Misc
{
    public static class Const
    {
        public struct InputString
        {
            public const string MOVE = "Move";
        }
        
        public struct PlayerAnimation
        {
            public const string RUN_ANIMATION = "isMoving";
        }
        
        public struct OtherAnimation
        {
            public const string ATTACK_ANIMATION = "Attack";
        }
        
        public struct GameBalance
        {
            public const float DAMAGE_INCREASE_PERCENTAGE = 0.05f; // 5% damage increase
            public const int ARMOR_UPGRADE_COST = 50;
            public const float ARMOR_INCREASE_AMOUNT = 0.1f;
            public const int MAIN_SCENE_INDEX = 0;
        }
    }
}