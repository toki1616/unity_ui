using UnityEngine;
using Zenject;

public class UtilInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("UtilInstaller run");

        //Model
        Container.Bind<ResourcesUtils>().AsSingle();

        //Presenter
        Container.Bind<CsvUtils>().AsSingle();
    }
}
