﻿using System;
using System.Text;
using System.Diagnostics;

using Moritz.Globals;
using Moritz.Score.Midi;
using Moritz.Score.Notation;

namespace Moritz.Score
{
	internal class RestSymbol : DurationSymbol
	{
        public RestSymbol(Voice voice, IUniqueDef iumdd, int minimumCrotchetDurationMS, float fontHeight)
            : base(voice, iumdd.MsDuration, iumdd.MsPosition, minimumCrotchetDurationMS, fontHeight)
        {
            if(iumdd is UniqueCautionaryChordDef)
            {
                Console.WriteLine("rest is UniqueCautionaryChordDef!");
            }
            LocalCautionaryChordDef = iumdd as UniqueCautionaryChordDef;
        }

        public override void WriteSVG(SvgWriter w)
        {
            if(LocalCautionaryChordDef == null)
            {
                w.SvgStartGroup("rest" + SvgScore.UniqueID_Number);
                w.WriteAttributeString("score", "object", null, "rest");

                float alignmentX = (this.Metrics.Left + this.Metrics.Right) / 2;
                w.WriteAttributeString("score", "alignmentX", null, alignmentX.ToString(M.En_USNumberFormat));

                Debug.Assert(_msDuration > 0);
                w.WriteAttributeString("score", "msDuration", null, _msDuration.ToString());

                if(this.Metrics != null)
                    this.Metrics.WriteSVG(w);

                w.SvgEndGroup();
            }
        }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("rest   ");
			sb.Append(InfoString);
			return sb.ToString();
		}

		#region display attributes
        /// <summary>
        /// If LocalizedCautionaryChordDef is set:
        /// a) this rest is used like any other rest when justifying systems, but
        /// b) it is not displayed, and does not affect the temporal positions or durations of any chords. 
        /// </summary>
        public UniqueCautionaryChordDef LocalCautionaryChordDef = null;
		#endregion display attributes
		#region verticalPos attributes
		public bool Centered = false; // capella default
		public int Shift_Gap = 0; // capella default
		#endregion verticalPos attributes

        /// <summary>
        /// Returns this.Metrics cast to RestMetrics.
        /// Before accessing this property, this.Metrics must be assigned to an object of type RestMetrics.
        /// </summary>
        internal RestMetrics RestMetrics
        {
            get
            {
                RestMetrics restMetrics = Metrics as RestMetrics;
                Debug.Assert(restMetrics != null);
                return restMetrics;
            }
        }

        public RestDef MidiRestDef { get { return _midiRestDef; } }
        private RestDef _midiRestDef = null;


	}
}
