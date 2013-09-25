﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

using Krystals4ObjectLibrary;

using Moritz.Score;
using Moritz.Score.Midi;

namespace Moritz.AssistantComposer
{
    class Winds
    {
        public Winds(List<Krystal> krystals, List<PaletteDef> paletteDefs, int nBaseChords)
        {
            MidiDefSequence baseMidiDefSequence = GetBaseMidiDefSequence(krystals[0], paletteDefs[0], nBaseChords);
            MidiDefSequences.Add(baseMidiDefSequence);

            MidiDefSequence tenorMidiDefSequence = GetTenorMidiDefSequence(baseMidiDefSequence);
            MidiDefSequences.Add(tenorMidiDefSequence);

            SetBaseWindIndexLyrics(baseMidiDefSequence);

            // etc. -- create and add other wind voices, so that wind channels are ordered bottom to top.
            // (MidiDefSequences[0] is the base Wind sequence.
        }

        private MidiDefSequence GetBaseMidiDefSequence(Krystal krystal, PaletteDef paletteDef, int nBaseChords)
        {
            List<List<int>> kValues = krystal.GetValues((uint)1);
            List<int> sequence = kValues[0]; // the flat list of values
            sequence = sequence.GetRange(0, nBaseChords);

            MidiDefSequence baseMidiDefSequence = new MidiDefSequence(paletteDef, sequence);

            baseMidiDefSequence.Transpose(-4);

            BaseWindKrystalStrandIndices = GetBaseWindStrandIndices(krystal, nBaseChords);

            return baseMidiDefSequence;
        }

        private MidiDefSequence GetTenorMidiDefSequence(MidiDefSequence baseMidiDefSequence)
        {
            MidiDefSequence tenorMidiDefSequence = baseMidiDefSequence.Clone();

            tenorMidiDefSequence.LocalizedMidiDurationDefs.Reverse();
            tenorMidiDefSequence.MsPosition = 0;
            tenorMidiDefSequence.Transpose(24);

            return tenorMidiDefSequence;
        }

        private List<int> GetBaseWindStrandIndices(Krystal krystal, int nBaseChords)
        {
            List<int> strandIndices = new List<int>();
            List<Strand> strands = krystal.Strands;
            int index = 0;
            foreach(Strand strand in strands)
            {
                if(index >= nBaseChords)
                {
                    break;
                }
                strandIndices.Add(index);
                index += strand.Values.Count;
            }
            return strandIndices;
        }

        private void SetBaseWindIndexLyrics(MidiDefSequence baseMidiDefSequence)
        {
            foreach(int index in BaseWindKrystalStrandIndices)
            {
                LocalMidiChordDef lmcd = baseMidiDefSequence[index].LocalMidiDurationDef as LocalMidiChordDef;
                if(lmcd != null)
                {
                    lmcd.Lyric = index.ToString();
                }
            }
        }

        public List<int> BaseWindKrystalStrandIndices = new List<int>();

        // each MidiDefSequence has the duration of the whole piece
        // The sequences are in bottom to top order. MidiDefSequences[0] is the base Wind
        public List<MidiDefSequence> MidiDefSequences = new List<MidiDefSequence>();

        // each voice has the duration of the whole piece
        public List<Voice> GetVoices(int topWindChannelIndex)
        {
            List<Voice> voices = new List<Voice>();
            int windChannelIndex = topWindChannelIndex;
            for(int i = MidiDefSequences.Count - 1; i >= 0; --i)
            {
                Voice voice = new Voice(null, (byte)(windChannelIndex++));
                voice.LocalizedMidiDurationDefs = MidiDefSequences[i].LocalizedMidiDurationDefs;
                voices.Add(voice);
            }
            return voices;
        }
    }
}
