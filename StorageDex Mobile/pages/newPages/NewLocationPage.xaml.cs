﻿using Android.Content.Res;
using Android.Nfc;
using Android.Runtime;
using Android.Service.Autofill;
using FFImageLoading.Forms;
using Org.W3c.Dom;
using Plugin.Media.Abstractions;
using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.miscPages;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.pages.newPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewLocationPage : ContentPage, INewPage, IRefreshable, IConfirmable, IToolbarDelete
    {



        public MediaFile finalImage;
        private int newLocationId; //the id of the new location - generated by the database
        private StorageLocation previousLocation;
        private bool isEditing = false; //if the page is being used to edit a location
        private TagDisplay tagDisplay;

        public NewLocationPage() : base()
        {
            InitializeComponent();
            newLocationId = DatabaseHandler.GetDatabase().GenerateNewLocationId(); //get the id for the new location




            //set colors
            this.BackgroundColor = PageColors.secondaryColor;

            InitContent();

            //this.Focused += (sen, e) =>
            //{
            //    if (currentMultiTask != null && !currentMultiTask.IsCompleted)
            //    {
            //        currentMultiTask.Dispose();
            //        currentMultiTask = null;
            //    }
            //};
        }


        //for editing a preexisting location
        public NewLocationPage(string name, string address, string city, string country, string notes, List<string> tags, int id, StorageLocation prevLocation) : base()
        {
            InitializeComponent();
            InitContent();
            newLocationId = id;
            this.nameInput.Text = name;
            this.streetAddressInput.Text = address;
            this.cityInput.Text = city;
            this.countryInput.Text = country;
            this.notesEditor.Text = notes;
           
            this.previousLocation = prevLocation;
            this.isEditing = true;

            if (isEditing)
            {
                this.Title = "Editing";
            }

            this.tagDisplay.AddTagBatch(tags);
        

            //set image
            if (ImageTools.HasImage(prevLocation))
            {

                if (ImageBaseHandler.current.isContainerCached(prevLocation.GetId()))
                {
                    SetImageHolderContent(ImageBaseHandler.current.GetLocationImageSource(previousLocation.GetId()));
                }
                else
                {
                    ImageSource imageLoadTask = ImageTools.LoadImage(System.IO.Path.Combine(Directories.locationImageDirectory, id.ToString() + ".jpg"));
                    SetImageHolderContent(imageLoadTask);
                }

               
                
            }

            
            





            //set colors
            this.BackgroundColor = PageColors.secondaryColor;

            

            //this.Focused += (sen, e) =>
            //{
            //    if (currentMultiTask != null && !currentMultiTask.IsCompleted)
            //    {
            //        currentMultiTask.Dispose();
            //        currentMultiTask = null;
            //    }
            //};
        }



        //initializes the content on the page 
        private void InitContent()
        {

            this.locationName.TextColor = PageColors.textColor;
            this.nameInput.TextColor = PageColors.textColor;
            this.streetAddress.TextColor = PageColors.textColor;
            this.streetAddressInput.TextColor = PageColors.textColor;
            this.city.TextColor = PageColors.textColor;
            this.cityInput.TextColor = PageColors.textColor;
            this.country.TextColor = PageColors.textColor;
            this.countryInput.TextColor = PageColors.textColor;
            this.tags.TextColor = PageColors.textColor;
            this.tagsInput.TextColor = PageColors.textColor;
            this.notes.TextColor = PageColors.textColor;
            this.notesEditor.TextColor = PageColors.textColor;

            tagDisplay = new TagDisplay(new List<string>());
            this.tagDisplayWrapper.Children.Add(tagDisplay);


        }

        //asks the user to pic a photo
        public async void AttachPhoto()
        {
            if (await UserPermissions.ValidateStoragePermissions()) //check permissions
            {
                MediaFile picked = await ExternalResources.PickPhoto();
                FinalizePhoto(picked);

            }
        }

        //has the user take a photo
        public async void TakePhoto()
        {
            if (await UserPermissions.ValidateStoragePermissions() && await UserPermissions.ValidateCameraPermissions()) //check permissions
            {

                MediaFile taken = await ExternalResources.TakePhoto();
                FinalizePhoto(taken);
            }
        }

        //finalizes a selected photo. puts it in the right directory and display it on the page
        private void FinalizePhoto(MediaFile imageIn)
        {
            CachedImage loadingImage = new CachedImage()
            {
                Source = "loading"
            };

            if(imageIn != null)
            {
                this.imageHolder.Content = loadingImage;
                finalImage = imageIn;
                SetImageHolderContent(ImageTools.MediaFileToImageSource(imageIn));

            }



        }

        //sets the image holder content
        private void SetImageHolderContent(ImageSource imageIn)
        {
            ImageButton newButton = new ImageButton() { Source = imageIn };
            this.imageHolder.Content = newButton;
            newButton.Clicked += (sen, e) => Navigation.PushAsync(new ImagePage(imageIn));
        }

        //compress the picture, move it to the correct directory and renames it
        //returns the new compressed and located image source
        private async Task<ImageSource> ImportPhoto(MediaFile photo)
        {

            ImageBaseHandler.current.AddLocationImage(newLocationId, ImageTools.MediaFileToImageSource(photo));
            await ImageTools.ImportImage(photo, Directories.locationImageDirectory, newLocationId.ToString());//save the new image in the internal storage with its id as the name
            return ImageTools.MediaFileToImageSource(finalImage);
        }

        //refreshes the current page
        public void Refresh()
        {
    
            RefreshTags();
            if (finalImage == null)
            {
                this.imageHolder.Content = null;
            }
            else
            {
                SetImageHolderContent(ImageTools.MediaFileToImageSource(finalImage));
            }
            

        }

        //refreshes the current tags on the page
        private void RefreshTags()
        {
            tagDisplay.RefreshTags();
        }

        //handles the tag input box
        //moves the new tag onto the line below when it is finished
        private void TagInputOnType(Object sender, TextChangedEventArgs args)
        {
            string text = tagsInput.Text;
            if (text.Trim() == ",")
            {
                tagsInput.Text = "";
            }
            else if (text.EndsWith(",")) //if ends with comma, tag is finished
            {
                string newTag = text.Substring(0, text.Length - 1);
                TagButton newTagButton = new TagButton(newTag);
                tagDisplay.AddTag(newTagButton);
                tagsInput.Text = "";

            }
        }

        //handles the enter key being pressed on tag input 
        private void TagInputCompleted(Object sender, EventArgs args)
        {
            string text = tagsInput.Text;
            if (text.Trim() == "," || text.Trim() == "")
            {
                tagsInput.Text = "";
            }
            else
            {
                TagButton newTagButton = new TagButton(text);
                tagDisplay.AddTag(newTagButton);
                tagsInput.Text = "";
            }
        }

       





        

        //whether or not something can be deleted
        //returns true if any of the tags are highlighted
        public bool CanDelete()
        {
            return tagDisplay.CanDelete();
        }

        //deletes all the highlighted tags
        public void Delete()
        {
            tagDisplay.DeleteTags();
        }

        //confirms the new location
        public async void Confirm()
        {

            if (ValidateName())
            {
                CreateLocation();
                if(finalImage != null)
                {
                    await ImportPhoto(finalImage);
                }

                Finish();
            }
        }
        
        
        //validates whether there is a name
        //returns true if so
        private bool ValidateName()
        {
            if(nameInput.Text.Trim() == "")
            {
                Task alert = DisplayAlert("Missing Required Information", "Please enter a location name", "OK");
                return false;
            }
            else
            {
                return true;
            }
        }
        
        //creates the location and add to database
        private void CreateLocation()
        {
           
            List<string> tagNamesList = new List<string>();
            foreach(TagButton button in tagDisplay.GetTags())
            {
                tagNamesList.Add(button.GetButtonText());
            }
            if (isEditing) //if editing a previous location, change the value
            {
                previousLocation.name = nameInput.Text.Trim();
                previousLocation.address = streetAddressInput.Text.Trim();
                previousLocation.city = cityInput.Text.Trim();
                previousLocation.country = countryInput.Text.Trim();
                previousLocation.tags = tagNamesList;
                previousLocation.notes = notesEditor.Text;

            }
            else
            {


                

                StorageLocation newLocation = new StorageLocation(newLocationId, nameInput.Text.Trim(), streetAddressInput.Text.Trim(),
      cityInput.Text.Trim(), countryInput.Text.Trim(), tagNamesList, notesEditor.Text);

                DatabaseHandler.GetDatabase().AddLocation(newLocation);
            }
  
        }

        //finishes the location creation
        private async void Finish()
        {
            DataTools.Save(); //save data
            await Navigation.PopAsync(); //go to location page once created
        }


    }


}