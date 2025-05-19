using Core;
using UnityEngine;
using Zenject;

namespace Data {
    public class DataInstaller :  MonoInstaller {
        [SerializeField] private DistributionSO _distribution;
        
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<DataProvider>().AsSingle().WithArguments(_distribution);
        }
    }
}