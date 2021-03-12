using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.viewPages;
using StorageDexLib;
using StorageDexLib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {

        List<ISearchable> searchList;

        //amount of result on a page at once - roughly
        private int SEARCH_RESULTS_ON_PAGE = 10;


        public SearchPage()
        {
            this.searchList = new List<ISearchable>();

            InitializeComponent();

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;


        }

        //makes a search page out of a list of locations - searches the list
        public SearchPage(List<StorageLocation> searchList)
        {
            this.searchList = new List<ISearchable>();

            foreach (StorageLocation location in searchList)
            {
                this.searchList.Add(location);
                foreach (StorageContainer container in location.GetContainers())
                {
                    this.searchList.Add(container);
                    foreach (Item item in container.GetItems())
                    {
                        this.searchList.Add(item);
                    }
                }

               
            }


            InitializeComponent();

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }

        //makes a search page out of a list of containers - searches the list
        public SearchPage(List<StorageContainer> searchList)
        {
            this.searchList = new List<ISearchable>();

            foreach (StorageContainer container in searchList)
            {
                this.searchList.Add(container);
                foreach (Item item in container.GetItems())
                {
                    this.searchList.Add(item);
                }
            }

            InitializeComponent();

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }

        //makes a search page out of a list of items - searches the list
        public SearchPage(List<Item> searchList)
        {
            this.searchList = new List<ISearchable>();

            foreach (Item item in searchList)
            {
                this.searchList.Add(item);
            }

            InitializeComponent();

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }


        //makes a search page out of a list of containers - searches the list
        public SearchPage(List<ISearchable> searchList)
        {
            this.searchList = searchList;
            InitializeComponent();

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }

        //searches the list with the given term
        private List<ISearchable> Search(string term)
        {
            List<ISearchable> returnList = new List<ISearchable>();

            foreach (ISearchable item in searchList)
            {
                if (item.GetName().IndexOf(term, 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    returnList.Add(item);
                }
                else if (item.ContainsTag(term))
                {
                    returnList.Add(item);
                }
                else if (item.OverrideSearch(term))
                {
                    returnList.Add(item);
                }
            }

            return returnList;
        }

        //runs the search and displays the search results
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //clear the current results stack
            resultsStack.Children.Clear();

            if (searchTextInput.Text.Length <= 2) //do nothing if search term is less than 2 characters
            {
                return;
            }

            List<ISearchable> results = Search(searchTextInput.Text);

            //split results into locations, containers and items
            List<ISearchable> locationResults = new List<ISearchable>();
            List<ISearchable> containerResults = new List<ISearchable>();
            List<ISearchable> itemResults = new List<ISearchable>();
            foreach (ISearchable result in results)
            {
                if (typeof(StorageLocation).IsInstanceOfType(result))
                {
                    locationResults.Add(result);
                }
                else if (typeof(StorageContainer).IsInstanceOfType(result))
                {
                    containerResults.Add(result);
                }
                else if (typeof(Item).IsInstanceOfType(result))
                {
                    itemResults.Add(result);
                }
            }



            if (locationResults.Count != 0)
            {
                Label locationResultsLabel = new Label() { Text = "Locations", TextColor = PageColors.textColor, FontSize = FontSizes.titleFont };
                resultsStack.Children.Add(locationResultsLabel);
                foreach (ISearchable result in locationResults)
                {
                    resultsStack.Children.Add(MakeResultElement(result));
                }
            }
            if (containerResults.Count != 0)
            {
                Label containerResultsLabel = new Label() { Text = "Containers", TextColor = PageColors.textColor, FontSize = FontSizes.titleFont };
                resultsStack.Children.Add(containerResultsLabel);
                foreach (ISearchable result in containerResults)
                {
                    resultsStack.Children.Add(MakeResultElement(result));
                }
            }
            if (itemResults.Count != 0)
            {
                Label itemResultsLabel = new Label() { Text = "Items", TextColor = PageColors.textColor, FontSize = FontSizes.titleFont };
                resultsStack.Children.Add(itemResultsLabel);
                foreach (ISearchable result in itemResults)
                {
                    resultsStack.Children.Add(MakeResultElement(result));
                }
            }




        }

        //makes the element for a search result
        private Grid MakeResultElement(ISearchable result)
        {
            Grid returnGrid = new Grid();
            ImageSource imgSource = "camera";
            ImageButton gridImage = new ImageButton();


            //get correct image source
            //if none is found - the source will default to the camera source
            if (typeof(StorageLocation).IsInstanceOfType(result))
            {

                StorageLocation resultLocation = (StorageLocation)result;

                TapGestureRecognizer gesture = new TapGestureRecognizer();
                gesture.Tapped += (sen, e) => { Navigation.PushAsync(new LocationPage(resultLocation)); };
                returnGrid.GestureRecognizers.Add(gesture);
                gridImage.Clicked += (sen, e) => { Navigation.PushAsync(new LocationPage(resultLocation)); };

                if (ImageTools.HasImage(resultLocation))
                {
                    if (ImageBaseHandler.current.isLocationCached(resultLocation.GetId())) //check for caching of image
                    {
                        imgSource = ImageBaseHandler.current.GetLocationImageSource(resultLocation.GetId());
                    }
                    else
                    {
                        imgSource = ImageTools.LoadImage(resultLocation);
                    }

                }
            }
            else if (typeof(StorageContainer).IsInstanceOfType(result))
            {

                StorageContainer resultContainer = (StorageContainer)result;
                TapGestureRecognizer gesture = new TapGestureRecognizer();
                gesture.Tapped += (sen, e) => { Navigation.PushAsync(new ContainerPage(resultContainer)); };
                returnGrid.GestureRecognizers.Add(gesture);
                gridImage.Clicked += (sen, e) => { Navigation.PushAsync(new ContainerPage(resultContainer)); };

                if (ImageTools.HasImage(resultContainer))
                {
                    if (ImageBaseHandler.current.isContainerCached(resultContainer.GetId())) //check for caching of image
                    {
                        imgSource = ImageBaseHandler.current.GetContainerImageSource(resultContainer.GetId());
                    }
                    else
                    {
                        imgSource = ImageTools.LoadImage(resultContainer);
                    }

                }
            }
            else if (typeof(Item).IsInstanceOfType(result))
            {
                Item resultItem = (Item)result;
                TapGestureRecognizer gesture = new TapGestureRecognizer();
                gesture.Tapped += (sen, e) => { Navigation.PushAsync(new ItemDetailsPage(resultItem)); };
                returnGrid.GestureRecognizers.Add(gesture);
                gridImage.Clicked += (sen, e) => { Navigation.PushAsync(new ItemDetailsPage(resultItem)); };



                if (ImageTools.HasImage(resultItem))
                {
                    if (ImageBaseHandler.current.isItemCached(resultItem.GetId())) //check for caching of image
                    {
                        imgSource = ImageBaseHandler.current.GetItemImageSource(resultItem.GetId());
                    }
                    else
                    {
                        imgSource = ImageTools.LoadImage(resultItem);
                    }

                }
            }

            ColumnDefinition imageColumn = new ColumnDefinition() { Width = new GridLength(0.2, GridUnitType.Star) };
            ColumnDefinition nameColumn = new ColumnDefinition() { Width = new GridLength(0.8, GridUnitType.Star) };
            returnGrid.ColumnDefinitions.Add(imageColumn);
            returnGrid.ColumnDefinitions.Add(nameColumn);

            gridImage.Source = imgSource;
            gridImage.BackgroundColor = Color.Transparent;
            Grid.SetColumn(gridImage, 0);
            returnGrid.Children.Add(gridImage);

            Label nameLabel = new Label() { Text = result.GetName() };
            nameLabel.TextColor = PageColors.textColor;
            nameLabel.FontSize = FontSizes.subTitleFont;
            nameLabel.VerticalTextAlignment = TextAlignment.Center;
            Grid.SetColumn(nameLabel, 1);
            returnGrid.Children.Add(nameLabel);

            returnGrid.HeightRequest = MasterNavigationPage.current.Height / SEARCH_RESULTS_ON_PAGE;

            return returnGrid;
        }
    }
}