Imports Microsoft.VisualBasic
Imports Microsoft.Practices.Prism.MefExtensions
Imports System.ComponentModel.Composition.Hosting
Imports System.Windows
Imports Microsoft.Practices.Prism.Modularity
Imports System.ComponentModel.Composition
Imports Microsoft.Practices.Prism.Regions
Imports DevExpress.Xpf.Ribbon
Namespace Q283154
	Public Class Bootstrapper
		Inherits MefBootstrapper
		Protected Overrides Sub ConfigureAggregateCatalog()
			MyBase.ConfigureAggregateCatalog()
			Me.AggregateCatalog.Catalogs.Add(New AssemblyCatalog(GetType(Bootstrapper).Assembly))
		End Sub
		Protected Overrides Sub ConfigureModuleCatalog()
			MyBase.ConfigureModuleCatalog()
			Dim moduleCatalog As ModuleCatalog = CType(Me.ModuleCatalog, ModuleCatalog)
			moduleCatalog.AddModule(GetType(Module1))
		End Sub
		Protected Overrides Function CreateShell() As DependencyObject
			Return New Shell()
		End Function
		Protected Overrides Sub InitializeShell()
			MyBase.InitializeShell()
			Application.Current.RootVisual = CType(Me.Shell, Shell)
		End Sub
		Protected Overrides Function ConfigureRegionAdapterMappings() As RegionAdapterMappings
			Dim mappings As RegionAdapterMappings = MyBase.ConfigureRegionAdapterMappings()
			mappings.RegisterMapping(GetType(RibbonPageCategoryBase), Container.GetExportedValue(Of RibbonAdapter)())
			Return mappings
		End Function
	End Class

	<Export, PartCreationPolicy(CreationPolicy.NonShared)> _
	Public Class RibbonAdapter
		Inherits RegionAdapterBase(Of RibbonPageCategoryBase)
		<ImportingConstructor> _
		Public Sub New(ByVal behaviorFactory As IRegionBehaviorFactory)
			MyBase.New(behaviorFactory)
		End Sub
		Protected Overrides Function CreateRegion() As IRegion
			Return New Region()
		End Function
		Protected Overrides Sub Adapt(ByVal region As IRegion, ByVal regionTarget As RibbonPageCategoryBase)
			Dim TempManager As Manager = New Manager(region, regionTarget)
		End Sub
		Private Class Manager
			Private Region As IRegion
			Private RegionTarget As RibbonPageCategoryBase
			Public Sub New(ByVal region As IRegion, ByVal regionTarget As RibbonPageCategoryBase)
				Me.Region = region
				Me.RegionTarget = regionTarget
				AddHandler Me.Region.Views.CollectionChanged, AddressOf Views_CollectionChanged
			End Sub

			Private Sub Views_CollectionChanged(ByVal sender As Object, ByVal e As System.Collections.Specialized.NotifyCollectionChangedEventArgs)
				If e.Action = System.Collections.Specialized.NotifyCollectionChangedAction.Reset Then
					RegionTarget.Pages.Clear()
					For Each p As RibbonPage In Region.Views
						RegionTarget.Pages.Add(p)
					Next p
				End If
			End Sub
		End Class
	End Class

End Namespace