//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("StorageDex Mobile.pages.miscPages.SearchPage.xaml", "pages/miscPages/SearchPage.xaml", typeof(global::StorageDex_Mobile.SearchPage))]

namespace StorageDex_Mobile {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("pages\\miscPages\\SearchPage.xaml")]
    public partial class SearchPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::StorageDex_Mobile.elements.BorderlessEntry searchTextInput;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.StackLayout resultsStack;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(SearchPage));
            searchTextInput = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::StorageDex_Mobile.elements.BorderlessEntry>(this, "searchTextInput");
            resultsStack = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.StackLayout>(this, "resultsStack");
        }
    }
}
