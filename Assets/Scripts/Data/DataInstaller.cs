using Core;
using UnityEngine;
using Zenject;

namespace Data {
    public class DataInstaller :  MonoInstaller {
        [SerializeField] private DistributionSO _distribution;
        [SerializeField] private int _endOffset;
        
        public override void InstallBindings() {
            Application.targetFrameRate = 60;
            Container.BindInterfacesAndSelfTo<DataProvider>().AsSingle().WithArguments(_distribution);
            Container.BindInterfacesAndSelfTo<SymbolSequenceGenerator>().AsSingle().WithArguments(_endOffset);
        }
    }
}