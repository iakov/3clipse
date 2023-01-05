namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    public class DialogueInstigator
    {
        public Dialogue CurrentDialogue;
        
        public void TryStartDialogue(Dialogue dialogue)
        {
            if (CurrentDialogue == null || CurrentDialogue.CurrentDialogueNode == null)
            {
                CurrentDialogue = dialogue;
                CurrentDialogue.StartDialogue();
            }
            else
            {
                throw new DialogueException("Can't start dialogue when another one is running!");
            }
        }
    }
}