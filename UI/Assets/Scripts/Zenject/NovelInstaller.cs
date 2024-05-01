using UnityEngine;
using Zenject;

public class NovelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("NovelInstaller run");

        //Model
        Container.Bind<NovelModel>().AsSingle();

        //Presenter
        Container.Bind<NovelPresenter>().AsSingle();
    }
}
