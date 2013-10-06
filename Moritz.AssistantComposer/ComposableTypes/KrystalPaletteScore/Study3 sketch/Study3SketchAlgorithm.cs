﻿using System.Collections.Generic;
using System.Diagnostics;

using Moritz.Score;
using Moritz.Score.Midi;
using Krystals4ObjectLibrary;

namespace Moritz.AssistantComposer
{
    /// <summary>
    /// Algorithm for testing Song 6's palettes.
    /// This may develope as composition progresses...
    /// </summary>
    public class Study3SketchAlgorithm : MidiCompositionAlgorithm
    {
        /// <summary>
        /// This constructor can be called with both parameters null,
        /// just to get the overridden properties.
        /// </summary>
        public Study3SketchAlgorithm(List<Krystal> krystals, List<PaletteDef> paletteDefs)
            : base(krystals, paletteDefs)
        {
        }

        /// <summary>
        /// The values are checked for consistency in the base constructor.
        /// </summary>
        public override List<byte> MidiChannels()
        {
            return new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7 };
        }
        
        /// <summary>
        /// Sets the midi content of the score, independent of its notation.
        /// This means adding MidiDurationDefs to each voice's MidiDurationDefs list.
        /// The MidiDurations will later be transcribed into a particular notation by a Notator.
        /// Notations are independent of the midi info.
        /// This DoAlgorithm() function is special to this composition.
        /// </summary>
        /// <returns>
        /// A list of sequential bars. Each bar contains all the voices in the bar, from top to bottom.
        /// </returns>
        public override List<List<Voice>> DoAlgorithm()
        {
            List<List<Voice>> bars = new List<List<Voice>>();
            List<Voice> bar1 = CreateBar1();
            bars.Add(bar1);
            int bar2StartMsPos = GetEndMsPosition(bar1);
            List<Voice> bar2 = CreateBar2(bar2StartMsPos);
            bars.Add(bar2);
            int bar3StartMsPos = GetEndMsPosition(bar2);
            List<List<Voice>> bars3to5 = CreateBars3to5(bar3StartMsPos);
            foreach(List<Voice> bar in bars3to5)
            {
                bars.Add(bar);
            }

            Debug.Assert(bars.Count == NumberOfBars());

            return bars;
        }

        /// <summary>
        /// The number of bars produced by DoAlgorithm().
        /// </summary>
        /// <returns></returns>
        public override int NumberOfBars()
        {
            return 5;
        }

        #region CreateBar1()
        List<Voice> CreateBar1()
        {
            List<Voice> bar = new List<Voice>();

            byte channel = 0;
            foreach(PaletteDef midiDurationDefs in _paletteDefs)
            {
                Voice voice = new Voice(null, channel);
                bar.Add(voice);
                WriteVoiceMidiDurationDefs1(voice, midiDurationDefs);
                ++channel;
            }
            return bar;
        }

        private void WriteVoiceMidiDurationDefs1(Voice voice, PaletteDef midiDurationDefs)
        {
            int msPosition = 0;
            foreach(MidiDurationDef midiDurationDef in midiDurationDefs)
            {
                LocalMidiDurationDef noteDef = new LocalMidiDurationDef(midiDurationDef);
                LocalMidiDurationDef restDef = new LocalMidiDurationDef(1500 - midiDurationDef.MsDuration);
                Debug.Assert(midiDurationDef.MsDuration > 0);
                Debug.Assert(noteDef.MsDuration == midiDurationDef.MsDuration);
                noteDef.MsPosition = msPosition;
                msPosition += noteDef.MsDuration; // for this test score
                restDef.MsPosition = msPosition;
                msPosition += restDef.MsDuration; // for this test score
                voice.LocalMidiDurationDefs.Add(noteDef);
                voice.LocalMidiDurationDefs.Add(restDef);
            }
        }
        #endregion CreateBar1()

        #region CreateBar2()
        /// <summary>
        /// This function creates only one bar, but with MidiPhrase objects. 
        /// </summary>
        List<Voice> CreateBar2(int bar2StartMsPos)
        {
            List<Voice> bar = new List<Voice>();

            byte channel = 0;
            List<MidiPhrase> midiPhrases = new List<MidiPhrase>();
            foreach(PaletteDef paletteDef in _paletteDefs)
            {
                bar.Add(new Voice(null, channel));
                MidiPhrase midiPhrase = new MidiPhrase(paletteDef);
                midiPhrase.MsDuration = 6000;
                midiPhrases.Add(midiPhrase);
                ++channel;
            }
            int msPosition = bar2StartMsPos;
            int maxBarMsPos = 0;
            for(int i = 0; i < midiPhrases.Count; ++i)
            {
                int maxMsPos = WriteVoiceMidiDurationDefsInBar2(bar[i], midiPhrases[i], msPosition, bar2StartMsPos);
                maxBarMsPos = maxBarMsPos > maxMsPos ? maxBarMsPos : maxMsPos;
                msPosition += 1500;
            }

            // now add the final rest in the bar
            for(int i = 0; i < midiPhrases.Count; ++i)
            {
                int mdsdEndPos = midiPhrases[i].EndMsPosition;
                if(maxBarMsPos > mdsdEndPos)
                {
                    LocalMidiDurationDef rest2Def = new LocalMidiDurationDef(maxBarMsPos - mdsdEndPos);
                    rest2Def.MsPosition = mdsdEndPos;
                    bar[i].LocalMidiDurationDefs.Add(rest2Def);
                }
            }
            return bar;
        }

        /// <summary>
        /// Writes the first rest (if any) and the MidiPhrase to the voice.
        /// Returns the endMsPos of the MidiPhrase. 
        /// </summary>
        private int WriteVoiceMidiDurationDefsInBar2(Voice voice, MidiPhrase midiPhrase, int msPosition, int bar2StartMsPos)
        {
            LocalMidiDurationDef rest1Def = null;
            if(msPosition > bar2StartMsPos)
            {
                rest1Def = new LocalMidiDurationDef(msPosition - bar2StartMsPos);
                rest1Def.MsPosition = bar2StartMsPos;
                voice.LocalMidiDurationDefs.Add(rest1Def);
            }

            midiPhrase.MsPosition = msPosition;
            foreach(LocalMidiDurationDef lmdd in midiPhrase)
            {
                voice.LocalMidiDurationDefs.Add(lmdd);
            }

            return midiPhrase.EndMsPosition;
        }
        #endregion CreateBar2()

        #region CreateBars3to5()
        /// <summary>
        /// This function creates three bars, identical to bar2 with two internal barlines.
        /// The MidiPhrase objects cross barlines. 
        /// </summary>
        List<List<Voice>> CreateBars3to5(int bar3StartMsPos)
        {
            List<List<Voice>> bars = new List<List<Voice>>();
            List<Voice> threeBars = CreateBar2(bar3StartMsPos);

            //int bar4StartPos = bar3StartMsPos + 6000;
            int bar4StartPos = bar3StartMsPos + 5950;
            List<List<Voice>> bars3And4Plus5 = SplitBar(threeBars, bar4StartPos);
            int bar5StartPos = bar3StartMsPos + 10500;
            List<List<Voice>> bars4and5 = SplitBar(bars3And4Plus5[1], bar5StartPos);

            bars.Add(bars3And4Plus5[0]); // bar 3
            //bars.Add(bars3And4Plus5[1]); // bars 4 and 5
            bars.Add(bars4and5[0]); // bar 4
            bars.Add(bars4and5[1]); // bar 5
            return bars;
        }

        #endregion CreateBars3to5()
    }
}
