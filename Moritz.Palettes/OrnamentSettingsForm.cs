using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

using Moritz.Globals;

namespace Moritz.Palettes
{
    public partial class OrnamentSettingsForm : Form
    {
        public OrnamentSettingsForm(PaletteForm paletteForm)
        {
            InitializeOrnamentSettingsForm(null, paletteForm);
        }

        public OrnamentSettingsForm(XmlReader r, PaletteForm paletteForm)
        {
            InitializeOrnamentSettingsForm(r, paletteForm);
        }

        private void InitializeOrnamentSettingsForm(XmlReader r, PaletteForm paletteForm)
        {
            InitializeComponent();
            _paletteForm = paletteForm;
            ConnectBasicChordControl();

            //int numberOfChordValues = -1;
            int numberOfBasicChordDefs = 12;
            if(r != null)
            {
                numberOfBasicChordDefs = ReadOrnamentSettingsForm(r);
                _paletteForm.SetSettingsHaveBeenSaved();
                this.Text = _paletteForm.Text + ": ornaments";
            }
            else
            {
                _paletteForm.SetSettingsHaveBeenSaved(); // removes the '*'
                this.Text = _paletteForm.Text + ": ornaments";
                _paletteForm.SetSettingsNotSaved();
            }

            _allNonOrnamentTextBoxes = GetNonOrnamentTextBoxes();
            _12OrnamentTextBoxes = Get12OrnamentTextBoxes();

            NumBasicChordDefsTextBox_Leave(NumBasicChordDefsTextBox, null);
        }

        private void ConnectBasicChordControl()
        {
            _bcc = new BasicChordControl(SetDialogState);
            _bcc.Location = new Point(0, 25);
            _bcc.BorderStyle = BorderStyle.None;
            this.TopPanel.Controls.Add(_bcc);
            TopPanel.TabIndex = 0;
            _bcc.TabIndex = 1;

            int rightMargin = _bcc.DurationsLabel.Location.X + _bcc.DurationsLabel.Size.Width;
            ReplaceLabel(_bcc.DurationsLabel, "relative durations", rightMargin);
            ReplaceLabel(_bcc.VelocitiesLabel, "velocity increments", rightMargin);
            ReplaceLabel(_bcc.MidiPitchesLabel, "transpositions", rightMargin);
            ReplaceLabel(_bcc.ChordDensitiesLabel, "note density factors", rightMargin);
        }

        private void ReplaceLabel(Label label, string newText, int rightMargin)
        {
            label.Text = newText;
            label.Location = new Point(rightMargin - label.Size.Width, label.Location.Y);
        }

        /************/

        private int ReadOrnamentSettingsForm(XmlReader r)
        {
            #region default values
            NumBasicChordDefsTextBox.Text = "";
            BankIndicesTextBox.Text = "";
            PatchIndicesTextBox.Text = "";
            NumberOfOrnamentsTextBox.Text = "";
            #endregion
            Debug.Assert(r.Name == "ornamentSettings");
            M.ReadToXmlElementTag(r, "numBasicChordDefs", "basicChord", "bankIndices", "patchIndices", "ornaments");

            while(r.Name == "numBasicChordDefs" || r.Name == "basicChord" || r.Name == "bankIndices" || r.Name == "patchIndices"
                || r.Name == "numOrnaments" || r.Name == "ornaments")
            {
                if(r.NodeType != XmlNodeType.EndElement)
                {
                    switch(r.Name)
                    {
                        case "numBasicChordDefs":
                            this.NumBasicChordDefsTextBox.Text = r.ReadElementContentAsString();
                            break;
                        case "basicChord":
                            _bcc.ReadBasicChordControl(r);
                            break;
                        case "bankIndices":
                            BankIndicesTextBox.Text = r.ReadElementContentAsString();
                            break;
                        case "patchIndices":
                            PatchIndicesTextBox.Text = r.ReadElementContentAsString();
                            break;
                        case "numOrnaments":
                            this.NumberOfOrnamentsTextBox.Text = r.ReadElementContentAsString();
                            break;
                        case "ornaments":
                            GetOrnaments(r);
                            break;
                    }
                }
                M.ReadToXmlElementTag(r, "ornamentSettings",
                    "numBasicChordDefs", "basicChord", "bankIndices", "patchIndices",
                    "numOrnaments", "ornaments");
            }
            Debug.Assert(r.Name == "ornamentSettings");
            return int.Parse(NumBasicChordDefsTextBox.Text);
        }

        private int GetOrnaments(XmlReader r)
        {
            int numberOfOrnaments = 0;
            Debug.Assert(r.Name == "ornaments");
            M.ReadToXmlElementTag(r, "ornament");
            while(r.Name == "ornament")
            {
                ++numberOfOrnaments;
                #region read each ornament
                switch(numberOfOrnaments)
                {
                    case 1:
                        Ornament1TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 2:
                        Ornament2TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 3:
                        Ornament3TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 4:
                        Ornament4TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 5:
                        Ornament5TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 6:
                        Ornament6TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 7:
                        Ornament7TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 8:
                        Ornament8TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 9:
                        Ornament9TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 10:
                        Ornament10TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 11:
                        Ornament11TextBox.Text = r.ReadElementContentAsString();
                        break;
                    case 12:
                        Ornament12TextBox.Text = r.ReadElementContentAsString();
                        break;
                }
                #endregion read each ornament
                M.ReadToXmlElementTag(r, "ornaments", "ornament");
            }
            Debug.Assert(r.Name == "ornaments");
            return numberOfOrnaments;
        }

        /************/

        private List<TextBox> GetNonOrnamentTextBoxes()
        {
            List<TextBox> textBoxes = new List<TextBox>();

            textBoxes.Add(NumBasicChordDefsTextBox);
            textBoxes.Add(_bcc.DurationsTextBox);
            textBoxes.Add(_bcc.VelocitiesTextBox);
            textBoxes.Add(_bcc.MidiPitchesTextBox);
            textBoxes.Add(_bcc.ChordOffsTextBox);
            textBoxes.Add(_bcc.ChordDensitiesTextBox);
            textBoxes.Add(_bcc.RootInversionTextBox);
            textBoxes.Add(_bcc.InversionIndicesTextBox);
            textBoxes.Add(_bcc.VerticalVelocityFactorsTextBox);
            textBoxes.Add(BankIndicesTextBox);
            textBoxes.Add(PatchIndicesTextBox);
            textBoxes.Add(NumberOfOrnamentsTextBox);

            return textBoxes;
        }

        /************/

        private List<TextBox> Get12OrnamentTextBoxes()
        {
            List<TextBox> textBoxes = new List<TextBox>();

            textBoxes.Add(this.Ornament1TextBox);
            textBoxes.Add(this.Ornament2TextBox);
            textBoxes.Add(this.Ornament3TextBox);
            textBoxes.Add(this.Ornament4TextBox);
            textBoxes.Add(this.Ornament5TextBox);
            textBoxes.Add(this.Ornament6TextBox);
            textBoxes.Add(this.Ornament7TextBox);
            textBoxes.Add(this.Ornament8TextBox);
            textBoxes.Add(this.Ornament9TextBox);
            textBoxes.Add(this.Ornament10TextBox);
            textBoxes.Add(this.Ornament11TextBox);
            textBoxes.Add(this.Ornament12TextBox);

            return textBoxes;
        }

        /************/

        #region TextBox_Leave handlers
        /************/
        private void NumBasicChordDefsTextBox_Leave(object sender, EventArgs e)
        {
            TextBox numBasicChordDefsTextBox = sender as TextBox;
            M.LeaveIntRangeTextBox(numBasicChordDefsTextBox, false, 1, 0, 12, SetDialogState);
            if(numBasicChordDefsTextBox.BackColor == M.TextBoxErrorColor)
            {
                DisableMainParameters();
            }
            else
            {
                this.EnableMainParameters();

                _numberOfBasicChordDefs = int.Parse(numBasicChordDefsTextBox.Text);
                _bcc.NumberOfChordValues = _numberOfBasicChordDefs;
                _bcc.SetHelpLabels();

                string valStr = (_numberOfBasicChordDefs == 1) ? " value" : " values";
                string inRangeStr = " in range [ 0..127 ]";
                BankIndicesHelpLabel.Text = _numberOfBasicChordDefs.ToString() + valStr + inRangeStr;
                PatchIndicesHelpLabel.Text = _numberOfBasicChordDefs.ToString() + valStr + inRangeStr;

                OrnamentDefRangeLabel.Text = "Each value in these ornament sequences must be in the range [ 1.." +
                    _numberOfBasicChordDefs.ToString() + " ].";

                TouchAllTextBoxes();
            }
        }
        private void DisableMainParameters()
        {
            _bcc.Enabled = false;

            _bcc.SetChordControls();

            BankIndicesTextBox.Enabled = false;
            BankIndicesTextBox.Enabled = false;
            BankIndicesTextBox.Enabled = false;

            PatchIndicesTextBox.Enabled = false;
            PatchIndicesTextBox.Enabled = false;
            PatchIndicesTextBox.Enabled = false;

            OrnamentsGroupBox.Enabled = false;
        }
        private void EnableMainParameters()
        {
            _bcc.Enabled = true;

            _bcc.SetChordControls();

            BankIndicesTextBox.Enabled = true;
            BankIndicesTextBox.Enabled = true;
            BankIndicesTextBox.Enabled = true;

            PatchIndicesTextBox.Enabled = true;
            PatchIndicesTextBox.Enabled = true;
            PatchIndicesTextBox.Enabled = true;

            OrnamentsGroupBox.Enabled = true;
        }
        public void TouchAllTextBoxes()
        {
            _bcc.NumberOfChordValues = _numberOfBasicChordDefs;
            _bcc.TouchAllTextBoxes();
            BankIndicesTextBox_Leave(BankIndicesTextBox, null);
            PatchIndicesTextBox_Leave(PatchIndicesTextBox, null);

            NumberOfOrnamentsTextBox_Leave(NumberOfOrnamentsTextBox, null);
            if(NumberOfOrnamentsTextBox.BackColor == M.TextBoxErrorColor)
            {
                DisableOrnamentTextBoxes();
            }
            else
            {
                EnableOrnamentTextBoxes();
            }
        }
        private void DisableOrnamentTextBoxes()
        {
            foreach(TextBox textBox in _12OrnamentTextBoxes)
            {
                textBox.Enabled = false;
            }
        }
        private void EnableOrnamentTextBoxes()
        {
            foreach(TextBox textBox in _12OrnamentTextBoxes)
            {
                textBox.Enabled = true;
            }
            TouchOrnamentTextBoxes();
        }
        private void TouchOrnamentTextBoxes()
        {
            for(int i = 1; i <= _ornaments.Count; ++i)
            {
                switch(i)
                {
                    case 1:
                        this.Ornament1TextBox_Leave(Ornament1TextBox, null);
                        break;
                    case 2:
                        this.Ornament2TextBox_Leave(Ornament2TextBox, null);
                        break;
                    case 3:
                        this.Ornament3TextBox_Leave(Ornament3TextBox, null);
                        break;
                    case 4:
                        this.Ornament4TextBox_Leave(Ornament4TextBox, null);
                        break;
                    case 5:
                        this.Ornament5TextBox_Leave(Ornament5TextBox, null);
                        break;
                    case 6:
                        this.Ornament6TextBox_Leave(Ornament6TextBox, null);
                        break;
                    case 7:
                        this.Ornament7TextBox_Leave(Ornament7TextBox, null);
                        break;
                    case 8:
                        this.Ornament8TextBox_Leave(Ornament8TextBox, null);
                        break;
                    case 9:
                        this.Ornament9TextBox_Leave(Ornament9TextBox, null);
                        break;
                    case 10:
                        this.Ornament10TextBox_Leave(Ornament10TextBox, null);
                        break;
                    case 11:
                        this.Ornament11TextBox_Leave(Ornament11TextBox, null);
                        break;
                    case 12:
                        this.Ornament12TextBox_Leave(Ornament12TextBox, null);
                        break;
                }
            }
        }
        /************/
        private void BankIndicesTextBox_Leave(object sender, EventArgs e)
        {
            M.LeaveIntRangeTextBox(sender as TextBox, true, (uint)_bcc.NumberOfChordValues, 0, 127, SetDialogState);
        }
        /************/
        private void PatchIndicesTextBox_Leave(object sender, EventArgs e)
        {
            M.LeaveIntRangeTextBox(sender as TextBox, true, (uint)_bcc.NumberOfChordValues, 0, 127, SetDialogState);
        }
        /************/
        private void NumberOfOrnamentsTextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, (uint)1, 1, 12, SetDialogState);
            _ornaments = new List<List<int>>();
            if(textBox.BackColor != M.TextBoxErrorColor)
            {
                int numberOfOrnaments = int.Parse(textBox.Text);

                for(int i = 0; i < numberOfOrnaments; ++i)
                {
                    _ornaments.Add(null);
                }
                TouchOrnamentTextBoxes();
                SetDialogSize(numberOfOrnaments);
            }
            _paletteForm.SetOrnamentControls();
        }
        private void SetDialogSize(int numberOfOrnaments)
        {
            OrnamentsGroupBox.Size = new Size(OrnamentsGroupBox.Size.Width, (46 + (numberOfOrnaments * 26)));
            this.Size = new Size(851,
                OrnamentsGroupBox.Location.Y + OrnamentsGroupBox.Size.Height + 2
                + BottomPanel.Size.Height + 42);

            #region set all ornamentGroupBox Control Locations
            // the following positions are reset here, in case the anchors change while editing)
            NumberOfOrnamentsLabel.Location = new Point(NumberOfOrnamentsLabel.Location.X, 22);
            NumberOfOrnamentsTextBox.Location = new Point(NumberOfOrnamentsTextBox.Location.X, 19);
            NumberOfOrnamentsHelpLabel.Location = new Point(NumberOfOrnamentsHelpLabel.Location.X, 22);
            int x = Ornament1TextBox.Location.X;
            Ornament1TextBox.Location = new Point(x, 45);
            Ornament2TextBox.Location = new Point(x, 71);
            Ornament3TextBox.Location = new Point(x, 97);
            Ornament4TextBox.Location = new Point(x, 123);
            Ornament5TextBox.Location = new Point(x, 149);
            Ornament6TextBox.Location = new Point(x, 175);
            Ornament7TextBox.Location = new Point(x, 201);
            Ornament8TextBox.Location = new Point(x, 227);
            Ornament9TextBox.Location = new Point(x, 253);
            Ornament10TextBox.Location = new Point(x, 279);
            Ornament11TextBox.Location = new Point(x, 305);
            Ornament12TextBox.Location = new Point(x, 331);
            x = OLabel1.Location.X;
            OLabel1.Location = new Point(x, 45 + 3);
            OLabel2.Location = new Point(x, 71 + 3);
            OLabel3.Location = new Point(x, 97 + 3);
            OLabel4.Location = new Point(x, 123 + 3);
            OLabel5.Location = new Point(x, 149 + 3);
            OLabel6.Location = new Point(x, 175 + 3);
            OLabel7.Location = new Point(x, 201 + 3);
            OLabel8.Location = new Point(x, 227 + 3);
            OLabel9.Location = new Point(x, 253 + 3);
            OLabel10.Location = new Point(x - 3, 279 + 3);
            OLabel11.Location = new Point(x - 3, 305 + 3);
            OLabel12.Location = new Point(x - 3, 331 + 3);
            #endregion

            for(int i = 0; i < _12OrnamentTextBoxes.Count; ++i)
            {
                if(i < numberOfOrnaments)
                {
                    _12OrnamentTextBoxes[i].Visible = true;
                }
                else
                {
                    _12OrnamentTextBoxes[i].Visible = false; // avoids shadow at bottom of group box
                }
            }

            BottomPanel.Location = new Point(BottomPanel.Location.X,
                OrnamentsGroupBox.Location.Y + OrnamentsGroupBox.Size.Height + 2);
        }
        /************/
        private void Ornament1TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 1);
        }
        private void Ornament2TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 2);
        }
        private void Ornament3TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 3);
        }
        private void Ornament4TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 4);
        }
        private void Ornament5TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 5);
        }
        private void Ornament6TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 6);
        }
        private void Ornament7TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 7);
        }
        private void Ornament8TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 8);
        }
        private void Ornament9TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 9);
        }
        private void Ornament10TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 10);
        }
        private void Ornament11TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 11);
        }
        private void Ornament12TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            M.LeaveIntRangeTextBox(textBox, false, uint.MaxValue, 1, _numberOfBasicChordDefs, SetDialogState);
            SetOrnaments(textBox, 12);
        }
        private void SetOrnaments(TextBox textBox, int ornamentNumber)
        {
            if(textBox.BackColor != M.TextBoxErrorColor)
            {
                Debug.Assert(_ornaments.Count >= ornamentNumber);
                _ornaments[ornamentNumber - 1] = M.StringToIntList(textBox.Text, ',');
            }
        }
        private void SetDialogState(TextBox textBox, bool okay)
        {
            SetSettingsNotSaved();

            if(okay)
            {
                textBox.BackColor = Color.White;
            }
            else
            {
                textBox.BackColor = M.TextBoxErrorColor;
            }
        }
        private void SetSettingsNotSaved()
        {
            if(!this.Text.EndsWith("*"))
                this.Text = this.Text + "*";
            _paletteForm.SetSettingsNotSaved();
        }
        #endregion

        /************/

        #region buttons
        private void ShowContainingPaletteButton_Click(object sender, EventArgs e)
        {
            if(_paletteForm.Enabled)
            {
                _paletteForm.BringToFront();
            }
            else
            {
                _paletteForm.BringPaletteChordFormToFront();
            }
        }
        private void ShowMainScoreFormButton_Click(object sender, EventArgs e)
        {
            _paletteForm.Callbacks.MainFormBringToFront();
        }
        #endregion buttons

        /************/

        #region public interface
        public void SetSettingsHaveBeenSaved()
        {
            if(this.Text.EndsWith("*"))
                this.Text = this.Text.Remove(this.Text.Length - 1);
        }
        public bool HasError
        {
            get
            {
                bool hasError = false;
                foreach(TextBox textBox in _allNonOrnamentTextBoxes)
                {
                    if(textBox.Enabled && textBox.BackColor == M.TextBoxErrorColor)
                    {
                        hasError = true;
                        break;
                    }
                }
                if(!hasError)
                {
                    for(int i = 0; i < _ornaments.Count; ++i)
                    {
                        if(_12OrnamentTextBoxes[i].BackColor == M.TextBoxErrorColor)
                        {
                            hasError = true;
                            break;
                        }
                    }
                }
                return hasError;
            }
        }
        public void WriteOrnamentSettingsForm(XmlWriter w)
        {
            w.WriteStartElement("ornamentSettings");

            Debug.Assert(!string.IsNullOrEmpty(NumBasicChordDefsTextBox.Text));
            w.WriteStartElement("numBasicChordDefs");
            w.WriteString(NumBasicChordDefsTextBox.Text);
            w.WriteEndElement();

            BasicChordControl.WriteBasicChordControl(w);

            if(!string.IsNullOrEmpty(this.BankIndicesTextBox.Text))
            {
                w.WriteStartElement("bankIndices");
                w.WriteString(BankIndicesTextBox.Text.Replace(" ", ""));
                w.WriteEndElement();
            }
            if(!string.IsNullOrEmpty(this.PatchIndicesTextBox.Text))
            {
                w.WriteStartElement("patchIndices");
                w.WriteString(PatchIndicesTextBox.Text.Replace(" ", ""));
                w.WriteEndElement();
            }
            Debug.Assert(!string.IsNullOrEmpty(NumberOfOrnamentsTextBox.Text));
            w.WriteStartElement("numOrnaments");
            w.WriteString(NumberOfOrnamentsTextBox.Text);
            w.WriteEndElement();
            w.WriteStartElement("ornaments");
            for(int i = 1; i <= _ornaments.Count; ++i)
            {
                #region write ornament elements
                w.WriteStartElement("ornament");
                switch(i)
                {
                    case 1:
                        w.WriteString(this.Ornament1TextBox.Text.Replace(" ", ""));
                        break;
                    case 2:
                        w.WriteString(this.Ornament2TextBox.Text.Replace(" ", ""));
                        break;
                    case 3:
                        w.WriteString(this.Ornament3TextBox.Text.Replace(" ", ""));
                        break;
                    case 4:
                        w.WriteString(this.Ornament4TextBox.Text.Replace(" ", ""));
                        break;
                    case 5:
                        w.WriteString(this.Ornament5TextBox.Text.Replace(" ", ""));
                        break;
                    case 6:
                        w.WriteString(this.Ornament6TextBox.Text.Replace(" ", ""));
                        break;
                    case 7:
                        w.WriteString(this.Ornament7TextBox.Text.Replace(" ", ""));
                        break;
                    case 8:
                        w.WriteString(this.Ornament8TextBox.Text.Replace(" ", ""));
                        break;
                    case 9:
                        w.WriteString(this.Ornament9TextBox.Text.Replace(" ", ""));
                        break;
                    case 10:
                        w.WriteString(this.Ornament10TextBox.Text.Replace(" ", ""));
                        break;
                    case 11:
                        w.WriteString(this.Ornament11TextBox.Text.Replace(" ", ""));
                        break;
                    case 12:
                        w.WriteString(this.Ornament12TextBox.Text.Replace(" ", ""));
                        break;
                }
                w.WriteEndElement(); // end of ornament
                #endregion
            }
            w.WriteEndElement(); // end of ornaments

            w.WriteEndElement(); // end of ornamentSettings
        }
        public BasicChordControl BasicChordControl { get { return _bcc; } }
        public List<List<int>> Ornaments { get { return _ornaments; } }
        #endregion

        #region private variables
        private BasicChordControl _bcc = null;
        private PaletteForm _paletteForm = null;
        private int _numberOfBasicChordDefs = -1;
        private List<TextBox> _allNonOrnamentTextBoxes;
        private List<TextBox> _12OrnamentTextBoxes;
        private List<List<int>> _ornaments = null;
        #endregion private variables
    }
}