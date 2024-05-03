using UnityEngine;
using Zenject;

public class UtilInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("UtilInstaller run");

        Container.Bind<ResourcesUtils>().AsSingle();
        Container.Bind<CsvUtils>().AsSingle();
        Container.Bind<StringSplitUtils>().AsSingle();
    }
}
