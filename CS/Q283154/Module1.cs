using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
namespace Q283154 {
    [ModuleExport(typeof(Module1))]
    public class Module1 : IModule {
        [Import]
        public IRegionManager RegionManager;
        public void Initialize() {
            RegionManager.RegisterViewWithRegion("View1Region", typeof(View1));
        }
    }
}