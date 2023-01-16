namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic
{
    public class StelaPresenter : InteractablePresenter
    {
        public override void Activate() => CurrentInteractable.Activate();

        public void SetLocation(string locationName){}
    }
}