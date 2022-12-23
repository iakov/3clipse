using System;
using _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.SavePresenters;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.Interfaces
{
    public interface ISavePresentersEventsObserver
    {
        event Action<BusySavePresenter> PresenterCleared;
        event Action<SavePresenter, Sprite> PresenterSelected;
    }
}