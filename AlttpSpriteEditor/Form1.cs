using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AlttpSpriteEditor
{
    public partial class FormMain : Form
    {
        public MemoryStream _ROM_Memory_Stream = new MemoryStream();

        int _header_offset;

        public bool _unsaved_changes = false;
        string _file_name;

        int _sprite_index;
        int _damage_type_index;
        int _prize_pack_index;
        List<string> _original_list = new List<string>();

        int _property_table1_location;
        int _property_table2_location;
        int _property_table3_location;
        int _property_table4_location;
        int _property_table5_location;
        int _property_table6_location;

        byte _property_byte1;
        byte _property_byte2;
        byte _property_byte3;
        byte _property_byte4;
        byte _property_byte5;
        byte _property_byte6;

        int _hp_table_location;

        int _damage_type_table_location;
        int _damage_type_definitions_table_location;

        int _prizepack_table_location;
        int _prizepack_drop_chance_table_location;
        int _prizepack_drops_table_location;

        byte _prizepack_drop1;
        byte _prizepack_drop2;
        byte _prizepack_drop3;
        byte _prizepack_drop4;
        byte _prizepack_drop5;
        byte _prizepack_drop6;
        byte _prizepack_drop7;
        byte _prizepack_drop8;

        // <Summary>
        // Initialize form and set some default values into some forms
        // </Summary>
        public FormMain()
        {
            InitializeComponent();

            for (int i = 0; i < CollectionsLists._sprites_collection.Count; i++)
            {
                _original_list.Add((string)CollectionsLists._sprites_collection[i]);
            }
        }
        
        // <summary>
        // this menu item loads a rom, converts it to a filestream,
        // copies it to a memorystream for to be edited. Then disposes of the filestream
        // </summary>
        private void LoadROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_unsaved_changes == true)
            {
                UnsavedChangesHandler();
            }

            if (_unsaved_changes == false)
            {
                if (loadROM.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(loadROM.FileName))
                    {
                        Console.WriteLine("File don't exist yo");
                        return;
                    }
                    _file_name = loadROM.FileName;
                    FileStream ROMFileStream = new FileStream(loadROM.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

                    _ROM_Memory_Stream.Position = 0;
                    ROMFileStream.CopyTo(_ROM_Memory_Stream);
                    ROMFileStream.Dispose();

                    _header_offset = (Autodetect.CheckHeader(_ROM_Memory_Stream) == 0) ? 0x00 : 0x0200;

                    if (Autodetect.CheckRegion(_ROM_Memory_Stream) != 0)
                    {
                        _property_table1_location = 0x6B080 + _header_offset;
                        _property_table2_location = 0x6B359 + _header_offset;
                        _property_table3_location = 0x6B44C + _header_offset;
                        _property_table4_location = 0x6B53F + _header_offset;
                        _property_table5_location = 0x6B632 + _header_offset;
                        _property_table6_location = 0x6B725 + _header_offset;

                        _hp_table_location = 0x6B173 + _header_offset;

                        _damage_type_table_location = 0x6B266 + _header_offset;

                        _prizepack_table_location = 0x6B632 + _header_offset;

                        _damage_type_definitions_table_location = ((Autodetect.CheckRegion(_ROM_Memory_Stream) == 1) ? 0x37427 : 0x3742D) + _header_offset;
                        _prizepack_drop_chance_table_location = ((Autodetect.CheckRegion(_ROM_Memory_Stream) == 1) ? 0x37A5C : 0x37A62) + _header_offset;
                        _prizepack_drops_table_location = ((Autodetect.CheckRegion(_ROM_Memory_Stream) == 1) ? 0x37A72 : 0x37A78) + _header_offset;

                        SpriteListBox.SelectedIndex = 1;
                        SpriteListBox.SelectedIndex = 0;
                        DamageTypesComboBox.SelectedIndex = 1;
                        DamageTypesComboBox.SelectedIndex = 0;
                        PrizePacksComboBox.SelectedIndex = 1;
                        PrizePacksComboBox.SelectedIndex = 0;

                        TabControlMain.Enabled = true;

                        _unsaved_changes = false;
                    }
                    else
                    {
                        BadRomDialog error = new BadRomDialog();
                        error.ShowDialog(this);
                    }
                }
            }
        }

        // <Summary>
        // this menu item will save the memorystream to a filestream chosen by the dialog.
        // </Summary>
        private void SaveROMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void Save()
        {
            FileStream ROMFileStream = new FileStream(_file_name, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            _ROM_Memory_Stream.Position = 0;
            _ROM_Memory_Stream.CopyTo(ROMFileStream);
            ROMFileStream.Dispose();

            _unsaved_changes = false;
        }

        private void UnsavedChangesHandler()
        {
            UnsavedChangesDialog unsavedDialog = new UnsavedChangesDialog();
            DialogResult result = unsavedDialog.ShowDialog(this);

            if (result == DialogResult.Yes)
            {
                _unsaved_changes = false;
                Save();
            }
            if (result == DialogResult.No)
            {
                _unsaved_changes = false;
            }
        }

        private void SaveAs()
        {
            if (saveROM.ShowDialog() == DialogResult.OK)
            {
                FileStream ROMFileStream = new FileStream(saveROM.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                _ROM_Memory_Stream.Position = 0;
                _ROM_Memory_Stream.CopyTo(ROMFileStream);
                ROMFileStream.Dispose();

                _unsaved_changes = false;
            }
        }

        // <Summary>
        // gets a byte to fill a form on the sprite editor when a new sprite is selected
        // from a list
        // </Summary>
        public static byte GetByte(int startPos, int index, MemoryStream ROMStream)
        {
            ROMStream.Position = (startPos + index);
            byte x = (byte)ROMStream.ReadByte();
            return x;
        }

        // <Summary>
        // sets a byte in the rom memory stream when it's changed in a form in the sprite editor
        // </Summary>
        public static void SetByte(int startPos, int index, MemoryStream ROMStream, byte newVal)
        {
            ROMStream.Position = (startPos + index);
            ROMStream.WriteByte(newVal);
        }

        private void SpriteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sprite_index = Conversion.ParseHexString(SpriteListBox.SelectedItem.ToString().Substring(1, 2));

            HealthTextBox.Text = GetByte(_hp_table_location, _sprite_index, _ROM_Memory_Stream).ToString();
            DamageTypeSelector.Value = GetByte(_damage_type_table_location, _sprite_index, _ROM_Memory_Stream) & 0x0F;
            PrizePackSelector.Value = GetByte(_prizepack_table_location, _sprite_index, _ROM_Memory_Stream) & 0x0F;

            _property_byte1 = GetByte(_property_table1_location, _sprite_index, _ROM_Memory_Stream);
            _property_byte2 = GetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream);
            _property_byte3 = GetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream);
            _property_byte4 = GetByte(_property_table4_location, _sprite_index, _ROM_Memory_Stream);
            _property_byte5 = GetByte(_property_table5_location, _sprite_index, _ROM_Memory_Stream);
            _property_byte6 = GetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream);

            //1
            OamSlotsSelector.Value = _property_byte1 & 0x1F;
            CollideLessCheckBox.Checked = ((_property_byte1 & 0x20) == 0x00) ? false : true;
            HarmlessCheckBox.Checked = ((_property_byte1 & 0x80) == 0x00) ? false : true;
            //2
            NoDeathAnimationCheckBox.Checked = ((_property_byte2 & 0x80) == 0x00) ? false : true;
            InvulnerableCheckBox.Checked = ((_property_byte2 & 0x40) == 0x00) ? false : true;
            AdjustChildCoordinatesCheckBox.Checked = ((_property_byte2 & 0x20) == 0x00) ? false : true;
            DrawShadowCheckBox.Checked = ((_property_byte2 & 0x10) == 0x00) ? false : true;
            PaletteSelector.Value = (_property_byte2 & 0x0E) >> 1;
            //3
            IgnoreCollisionCheckBox.Checked = ((_property_byte3 & 0x80) == 0x00) ? false : true;
            StatisCheckBox.Checked = ((_property_byte3 & 0x40) == 0x00) ? false : true;
            PersistCheckBox.Checked = ((_property_byte3 & 0x20) == 0x00) ? false : true;
            HitBoxDimensionsSelector.Value = _property_byte3 & 0x1F;
            //4
            InteractiveTileHitBoxSelector.Value = (_property_byte4 & 0xF0) >> 4;
            DiesLikeABossCheckBox.Checked = ((_property_byte4 & 0x02) == 0x00) ? false : true;
            FallsInHolesCheckBox.Checked = ((_property_byte4 & 0x01) == 0x00) ? false : true;
            //5
            DisableTileInteractionsCheckBox.Checked = ((_property_byte5 & 0x80) == 0x00) ? false : true;
            IsShieldBlockableCheckBox.Checked = ((_property_byte5 & 0x20) == 0x00) ? false : true;
            AlternateDamageSoundCheckBox.Checked = ((_property_byte5 & 0x10) == 0x00) ? false : true;
            //6
            DeflectsProjectilesCheckBox.Checked = ((_property_byte6 & 0x10) == 0x00) ? false : true;
            ImperviousToSwordCheckBox.Checked = ((_property_byte6 & 0x04) == 0x00) ? false : true;
            ImperviousToArrowsCheckBox.Checked = ((_property_byte6 & 0x02) == 0x00) ? false : true;
            CollideLessCheckBox.Checked = ((_property_byte6 & 0x01) == 0x00) ? false : true;
        }

        // <Summary>
        // Refines the sprite list as text is entered into a search box
        // </Summary>
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SpriteListBox.Items.Clear();

            List<string> refinedList = SearchBoxTools.ListBoxRefinement(_original_list, SearchTextBox.Text.ToLower());

            for (int i = 0; i < refinedList.Count; i++)
            {
                SpriteListBox.Items.Add(refinedList[i]);
            }
        }

        // </Summary
        // controller for the hp editor text box, checks if an input is valid then updates the hp of the selected
        // sprite in the main memorystream of the rom
        // </Summary>
        private void HealthTextBox_TextChanged(object sender, EventArgs e)
        {
            int? val = TextBoxExceptionHandler.CheckNumericRange(HealthTextBox.Text, 255);

            HealthTextBox.Text = val.ToString();

            if (val == null)
            {
                val = 0;
            }

            SetByte(_hp_table_location, _sprite_index, _ROM_Memory_Stream, (byte)val);

            _unsaved_changes = true;
        }

        // <Summary>
        // controller for the damage type selector. Updates the damage type of the selected sprite
        // when the damage type is changed, within a set range of values
        // </Summary>
        private void DamageTypeSelector_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)DamageTypeSelector.Value;
            int val2 = (int)(GetByte(_damage_type_table_location, _sprite_index, _ROM_Memory_Stream) & 0xF0);
            byte val3 = (byte)(val2 | val);
            SetByte(_damage_type_table_location, _sprite_index, _ROM_Memory_Stream, val3);

            _unsaved_changes = true;
        }

        // <Summary>
        // controller for the prize pack selector. Updates the prize pack of the selected sprite
        // when the prize pack is changed, within a set range of values
        // </Summary>
        private void PrizePackSelector_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)PrizePackSelector.Value;
            int val2 = (int)(GetByte(_prizepack_table_location, _sprite_index, _ROM_Memory_Stream) & 0xF0);
            byte val3 = (byte)(val2 | val);
            SetByte(_prizepack_table_location, _sprite_index, _ROM_Memory_Stream, val3);

            _unsaved_changes = true;
        }

        private void OamSlotsSelector_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)OamSlotsSelector.Value;
            byte val2 = (byte)((GetByte(_property_table1_location, _sprite_index, _ROM_Memory_Stream) & 0xE0) | val);
            SetByte(_property_table1_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void PaletteSelector_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)PaletteSelector.Value;
            byte val2 = (byte)((GetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream) & 0xF1) | (val << 1));
            SetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void HitBoxDimensionsSelector_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)HitBoxDimensionsSelector.Value;
            byte val2 = (byte)((GetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream) & 0xE0) | val);
            SetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void InteractiveTileHitBoxSelector_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)InteractiveTileHitBoxSelector.Value;
            byte val2 = (byte)((GetByte(_property_table4_location, _sprite_index, _ROM_Memory_Stream) & 0x0F) | (val << 4));
            SetByte(_property_table4_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void HarmlessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (HarmlessCheckBox.Checked == true) ? 0x80 : 0x00;
            byte val2 = (byte)((GetByte(_property_table1_location, _sprite_index, _ROM_Memory_Stream) & 0x7F) | val);
            SetByte(_property_table1_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void InvulnerableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (InvulnerableCheckBox.Checked == true) ? 0x40 : 0x00;
            byte val2 = (byte)((GetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream) & 0xBF) | val);
            SetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void AdjustChildCoordinatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (AdjustChildCoordinatesCheckBox.Checked == true) ? 0x20 : 0x00;
            byte val2 = (byte)((GetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream) & 0xDF) | val);
            SetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void DrawShadowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (DrawShadowCheckBox.Checked == true) ? 0x10 : 0x00;
            byte val2 = (byte)((GetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream) & 0xEF) | val);
            SetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void NoDeathAnimationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (NoDeathAnimationCheckBox.Checked == true) ? 0x80 : 0x00;
            byte val2 = (byte)((GetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream) & 0x7F) | val);
            SetByte(_property_table2_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void DiesLikeABossCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (DiesLikeABossCheckBox.Checked == true) ? 0x02 : 0x00;
            byte val2 = (byte)((GetByte(_property_table4_location, _sprite_index, _ROM_Memory_Stream) & 0xFD) | val);
            SetByte(_property_table4_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void IsShieldBlockableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (IsShieldBlockableCheckBox.Checked == true) ? 0x20 : 0x00;
            byte val2 = (byte)((GetByte(_property_table5_location, _sprite_index, _ROM_Memory_Stream) & 0xDF) | val);
            SetByte(_property_table5_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void StatisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (StatisCheckBox.Checked == true) ? 0x40 : 0x00;
            byte val2 = (byte)((GetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream) & 0xBF) | val);
            SetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void PersistCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (PersistCheckBox.Checked == true) ? 0x20 : 0x00;
            byte val2 = (byte)((GetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream) & 0xDF) | val);
            SetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void FallsInHolesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (FallsInHolesCheckBox.Checked == true) ? 0x01 : 0x00;
            byte val2 = (byte)((GetByte(_property_table4_location, _sprite_index, _ROM_Memory_Stream) & 0xFE) | val);
            SetByte(_property_table4_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void AlternateDamageSoundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (AlternateDamageSoundCheckBox.Checked == true) ? 0x10 : 0x00;
            byte val2 = (byte)((GetByte(_property_table5_location, _sprite_index, _ROM_Memory_Stream) & 0xEF) | val);
            SetByte(_property_table5_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void IgnoreCollisionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (IgnoreCollisionCheckBox.Checked == true) ? 0x80 : 0x00;
            byte val2 = (byte)((GetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream) & 0x7F) | val);
            SetByte(_property_table3_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void DisableTileInteractionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (DisableTileInteractionsCheckBox.Checked == true) ? 0x80 : 0x00;
            byte val2 = (byte)((GetByte(_property_table5_location, _sprite_index, _ROM_Memory_Stream) & 0x7F) | val);
            SetByte(_property_table5_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void ImperviousToSwordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (ImperviousToSwordCheckBox.Checked == true) ? 0x04 : 0x00;
            byte val2 = (byte)((GetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream) & 0xFB) | val);
            SetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void DeflectsProjectilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (DeflectsProjectilesCheckBox.Checked == true) ? 0x10 : 0x00;
            byte val2 = (byte)((GetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream) & 0xEF) | val);
            SetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void ImperviousToArrowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (ImperviousToArrowsCheckBox.Checked == true) ? 0x02 : 0x00;
            byte val2 = (byte)((GetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream) & 0xFD) | val);
            SetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        private void CollideLessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int val = (CollideLessCheckBox.Checked == true) ? 0x01 : 0x00;
            byte val2 = (byte)((GetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream) & 0xFE) | val);
            SetByte(_property_table6_location, _sprite_index, _ROM_Memory_Stream, val2);

            _unsaved_changes = true;
        }

        // <Summary>
        // This is the selector for the damage type currently being edited. Updates editable fields when changed.
        // </Summary>
        private void DamageTypesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _damage_type_index = DamageTypesComboBox.SelectedIndex;
            Armor1TextBox.Text = GetByte(_damage_type_definitions_table_location, (_damage_type_index * 3), _ROM_Memory_Stream).ToString();
            Armor2TextBox.Text = GetByte(_damage_type_definitions_table_location, (_damage_type_index * 3) + 1, _ROM_Memory_Stream).ToString();
            Armor3TextBox.Text = GetByte(_damage_type_definitions_table_location, (_damage_type_index * 3) + 2, _ROM_Memory_Stream).ToString();
        }

        // <Summary>
        // Controller for text box which receives damage a sprite does to link with the selected damage type
        // when link is wearing level 1 armor. Checks for valid inupt then updates the memorystream when changed.
        // </Summary>
        private void Armor1TextBox_TextChanged(object sender, EventArgs e)
        {
            int? val = TextBoxExceptionHandler.CheckNumericRange(Armor1TextBox.Text, 255);
            Armor1TextBox.Text = val.ToString();

            if (val == null)
            {
                val = 0;
            }

            SetByte(_damage_type_definitions_table_location, (_damage_type_index * 3), _ROM_Memory_Stream, (byte)val);

            _unsaved_changes = true;
        }

        // <Summary>
        // Controller for text box which receives damage a sprite does to link with the selected damage type
        // when link is wearing level 2 armor. Checks for valid inupt then updates the memorystream when changed.
        // </Summary>
        private void Armor2TextBox_TextChanged(object sender, EventArgs e)
        {
            int? val = TextBoxExceptionHandler.CheckNumericRange(Armor2TextBox.Text, 255);
            Armor2TextBox.Text = val.ToString();

            if (val == null)
            {
                val = 0;
            }

            SetByte(_damage_type_definitions_table_location, (_damage_type_index * 3) + 1, _ROM_Memory_Stream, (byte)val);

            _unsaved_changes = true;
        }

        // <Summary>
        // Controller for text box which receives damage a sprite does to link with the selected damage type
        // when link is wearing level 3 armor. Checks for valid inupt then updates the memorystream when changed.
        // </Summary>
        private void Armor3TextBox_TextChanged(object sender, EventArgs e)
        {
            int? val = TextBoxExceptionHandler.CheckNumericRange(Armor3TextBox.Text, 255);
            Armor3TextBox.Text = val.ToString();

            if (val == null)
            {
                val = 0;
            }

            SetByte(_damage_type_definitions_table_location, (_damage_type_index * 3) + 2, _ROM_Memory_Stream, (byte)val);

            _unsaved_changes = true;
        }

        private void PrizePacksComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _prize_pack_index = PrizePacksComboBox.SelectedIndex;

            int dropPercent = GetByte(_prizepack_drop_chance_table_location, _prize_pack_index, _ROM_Memory_Stream);

            if (dropPercent == 1)
            {
                FiftyPercentRadioButton.Checked = true;
            }
            else
            {
                HundredPercentRadioButton.Checked = true;
            }

            _prizepack_drop1 = GetByte(_prizepack_drops_table_location, (_prize_pack_index * 8), _ROM_Memory_Stream);
            _prizepack_drop2 = GetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 1, _ROM_Memory_Stream);
            _prizepack_drop3 = GetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 2, _ROM_Memory_Stream);
            _prizepack_drop4 = GetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 3, _ROM_Memory_Stream);
            _prizepack_drop5 = GetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 4, _ROM_Memory_Stream);
            _prizepack_drop6 = GetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 5, _ROM_Memory_Stream);
            _prizepack_drop7 = GetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 6, _ROM_Memory_Stream);
            _prizepack_drop8 = GetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 7, _ROM_Memory_Stream);

            Drop1Button.Text = 
                CollectionsLists._sprites_collection[_prizepack_drop1].Substring(6);
            Drop2Button.Text = 
                CollectionsLists._sprites_collection[_prizepack_drop2].Substring(6);
            Drop3Button.Text = 
                CollectionsLists._sprites_collection[_prizepack_drop3].Substring(6);
            Drop4Button.Text = 
                CollectionsLists._sprites_collection[_prizepack_drop4].Substring(6);
            Drop5Button.Text = 
                CollectionsLists._sprites_collection[_prizepack_drop5].Substring(6);
            Drop6Button.Text = 
                CollectionsLists._sprites_collection[_prizepack_drop6].Substring(6);
            Drop7Button.Text = 
                CollectionsLists._sprites_collection[_prizepack_drop7].Substring(6);
            Drop8Button.Text = 
                CollectionsLists._sprites_collection[_prizepack_drop8].Substring(6);
        }

        private void FiftyPercentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            int val = (FiftyPercentRadioButton.Checked == true) ? 0x01 : 0x00;
            SetByte(_prizepack_drop_chance_table_location, _prize_pack_index, _ROM_Memory_Stream, (byte)val);

            _unsaved_changes = true;
        }

        private void HundredPercentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            int val = (HundredPercentRadioButton.Checked == true) ? 0x00 : 0x01;
            SetByte(_prizepack_drop_chance_table_location, _prize_pack_index, _ROM_Memory_Stream, (byte)val);

            _unsaved_changes = true;
        }

        private void Drop1Button_Click(object sender, EventArgs e)
        {
            SpriteSelectorWindow selectorWindow = new SpriteSelectorWindow(_original_list, _prizepack_drop1);
            selectorWindow.ShowDialog(this);
            _prizepack_drop1 = (byte)selectorWindow._index;
            selectorWindow.Dispose();

            SetByte(_prizepack_drops_table_location, (_prize_pack_index * 8), _ROM_Memory_Stream, _prizepack_drop1);

            Drop1Button.Text =
                CollectionsLists._sprites_collection[_prizepack_drop1].Substring(6);

            _unsaved_changes = true;
        }

        private void Drop2Button_Click(object sender, EventArgs e)
        {
            SpriteSelectorWindow selectorWindow = new SpriteSelectorWindow(_original_list, _prizepack_drop2);
            selectorWindow.ShowDialog(this);
            _prizepack_drop2 = (byte)selectorWindow._index;
            selectorWindow.Dispose();

            SetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 1, _ROM_Memory_Stream, _prizepack_drop2);

            Drop2Button.Text =
                CollectionsLists._sprites_collection[_prizepack_drop2].Substring(6);

            _unsaved_changes = true;
        }

        private void Drop3Button_Click(object sender, EventArgs e)
        {
            SpriteSelectorWindow selectorWindow = new SpriteSelectorWindow(_original_list, _prizepack_drop3);
            selectorWindow.ShowDialog(this);
            _prizepack_drop3 = (byte)selectorWindow._index;
            selectorWindow.Dispose();

            SetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 2, _ROM_Memory_Stream, _prizepack_drop3);

            Drop3Button.Text =
                CollectionsLists._sprites_collection[_prizepack_drop3].Substring(6);

            _unsaved_changes = true;
        }

        private void Drop4Button_Click(object sender, EventArgs e)
        {
            SpriteSelectorWindow selectorWindow = new SpriteSelectorWindow(_original_list, _prizepack_drop4);
            selectorWindow.ShowDialog(this);
            _prizepack_drop4 = (byte)selectorWindow._index;
            selectorWindow.Dispose();

            SetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 3, _ROM_Memory_Stream, _prizepack_drop4);

            Drop4Button.Text =
                CollectionsLists._sprites_collection[_prizepack_drop4].Substring(6);

            _unsaved_changes = true;
        }

        private void Drop5Button_Click(object sender, EventArgs e)
        {
            SpriteSelectorWindow selectorWindow = new SpriteSelectorWindow(_original_list, _prizepack_drop5);
            selectorWindow.ShowDialog(this);
            _prizepack_drop5 = (byte)selectorWindow._index;
            selectorWindow.Dispose();

            SetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 4, _ROM_Memory_Stream, _prizepack_drop5);

            Drop5Button.Text =
                CollectionsLists._sprites_collection[_prizepack_drop5].Substring(6);

            _unsaved_changes = true;
        }

        private void Drop6Button_Click(object sender, EventArgs e)
        {
            SpriteSelectorWindow selectorWindow = new SpriteSelectorWindow(_original_list, _prizepack_drop6);
            selectorWindow.ShowDialog(this);
            _prizepack_drop6 = (byte)selectorWindow._index;
            selectorWindow.Dispose();

            SetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 5, _ROM_Memory_Stream, _prizepack_drop6);

            Drop6Button.Text =
                CollectionsLists._sprites_collection[_prizepack_drop6].Substring(6);

            _unsaved_changes = true;
        }

        private void Drop7Button_Click(object sender, EventArgs e)
        {
            SpriteSelectorWindow selectorWindow = new SpriteSelectorWindow(_original_list, _prizepack_drop7);
            selectorWindow.ShowDialog(this);
            _prizepack_drop7 = (byte)selectorWindow._index;
            selectorWindow.Dispose();

            SetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 6, _ROM_Memory_Stream, _prizepack_drop7);

            Drop7Button.Text =
                CollectionsLists._sprites_collection[_prizepack_drop7].Substring(6);

            _unsaved_changes = true;
        }

        private void Drop8Button_Click(object sender, EventArgs e)
        {
            SpriteSelectorWindow selectorWindow = new SpriteSelectorWindow(_original_list, _prizepack_drop8);
            selectorWindow.ShowDialog(this);
            _prizepack_drop8 = (byte)selectorWindow._index;
            selectorWindow.Dispose();

            SetByte(_prizepack_drops_table_location, (_prize_pack_index * 8) + 7, _ROM_Memory_Stream, _prizepack_drop8);

            Drop8Button.Text =
                CollectionsLists._sprites_collection[_prizepack_drop8].Substring(6);

            _unsaved_changes = true;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_unsaved_changes == true)
            {
                UnsavedChangesHandler();
            }
            if (_unsaved_changes == true)
            {
                e.Cancel = true;
            }
        }
    }
}
