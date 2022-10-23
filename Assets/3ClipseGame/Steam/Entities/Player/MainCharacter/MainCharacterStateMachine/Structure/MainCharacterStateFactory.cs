using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Unconsciouned;
using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure
{
    public class MainCharacterStateFactory : StateFactory<MainCharacterStateMachine>
    {
        public MainCharacterStateFactory(MainCharacterStateMachine context) : base(context){}

        public MainCharacterState ExploreState() => new ExploreMainCharacterState(Context, this);
        public MainCharacterState AnimalControlState() => new AnimalControlState(Context, this);
    }
}
