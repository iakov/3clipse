namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    public class DialogueInstigator
    {
        public Dialogue CurrentDialogue;
        
        private void TryStartDialogue(Dialogue dialogue)
        {
            if (CurrentDialogue == null || CurrentDialogue.CurrentDialogueNode == null)
            {
                CurrentDialogue = dialogue;
                CurrentDialogue.StartDialogue();
            }
        }
    }
}