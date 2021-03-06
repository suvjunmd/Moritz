﻿using System.Collections.Generic;

using Moritz.Xml;

namespace Moritz.Symbols
{
    /// <summary>
    /// AnchorageSymbols can have a list of attached DrawObjects.
    /// </summary>
    public abstract class AnchorageSymbol : NoteObject
    {
        public AnchorageSymbol(Voice voice)
            : base(voice)
        {
        }

        public AnchorageSymbol(Voice voice, float fontHeight)
            : base(voice, fontHeight)
        {
        }

        /// <summary>
        /// Returns the (positive) horizontal distance by which this anchorage symbol overlaps
        /// (any characters in) the previous noteObjectMoment (which contains symbols from both voices
        /// in a 2-voice staff). The result can be 0. If there is no overlap, the result is float.Minval.
        /// </summary>
        /// <param name="previousAS"></param>
        public virtual float OverlapWidth(NoteObjectMoment previousNOM)
        {
            float overlap = float.MinValue;
            float localOverlap = float.MinValue;
            foreach(AnchorageSymbol previousAS in previousNOM.AnchorageSymbols)
            {
                localOverlap = this.Metrics.OverlapWidth(previousAS);
                overlap = overlap > localOverlap ? overlap : localOverlap;
            }
            return overlap;
        }

        public List<DrawObject> DrawObjects { get { return _drawObjects; } set { _drawObjects = value; } }

        private List<DrawObject> _drawObjects = new List<DrawObject>();
      
        /// <summary>
        /// This field is set to true (while creating a MidiScore for performance) if a specific
        /// dynamic has been attached to this anchorageSymbol..
        /// </summary>
        public bool HasExplicitDynamic = false;
        /// <summary>
        /// Both rests, chords and the final barline have Velocity and ControlSymbols, so that hairpins etc. can be attached to them!
        /// </summary>
        public byte Velocity = 0;

        public void AddDynamic(byte midiVelocity, byte currentVelocity)
        {
            if(midiVelocity != currentVelocity)
            {
                string dynamicString = "";
                #region get dynamicString and _dynamic
                // note that cLicht has pppp and ffff, but these dynamics are not used here (in Study2)
                // These are the dynamicStrings for cLicht
                if(midiVelocity > 112F)
                {
                    dynamicString = "Ï";
                }
                else if(midiVelocity > 96)
                {
                    dynamicString = "ƒ";
                }
                else if(midiVelocity > 80)
                {
                    dynamicString = "f";
                }
                else if(midiVelocity > 64)
                {
                    dynamicString = "F";
                }
                else if(midiVelocity > 48)
                {
                    dynamicString = "P";
                }
                else if(midiVelocity > 32)
                {
                    dynamicString = "p";
                }
                else if(midiVelocity > 16)
                {
                    dynamicString = "π";
                }
                else
                {
                    dynamicString = "∏";
                }
                #endregion get dynamicString and _dynamic

				DynamicText dynamicText = new DynamicText(this, dynamicString, FontHeight);
                this._drawObjects.Add(dynamicText);
            }
        }  
    }
}
