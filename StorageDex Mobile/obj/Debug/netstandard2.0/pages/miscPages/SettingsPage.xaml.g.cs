//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("StorageDex Mobile.pages.miscPages.SettingsPage.xaml", "pages/miscPages/SettingsPage.xaml", typeof(global::StorageDex_Mobile.pages.miscPages.SettingsPage))]

namespace StorageDex_Mobile.pages.miscPages {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("pages\\miscPages\\SettingsPage.xaml")]
    public partial class SettingsPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.TableView settingsTable;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::StorageDex_Mobile.elements.ButtonTableCell exportDataButtonCell;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::StorageDex_Mobile.elements.ButtonTableCell importDataButtonCell;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(SettingsPage));
            settingsTable = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.TableView>(this, "settingsTable");
            exportDataButtonCell = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::StorageDex_Mobile.elements.ButtonTableCell>(this, "exportDataButtonCell");
            importDataButtonCell = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::StorageDex_Mobile.elements.ButtonTableCell>(this, "importDataButtonCell");
        }
    }
}
