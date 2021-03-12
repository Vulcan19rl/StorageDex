using Android.Nfc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    class TagDisplay : StackLayout
{
        List<TagButton> tags = new List<TagButton>();


        //displays a list of tags in a box
        public TagDisplay(List<string> tags)
        {
            this.AddTagBatch(tags);
            this.Children.Add(new StackLayout() { Orientation = StackOrientation.Horizontal });
        }

        public TagDisplay()
        {
            this.Children.Add(new StackLayout() { Orientation = StackOrientation.Horizontal });
        }

        //adds a tag to the page
        private void AddTagToPage(TagButton newTag)
        {
            TagButton newButton = newTag;
            AddTagToOptimalRow(newButton);


        }

        //clears the tags in the display
        public void ClearTags()
        {

            this.Children.Clear();
            this.Children.Add(new StackLayout() { Orientation = StackOrientation.Horizontal });
            this.tags = new List<TagButton>();
        }

        //adds a new tag row to the current page and returns the new row
        private StackLayout MakeNewTagsRow()
        {
            StackLayout newLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal

            };
            this.Children.Add(newLayout);
            return newLayout;


        }

        //adds the given tag to the optimal row
        private void AddTagToOptimalRow(TagButton newTag)
        {
            List<View> rows = this.Children.ToList();
            rows.RemoveAt(0);

            int newTagWidth = newTag.CalculateButtonWidth();

            StackLayout optimalRow = null;

            if (newTagWidth >= MasterNavigationPage.current.Width)
            {
                optimalRow = MakeNewTagsRow();
            }
            else
            {
                foreach (StackLayout row in rows)
                {
                    if (row.Width == 0)
                    {
                        optimalRow = row;
                        break;
                    }
                    else if (LayoutWidth(row) + newTagWidth <=  MasterNavigationPage.current.Width)
                    {
                        optimalRow = row;
                        break;
                    }
                }

                if (optimalRow == null)
                {
                    optimalRow = MakeNewTagsRow();
                }
            }

            optimalRow.Children.Add(newTag);




        }

        //returns the current tags row
        private StackLayout GetCurrentTagsRow()
        {
            StackLayout currentRow = (StackLayout)this.Children.ToList().Last();
            return currentRow;
        }

        //returns the width of the layout
        //must be given a layout containing just tag buttons
        private double LayoutWidth(StackLayout rowIn)
        {
            double total = 0;
            foreach (TagButton button in rowIn.Children)
            {

                total += button.Width;
            }

            return total;
        }

        //refreshes the tag in the display
        public void RefreshTags()
        {
            this.Children.Clear();
            this.Children.Add(new StackLayout() { Orientation = StackOrientation.Horizontal });
            foreach (TagButton button in tags)
            {
                AddTagToPage(button);
            }
        }

        //adds a tag to the display
        public void AddTag(TagButton tagIn)
        {
            if(tagIn.Text == "") //if tag text is empty, dont add
            {
                return;
            }
            this.tags.Add(tagIn);
            AddTagToPage(tagIn);
            RefreshTags();
        }
        //adds a list of tags to the display
        public void AddTagBatch(List<TagButton> tagsIn)
        {
            foreach (TagButton tagIn in tagsIn)
            {
                AddTag(tagIn);
            }
        }

        //adds a list of tags to the display from strings
        public void AddTagBatch(List<string> tagsIn)
        {
            foreach(string tagIn in tagsIn)
            {
                AddTag(new TagButton(tagIn));
            }
        }

        //adds a list of string to the display without onclick
        public void AddTagToBatchWithoutClick(List<string> tagsIn)
        {
            foreach(string tagIn in tagsIn)
            {
                AddTag(new TagButton(tagIn, false));
            }
        }

        //deletes the tag from the display
        public void DeleteTags()
        {
            Console.WriteLine("deleting tags");
            int i = 0;
            while (i < tags.Count) //move through tag list to find all that are hightlighted and delete them
            {
                TagButton button = tags[i];
                if (button.isHighlighted())
                {
                    tags.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        //returns true if a tag can be deleted. aka is highlighted
        public bool CanDelete()
        {
            foreach (TagButton button in tags)
            {

                if (button.isHighlighted())
                {
                    return true;
                }
            }
            return false;
        }

        //returns a list of the tagbuttons in the display
        public List<TagButton> GetTags()
        {
            return tags;
        }

    

    }
}
