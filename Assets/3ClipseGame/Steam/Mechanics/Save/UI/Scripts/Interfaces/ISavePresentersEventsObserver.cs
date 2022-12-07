using System;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SavePresenters;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.Interfaces
{
    public interface ISavePresentersEventsObserver
    {
        event Action<BusySavePresenter> PresenterCleared;
        event Action<SavePresenter, Sprite> PresenterSelected;
    }
}