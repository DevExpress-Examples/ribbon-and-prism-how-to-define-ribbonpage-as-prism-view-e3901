Imports Microsoft.VisualBasic
Imports Microsoft.Practices.Prism.MefExtensions.Modularity
Imports System.ComponentModel.Composition
Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions
Namespace Q283154
	<ModuleExport(GetType(Module1))> _
	Public Class Module1
		Implements IModule
        <Import()> _
        Public RegionManager As IRegionManager

        Public Sub Initialize() Implements IModule.Initialize
            RegionManager.RegisterViewWithRegion("View1Region", GetType(View1))
        End Sub
    End Class
End Namespace