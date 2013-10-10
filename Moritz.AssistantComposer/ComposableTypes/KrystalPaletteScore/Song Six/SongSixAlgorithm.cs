﻿using System.Collections.Generic;
using System.Diagnostics;
using System;

using Moritz.Score;
using Moritz.Score.Midi;
using Krystals4ObjectLibrary;

namespace Moritz.AssistantComposer
{
    /// <summary>
    /// The Algorithm for Song 6.
    /// This will develope as composition progresses...
    /// </summary>
    internal class SongSixAlgorithm : MidiCompositionAlgorithm
    {
        public SongSixAlgorithm(List<Krystal> krystals, List<PaletteDef> paletteDefs)
            : base(krystals, paletteDefs)
        {
        }

        /// <summary>
        /// The values are then checked for consistency in the base constructor.
        /// </summary>
        public override List<byte> MidiChannels()
        {
            return new List<byte>() { 0, 1, 2, 3, 4 };
        }

        /// <summary>
        /// The number of bars produced by DoAlgorithm().
        /// </summary>
        /// <returns></returns>
        public override int NumberOfBars()
        {
            return 93;
        }

        /// <summary>
        /// Sets the midi content of the score, independent of its notation.
        /// This means adding LocalMidiDurationDefs to each VoiceDef's LocalMidiDurationDefs list.
        /// The LocalMidiDurationDefs will later be transcribed into a particular notation by a Notator.
        /// Notations are independent of the midi info.
        /// This DoAlgorithm() function is special to this composition.
        /// </summary>
        /// <returns>
        /// A list of sequential bars. Each bar contains all the voices in the bar, from top to bottom.
        /// </returns>
        public override List<List<Voice>> DoAlgorithm()
        {
            int tempInterludeMsDuration = int.MaxValue / 50;
            Clytemnestra clytemnestra = new Clytemnestra(tempInterludeMsDuration);
            // The wind3 is the lowest wind. The winds are numbered from top to bottom in the score.
            VoiceDef wind3 = new VoiceDef(_paletteDefs[0], _krystals[2]);
            wind3.Transpose(-13);

            AlignClytemnestraToRootWind(clytemnestra, wind3, tempInterludeMsDuration);

            VoiceDef wind2 = GetWind2(wind3, clytemnestra.LocalMidiDurationDefs[59].MsPosition);
            VoiceDef wind1 = GetWind1(wind3, clytemnestra.LocalMidiDurationDefs[116].MsPosition);
            
            // Complete the winds and birds.

            VoiceDef control = GetControlVoiceDef(clytemnestra, wind1, wind2, wind3);

            #region code for testing VoiceDef functions
            //bassWind.SetContour(11, new List<int>() { 1, 4, 1, 2 }, 1, 1);
            //bassWind.Translate(15, 4, 16);
            // TODO:
            // Cut, Copy, PasteAt (List<LocalMididurationDefs>) !!
            #endregion

            // Add each voiceDef to voiceDefs here, in top to bottom (=channelIndex) order in the score.
            List<VoiceDef> voiceDefs = new List<VoiceDef>() {control, clytemnestra, wind1, wind2, wind3 /* etc.*/};
            Debug.Assert(voiceDefs.Count == MidiChannels().Count);
            foreach(VoiceDef voiceDef in voiceDefs)
            {
                voiceDef.SetLyricsToIndex();
            }
            List<int> barlineMsPositions = GetBarlineMsPositions(control, clytemnestra, wind1, wind2, wind3 /* etc.*/);
            // this system contains one Voice per channel (not divided into bars)
            List<Voice> system = GetVoices(voiceDefs);
            List<List<Voice>> bars = GetBars(system, barlineMsPositions);

            return bars;
        }

        /// <summary>
        /// Aligns Clytemnestra's verses to MsPositions in the rootWind (the lowest wind)
        /// </summary>
        /// <param name="clytemnestra"></param>
        /// <param name="rootWind"></param>
        /// <param name="tempInterludeMsDuration"></param>
        private void AlignClytemnestraToRootWind(Clytemnestra clytemnestra, VoiceDef rootWind, int tempInterludeMsDuration)
        {
            List<int> verseMsPositions = new List<int>();
            verseMsPositions.Add(rootWind[8].MsPosition);
            verseMsPositions.Add(rootWind[20].MsPosition);
            verseMsPositions.Add(rootWind[33].MsPosition);
            verseMsPositions.Add(rootWind[49].MsPosition);
            verseMsPositions.Add(rootWind[70].MsPosition);

            List<LocalMidiDurationDef> clmdds = clytemnestra.LocalMidiDurationDefs;
            clmdds[0].MsDuration = verseMsPositions[0];
            clmdds[0].UniqueMidiDurationDef.MsDuration = clmdds[0].MsDuration;
            clytemnestra.MsPosition = 0; // sets all the positions
            for(int verse = 2; verse <= 5; ++verse)
            {
                int interludeIndex = clytemnestra.LocalMidiDurationDefs.FindIndex(
                    x => (x.UniqueMidiDurationDef is UniqueMidiRestDef 
                          && x.MsDuration == tempInterludeMsDuration));
                clmdds[interludeIndex].MsDuration = verseMsPositions[verse - 1] - clmdds[interludeIndex].MsPosition;
                clmdds[interludeIndex].UniqueMidiDurationDef.MsDuration = clmdds[interludeIndex].MsDuration;
                clytemnestra.MsPosition = 0; // sets all the positions
            }
            int lastIndex = clmdds.Count - 1;
            clmdds[lastIndex].MsDuration = rootWind.EndMsPosition - clmdds[lastIndex].MsPosition;
            clmdds[lastIndex].UniqueMidiDurationDef.MsDuration = clmdds[lastIndex].MsDuration;
        }

        /// <summary>
        /// The returned barlineMsPositions contain both the position of bar 1 (0ms) and the position of the final barline.
        /// </summary>
        private List<int> GetBarlineMsPositions(VoiceDef control, Clytemnestra clytemnestra, VoiceDef altoWind, VoiceDef tenorWind, VoiceDef bassWind /* etc.*/)
        {
            VoiceDef ctl = control;
            Clytemnestra c = clytemnestra;
            VoiceDef aw = altoWind;
            VoiceDef tw = tenorWind;
            VoiceDef bw = bassWind;

            List<int> barlineMsPositions = new List<int>()
            {
                #region msPositions
                #region intro
                0,
                bw[1].MsPosition,
                bw[3].MsPosition,
                bw[5].MsPosition,
                #endregion
                #region verse 1
                c[1].MsPosition,
                c[3].MsPosition,
                c[8].MsPosition,
                c[12].MsPosition,
                c[15].MsPosition,
                c[18].MsPosition,
                c[22].MsPosition,
                c[27].MsPosition,
                c[34].MsPosition,
                c[38].MsPosition,
                c[41].MsPosition,
                c[47].MsPosition,
                c[49].MsPosition,
                c[50].MsPosition,
                c[54].MsPosition,
                c[58].MsPosition,
                #endregion
                #region interlude after verse 1
                c[59].MsPosition,
                bw[15].MsPosition,
                #endregion
                #region verse 2
                c[60].MsPosition,
                c[62].MsPosition,
                c[67].MsPosition,
                c[71].MsPosition,
                c[73].MsPosition,
                c[77].MsPosition,
                c[81].MsPosition,
                c[86].MsPosition,
                c[88].MsPosition,
                c[92].MsPosition,
                c[94].MsPosition,
                c[97].MsPosition,
                c[100].MsPosition,
                c[104].MsPosition,
                c[107].MsPosition,
                c[111].MsPosition,
                c[115].MsPosition,
                #endregion
                #region interlude after verse 2
                c[116].MsPosition,
                bw[27].MsPosition,
                #endregion
                #region verse 3
                c[117].MsPosition,
                c[119].MsPosition,
                c[124].MsPosition,
                c[126].MsPosition,
                c[128].MsPosition,
                c[131].MsPosition,
                c[135].MsPosition,
                c[139].MsPosition,
                c[141].MsPosition,
                c[146].MsPosition,
                c[148].MsPosition,
                c[152].MsPosition,
                c[159].MsPosition,
                c[164].MsPosition,
                c[168].MsPosition,
                c[172].MsPosition,
                #endregion
                #region interlude after verse 3
                c[173].MsPosition,
                bw[40].MsPosition,
                bw[45].MsPosition,
                #endregion
                #region verse 4, Oft have ye...
                c[174].MsPosition,
                c[177].MsPosition,
                c[183].MsPosition,
                c[185].MsPosition,
                c[192].MsPosition,
                c[196].MsPosition,
                c[204].MsPosition,
                c[206].MsPosition,
                c[214].MsPosition,
                c[219].MsPosition,
                c[221].MsPosition,
                c[225].MsPosition,
                c[227].MsPosition,
                c[229].MsPosition,
                c[233].MsPosition,
                c[236].MsPosition,
                c[242].MsPosition,
                c[252].MsPosition,
                c[257].MsPosition,
                c[259].MsPosition,
                c[263].MsPosition,
                c[267].MsPosition,
                #endregion
                #region interlude after verse 4
                c[268].MsPosition,
                bw[63].MsPosition,
                #endregion
                #region verse 5
                c[269].MsPosition,
                c[270].MsPosition,
                c[272].MsPosition,
                c[276].MsPosition,
                c[279].MsPosition,
                c[283].MsPosition,
                c[288].MsPosition,
                #endregion
                #region interlude after verse 5 (finale)
                c[289].MsPosition,
                bw[77].MsPosition,
                #endregion
                // final barline
                bw.EndMsPosition
                #endregion
            };

            Debug.Assert(barlineMsPositions.Count == NumberOfBars() + 1); // includes bar 1 (mPos=0) and the final barline.

            return barlineMsPositions;
        }

        /// <summary>
        /// These barlines do not include the barlines at the beginning, middle or end of Clytemnestra's verses.
        /// </summary>
        /// <param name="bassWind"></param>
        /// <param name="barlineMsPositions"></param>
        /// <returns></returns>
        private List<int> AddInterludeBarlinePositions(VoiceDef bassWind, List<int> barlineMsPositions)
        {
            List<int> newBarlineIndices = new List<int>() { 1, 3, 5, 15, 27, 40, 45, 63, 77 }; // by inspection of the score
            foreach(int index in newBarlineIndices)
            {
                barlineMsPositions.Add(bassWind.LocalMidiDurationDefs[index].MsPosition);
            }
            barlineMsPositions.Sort();

            return barlineMsPositions;
        }

        private VoiceDef GetWind2(VoiceDef wind3, int rotationMsPosition)
        {
            VoiceDef wind2 = GetRotatedWind(wind3, rotationMsPosition);
            wind2.Transpose(19); // the basic pitch
            wind2.AlignObjectAtIndex(0, 14, 82, rotationMsPosition);

            return wind2;
        }

        private VoiceDef GetWind1(VoiceDef bassWind, int rotationMsPosition)
        {
            VoiceDef wind1 = GetRotatedWind(bassWind, rotationMsPosition);
            wind1.Transpose(31); // the basic pitch
            wind1.AlignObjectAtIndex(0, 24, 82, rotationMsPosition);

            return wind1;
        }
        /// <summary>
        /// Returns a VoiceDef containing clones of the LocalMidiDurationDefs in the originalVoiceDef argument,
        /// rotated so that the original first LocalMidiDurationDef is positioned close to rotationMsPosition.
        /// </summary>
        /// <param name="originalVoiceDef"></param>
        /// <returns></returns>
        private VoiceDef GetRotatedWind(VoiceDef originalVoiceDef, int rotationMsPosition)
        {
            VoiceDef tempWind = originalVoiceDef.Clone();
            int finalBarlineMsPosition = originalVoiceDef.EndMsPosition;
            int msDurationAfterSynch = finalBarlineMsPosition - rotationMsPosition;

            List<LocalMidiDurationDef> originalLmdds = tempWind.LocalMidiDurationDefs;
            List<LocalMidiDurationDef> originalStartLmdds = new List<LocalMidiDurationDef>();
            List<LocalMidiDurationDef> newWindLmdds = new List<LocalMidiDurationDef>();
            int accumulatingMsDuration = 0;
            for(int i = 0; i < tempWind.Count; ++i)
            {
                if(accumulatingMsDuration < msDurationAfterSynch)
                {
                    originalStartLmdds.Add(originalLmdds[i]);
                    accumulatingMsDuration += originalLmdds[i].MsDuration;
                }
                else
                {
                    newWindLmdds.Add(originalLmdds[i]);
                }
            }
            newWindLmdds.AddRange(originalStartLmdds);

            int msPosition = 0;
            foreach(LocalMidiDurationDef lmdd in newWindLmdds)
            {
                lmdd.MsPosition = msPosition;
                msPosition += lmdd.MsDuration;
            }
            VoiceDef newRotatedWind = new VoiceDef(newWindLmdds);

            return newRotatedWind;
        }

        /// <summary>
        /// The control VoiceDef consists of single note + rest pairs,
        /// whose msPositions and msDurations are composed here.
        /// </summary>
        private VoiceDef GetControlVoiceDef(Clytemnestra clytemnestra, VoiceDef wind1, VoiceDef wind2, VoiceDef wind3)
        {
            VoiceDef w3 = wind3;
            VoiceDef c = clytemnestra;
            // The control note msPositions and following rest msDurations.
            // The columns here are note MsPositions and rest MsDurations respectively.
            // A rest's MsPosition is found by subtracting its MsDuration from the following note msPosition.
            List<int> controlNoteAndRestInfo = new List<int>()
            {
                #region positions (in temporal order)
                #region introduction
                0, 100,
                w3[1].MsPosition, 100,
                w3[3].MsPosition, 100,
                w3[5].MsPosition, 100,
                #endregion
                #region verse 1
                c[1].MsPosition,  100,
                c[3].MsPosition,  100,
                c[7].MsPosition,  100,
                c[14].MsPosition, 100,
                c[17].MsPosition, 100,
                c[24].MsPosition, 100,
                c[31].MsPosition, 100,
                c[40].MsPosition, 100,
                c[49].MsPosition, 100,
                #endregion
                #region interlude after verse 1
                c[59].MsPosition,  100,
                w3[15].MsPosition, 100,
                #endregion
                #region verse 2
                c[60].MsPosition, 100,
                c[62].MsPosition, 100,
                c[66].MsPosition, 100,
                c[83].MsPosition, 100,
                c[94].MsPosition, 100,
                c[99].MsPosition, 100,
                #endregion
                #region interlude after verse 2
                c[106].MsPosition, 100,
                c[116].MsPosition, 100,
                w3[27].MsPosition, 100,
                #endregion
                #region verse 3
                c[117].MsPosition, 100,
                c[119].MsPosition, 100,
                c[123].MsPosition, 100,
                c[130].MsPosition, 100,
                c[141].MsPosition, 100,
                c[152].MsPosition, 100,
                c[163].MsPosition, 100,
                #endregion
                #region interlude after verse 3
                c[173].MsPosition, 100,
                w3[40].MsPosition, 100,
                w3[45].MsPosition, 100,
                #endregion
                #region verse 4
                c[174].MsPosition, 100,
                c[185].MsPosition, 100,
                c[216].MsPosition, 100,
                c[235].MsPosition, 100,
                c[255].MsPosition, 100,
                #endregion
                #region interlude after verse 4
                c[268].MsPosition, 100,
                w3[63].MsPosition, 100,
                #endregion
                #region verse 5
                c[269].MsPosition, 100,
                c[278].MsPosition, 100,
                c[288].MsPosition, 100,
                #endregion
                #region finale
                c[289].MsPosition, 100,
                w3[77].MsPosition, 400,
                #endregion
                w3.EndMsPosition // final barline position
                #endregion
            };

            #region check consistency of controlNoteAndRestInfo
            for(int i = 0; i < controlNoteAndRestInfo.Count - 3; i += 2)
            {
                int noteMsPosition = controlNoteAndRestInfo[i];
                int restMsDuration = controlNoteAndRestInfo[i + 1];
                int nextNoteMsPosition = controlNoteAndRestInfo[i + 2];
                int restMsPosition = nextNoteMsPosition - restMsDuration;
                int noteMsDuration = restMsPosition - noteMsPosition;

                Debug.Assert(nextNoteMsPosition > noteMsPosition);
                Debug.Assert(restMsPosition > noteMsPosition);
                Debug.Assert(noteMsDuration > 0 && restMsDuration > 0);
            }
            #endregion

            List<LocalMidiDurationDef> controlLmdds = new List<LocalMidiDurationDef>();

            for(int i = 0; i < controlNoteAndRestInfo.Count - 2; i += 2)
            {
                int noteMsPosition = controlNoteAndRestInfo[i];
                int restMsDuration = controlNoteAndRestInfo[i + 1]; 
                int nextNoteMsPosition = controlNoteAndRestInfo[i + 2];
                int restMsPosition = nextNoteMsPosition - restMsDuration;
                int noteMsDuration = restMsPosition - noteMsPosition;
                
                UniqueMidiChordDef umcd = new UniqueMidiChordDef(new List<byte>() { (byte)67 }, new List<byte>() { (byte)0 }, noteMsDuration, false, new List<MidiControl>());
                LocalMidiDurationDef lmChordd = new LocalMidiDurationDef(umcd, noteMsPosition, noteMsDuration);

                LocalMidiDurationDef lmRestd = new LocalMidiDurationDef(restMsDuration);
                lmRestd.MsPosition = restMsPosition;

                controlLmdds.Add(lmChordd);
                controlLmdds.Add(lmRestd);
            }
            VoiceDef controlVoiceDef = new VoiceDef(controlLmdds);

            return controlVoiceDef;
        }

        private List<Voice> GetVoices(List<VoiceDef> voiceDefs)
        {
            byte channelIndex = 0;
            List<Voice> voices = new List<Voice>();

            foreach(VoiceDef voiceDef in voiceDefs)
            {
                Voice voice = new Voice(null, channelIndex++);
                voice.LocalMidiDurationDefs = voiceDef.LocalMidiDurationDefs;
                voices.Add(voice);
            }

            return voices;
        }

        private List<List<Voice>> GetBars(List<Voice> system, List<int> barlineMsPositions)
        {
            // barlineMsPositions contains both msPos=0 and the position of the final barline
            List<List<Voice>> bars = new List<List<Voice>>();
            bars = GetBarsFromBarlineMsPositions(system, barlineMsPositions);
            Debug.Assert(bars.Count == NumberOfBars());
            return bars;
        }

        /// <summary>
        /// Splits the voices (currently in a single bar) into bars
        /// barlineMsPositions contains both msPosition 0, and the position of the final barline.
        /// </summary>
        private List<List<Voice>> GetBarsFromBarlineMsPositions(List<Voice> voices, List<int> barLineMsPositions)
        {
            List<List<Voice>> bars = new List<List<Voice>>();
            List<List<Voice>> twoBars = null;

            for(int i = barLineMsPositions.Count - 2; i >= 1; --i)
            {
                twoBars = SplitBar(voices, barLineMsPositions[i]);
                bars.Insert(0, twoBars[1]);
                voices = twoBars[0];
            }
            bars.Insert(0, twoBars[0]);

            return bars;
        }
    }
}
