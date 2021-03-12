using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GridImageButtonView : ContentView
{
        //the amount of images in a row
        private readonly int IMAGES_IN_ROW = 3;
        private int currentPosInRow = 0; //the current position in the image row
        private bool endOfRow = false; //whether or not the image row is renderer is at the end

        public GridImageButtonView()
        {
            InitializeComponent();

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;


        }

        //clears everything on the screen
        public void Clear()
        {
            mainLayout.Children.Clear();
            currentPosInRow = 0;
            endOfRow = false;
        }

        //adds a view to the image button grid
        public void AddImageButtonEquivalent(View viewIn)
        {
            List<View> children = mainLayout.Children.ToList();





            Grid toAddTo = new Grid();
            toAddTo.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });
            toAddTo.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });
            toAddTo.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });
            toAddTo.RowDefinitions.Add(new RowDefinition { Height = MasterNavigationPage.current.Height / 5 });



            if (children.Count == 0) //if no rows so far, add one
            {

                mainLayout.Children.Add(toAddTo);
                toAddTo.Children.Add(viewIn);

            }
            else if (endOfRow)
            {
                Grid.SetColumn(viewIn, currentPosInRow);
                mainLayout.Children.Add(toAddTo);
                toAddTo.Children.Add(viewIn);

            }
            else
            {
                Grid.SetColumn(viewIn, currentPosInRow);
                ((Grid)children.Last()).Children.Add(viewIn);

            }

            currentPosInRow++;
            if (currentPosInRow + 1 > IMAGES_IN_ROW)
            {
                endOfRow = true;
                currentPosInRow = 0;
            }
            else
            {
                endOfRow = false;
            }
        }
        //adds an image to the layout
        //send null to get the camera image source
        public void AddImageButton(ImageButton imageButtonIn)
        {
            AddImageButtonEquivalent(imageButtonIn);


        }
    }
}