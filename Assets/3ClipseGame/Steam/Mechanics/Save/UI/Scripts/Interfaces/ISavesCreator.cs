using System.Collections.Generic;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Data;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SavePresenters;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.Interfaces
{
    public interface ISavesCreator
    {
        List<BusySavePresenter> CreateBusyPresenters(List<GameSave> saves);
        BusySavePresenter CreateBusyPresenter(GameSave save, int id);
        List<EmptySavePresenter> CreateEmptyPresenters(int busyPresentersAmount, int presentersAmount);
        EmptySavePresenter CreateEmptyPresenter(int id);
    }
}