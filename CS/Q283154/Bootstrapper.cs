using Microsoft.Practices.Prism.MefExtensions;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using DevExpress.Xpf.Ribbon;
namespace Q283154 {
    public class Bootstrapper : MefBootstrapper {
        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
        }
        protected override void ConfigureModuleCatalog() {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(Module1));
        }
        protected override DependencyObject CreateShell() {
            return new Shell();
        }
        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.RootVisual = (Shell)this.Shell;
        }
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings() {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(RibbonPageCategoryBase), Container.GetExportedValue<RibbonAdapter>());
            return mappings;
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RibbonAdapter : RegionAdapterBase<RibbonPageCategoryBase> {
        [ImportingConstructor]
        public RibbonAdapter(IRegionBehaviorFactory behaviorFactory)
            : base(behaviorFactory) {
        }
        protected override IRegion CreateRegion() {
            return new Region();
        }
        protected override void Adapt(IRegion region, RibbonPageCategoryBase regionTarget) {
            new Manager(region, regionTarget);
        }
        class Manager {
            IRegion Region;
            RibbonPageCategoryBase RegionTarget;
            public Manager(IRegion region, RibbonPageCategoryBase regionTarget) {
                Region = region;
                RegionTarget = regionTarget;
                Region.Views.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Views_CollectionChanged);
            }

            void Views_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
                if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset) {
                    RegionTarget.Pages.Clear();
                    foreach(RibbonPage p in Region.Views)
                        RegionTarget.Pages.Add(p);
                }
            }
        }
    }
         
}