using UnityEngine;
using Zenject;

public class ChatInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("ChatInstaller run");

        //Model
        Container.Bind<ChatModel>().AsSingle();

        //Presenter
        Container.Bind<ChatPresenter>().AsSingle();
    }
}
