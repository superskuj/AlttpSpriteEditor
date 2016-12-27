using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlttpSpriteEditor
{
    public partial class SpriteSelectorWindow : Form
    {
        List<String> _original_list;
        public int _index;

        // <Summary>
        // Constructor for this form. Receives a list to display and a reference to an Item to edit.
        // Displays the list in a listbox and determines which list-item to select based on a property of the referenced Item
        // </Summary>
        public SpriteSelectorWindow(List<String> originalList, int index)
        {
            InitializeComponent();
            
            _original_list = originalList;
            _index = index;

            for (int i = 0; i < _original_list.Count; i++)
            {
                SpriteSelectorListBox.Items.Add(_original_list[i]);
            }

            SpriteSelectorListBox.SelectedIndex = _index;
        }

        // <Summary>
        // List box refiner, uses a method from SearchBoxTools
        // </Summary>
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SpriteSelectorListBox.Items.Clear();

            List<string> refinedList = SearchBoxTools.ListBoxRefinement(_original_list, SearchTextBox.Text.ToLower());

            for (int i = 0; i < refinedList.Count; i++)
            {
                SpriteSelectorListBox.Items.Add(refinedList[i]);
            }
        }

        private void SpriteSelectorListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //probably don't need this event handler
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            _index = Conversion.ParseHexString(SpriteSelectorListBox.SelectedItem.ToString().Substring(1, 2));
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
