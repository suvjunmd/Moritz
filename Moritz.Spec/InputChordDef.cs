using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml;
using System.Text;

using Krystals4ObjectLibrary;
using Moritz.Globals;
using Moritz.Xml;

namespace Moritz.Spec
{
    ///<summary>
    /// A InputChordDef can be saved and retrieved from voices in an SVG file.
    /// Each inputChord in an SVG file will be given an ID of the form "inputChord"+uniqueNumber, but
    /// Neither the AssistantPerformer nor Moritz actually uses these ids.
    ///</summary>
    public class InputChordDef : DurationDef, IUniqueSplittableChordDef
    {
        /// <summary>
        /// Constructs a multi-note chord, each inputNoteDef has a notated pitch and a SeqDef.
		/// The inputNoteDefs must be in order of their notated pitches (bottom to top).
        /// </summary>
        public InputChordDef(int msPosition, int msDuration, List<InputNoteDef> inputNoteDefs)
            : base(msDuration)
        {
			#region check notated pitches and trkRef positions
			int pitchBelow = -1;
			foreach(InputNoteDef ind in inputNoteDefs)
			{
				Debug.Assert(ind.NotatedMidiPitch > pitchBelow);
				pitchBelow = ind.NotatedMidiPitch;

				if(ind.NoteOnTrkOns != null)
				{
					foreach(TrkOn trkOn in ind.NoteOnTrkOns)
					{ 
						Debug.Assert(msPosition <= trkOn.TrkMsPosition);
					}
				}
				if(ind.NoteOffTrkOns != null)
				{
					int minSeqPos = msPosition + msDuration;
					foreach(TrkOn trkRef in ind.NoteOffTrkOns)
					{
						Debug.Assert(minSeqPos <= trkRef.TrkMsPosition);
					}
				}
				// Note that there is no corresponding check for ind.NoteOnTrkOffs and ind.NoteOffTrkOffs
			}
			#endregion

            _msPosition = msPosition;
			_msDuration = msDuration;
			_inputNoteDefs = inputNoteDefs;
			_lyric = null;
			_inputControls = null;
			_msDurationToNextBarline = null;
        }

        /// <summary>
        /// Transpose the notatedPitches by the number of semitones given in the argument.
        /// Negative interval values transpose down.
        /// It is not an error if Midi values would exceed the range 0..127.
        /// In this case, they are silently coerced to 0 or 127 respectively.
        /// </summary>
        public void Transpose(int interval)
        {
			for(int i = 0; i < _inputNoteDefs.Count; ++i)
            {
				_inputNoteDefs[i].NotatedMidiPitch = M.MidiValue(_inputNoteDefs[i].NotatedMidiPitch + interval);
            }
        }

        public override string ToString()
        {
            return ("MsPosition=" + MsPosition.ToString() + " MsDuration=" + MsDuration.ToString() + " InputChordDef");
        }

        /// <summary>
        /// Multiplies the MsDuration by the given factor.
        /// </summary>
        /// <param name="factor"></param>
        public void AdjustMsDuration(double factor)
        {
            MsDuration = (int)(_msDuration * factor);
        }

        /// <summary>
        /// Writes the logical content of this InputChordDef
        /// </summary>
        /// <param name="w"></param>
        public void WriteSvg(SvgWriter w)
        {
			// we are inside a score:inputChord element

            w.WriteStartElement("score", "inputNotes", null);
			if(_inputControls != null)
			{
				_inputControls.WriteSvg(w);
			}			
			foreach(InputNoteDef ind in _inputNoteDefs)
			{
				ind.WriteSvg(w);
			}
			w.WriteEndElement(); // score:inputNotes
        }

        public override IUniqueDef DeepClone()
        {
            throw new NotImplementedException("InputChordDef.DeepClone()");
        }

        public int MsPosition { get { return _msPosition; } set { _msPosition = value; } }
        private int _msPosition = 0;

		public string Lyric { get { return _lyric; } set { _lyric = value; } }
		private string _lyric = null;

		public InputControls InputControls { get { return _inputControls; } set { _inputControls = value; } }
		private InputControls _inputControls = null;

		public List<InputNoteDef> InputNoteDefs { get {return _inputNoteDefs; }}
		private List<InputNoteDef> _inputNoteDefs = new List<InputNoteDef>();

		public List<byte> NotatedMidiPitches
		{
			get
			{
				List<byte> rList = new List<byte>();
				foreach(InputNoteDef ind in _inputNoteDefs)
				{
					rList.Add(ind.NotatedMidiPitch);
				}
				return rList;
			}
			// The new pitches must be in ascending order
			set
			{
				List<byte> newPitches = value;
				Debug.Assert(newPitches.Count == _inputNoteDefs.Count);
				int pitchBelow = -1;
				for(int i = 0; i < newPitches.Count; ++i)
				{
					byte newPitch = newPitches[i];
					Debug.Assert(newPitch > pitchBelow);
					pitchBelow = newPitch;
					_inputNoteDefs[i].NotatedMidiPitch = newPitch;
				}
			}
		}

        public int? MsDurationToNextBarline { get { return _msDurationToNextBarline; } set { _msDurationToNextBarline = value; } }
        private int? _msDurationToNextBarline = null;
    }
}
