using ToonBoomCore.Gameplay.Systems.Clear;
using ToonBoomCore.Gameplay.Systems.EntityPool;
using ToonBoomCore.Gameplay.Systems.Gravity;
using ToonBoomCore.Gameplay.Systems.Grid;
using ToonBoomCore.Gameplay.Systems.Life;
using ToonBoomCore.Gameplay.Systems.Move;
using ToonBoomCore.Gameplay.Systems.Refill;
using ToonBoomCore.Gameplay.Systems.SecondaryClear;
using ToonBoomCore.Gameplay.Systems.StartGame;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Core
{
    public class CoreSystemReferenceHandler : GameSystem
    {
        private static CoreSystemReferenceHandler _instance;

        

        public static CoreSystemReferenceHandler Instance => _instance ??= new CoreSystemReferenceHandler();
        
        private TapSystem _tapSystem = new TapSystem();
        private ClearNodeSystem _clearNodeSystem = new ClearNodeSystem();
        private SecondaryClearSystem _secondaryClearSystem = new SecondaryClearSystem();
        
        private LifeSystem _lifeSystem = new LifeSystem();

        private EntityPoolSystem _entityPoolSystem = new EntityPoolSystem();
        
        private EntityOnGridSystem _entityOnGridSystem = new EntityOnGridSystem();

        private MoveSystem _moveSystem = new MoveSystem();

        private GravitySystem _gravitySystem = new GravitySystem();
        
        private RefillSystem _refillSystem = new RefillSystem();

        private StartGameSystem _startGameSystem = new StartGameSystem();
        
        // these systems will be referenced by other systems
        public SecondaryClearSystem SecondaryClearSystem => _secondaryClearSystem;

        public ClearNodeSystem ClearNodeSystem => _clearNodeSystem;


        public LifeSystem LifeSystem => _lifeSystem;

        public EntityPoolSystem EntityPoolSystem => _entityPoolSystem;
        
        public EntityOnGridSystem EntityOnGridSystem => _entityOnGridSystem; 
        
        public MoveSystem MoveSystem => _moveSystem;

        public GravitySystem GravitySystem => _gravitySystem;
        
        public RefillSystem RefillSystem => _refillSystem;

        
        // these systems are systems that will be accessed by non systems
        // input, game flow, etc.
        
        public StartGameSystem StartGameSystem => _startGameSystem;
        
        public TapSystem TapSystem => _tapSystem;
        public override void Initialize(ILevelState levelState)
        {
            
            _tapSystem.Initialize(levelState);
            _clearNodeSystem.Initialize(levelState);
            _secondaryClearSystem.Initialize(levelState);
            _lifeSystem.Initialize(levelState);
            _entityPoolSystem.Initialize(levelState);   
            _entityOnGridSystem.Initialize(levelState);
            _moveSystem.Initialize(levelState);
            _gravitySystem.Initialize(levelState);
            _refillSystem.Initialize(levelState);
            _startGameSystem.Initialize(levelState);
        }
    }
}