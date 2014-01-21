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
    internal partial class SongSixAlgorithm : MidiCompositionAlgorithm
    {
        /// <summary>
        /// The arguments are all complete to the end of Verse 3
        /// </summary>
        private void GetFuriesInterlude3ToEnd(Furies1 furies1, Furies2 furies2, Furies3 furies3, Furies4 furies4,
            Clytemnestra clytemnestra, VoiceDef wind1, VoiceDef wind2, VoiceDef wind3, List<PaletteDef> palettes,
            Dictionary<string, int> msPositions)
        {
            furies1.GetFinale(palettes, msPositions);
            furies1.AdjustAlignments(clytemnestra, wind2, wind3);
            furies1.AdjustVelocities(msPositions);

            msPositions.Add("furies2FinaleStart", furies1[47].MsPosition);
            msPositions.Add("furies2FinalePart2Start", wind1[54].MsPosition);
            msPositions.Add("finalBar", furies1[280].MsPosition);

            furies4.GetFinale(palettes, msPositions);
            furies4.AdjustAlignments(furies1, clytemnestra, wind3);
            furies4.AdjustVelocities(msPositions);

            furies2.GetFinale(palettes, msPositions);
            furies2.AdjustAlignments(furies1, furies4, clytemnestra);
            furies2.AdjustVelocities(msPositions);

            msPositions.Add("furies3FinaleStart", furies2[66].MsPosition);

            furies3.GetFinale(palettes, msPositions);
            furies3.AdjustAlignments(furies1, furies2, furies4, clytemnestra, wind1);
            furies3.AdjustVelocities(msPositions);

            AdjustPostludePans(furies1, furies2, furies3, furies4, msPositions["postlude"]);
            SetFuriesFinalePitches(furies1, furies2, furies3, furies4, msPositions);
        }

        private void AdjustPostludePans(Furies1 furies1, Furies2 furies2, Furies3 furies3, Furies4 furies4, int postludeMsPosition)
        {
            double posDiff = ((double)(furies1.EndMsPosition - postludeMsPosition)) / 4;
            int postludeMsPosition1 = postludeMsPosition + (int)posDiff;
            int postludeMsPosition2 = postludeMsPosition + (int)(posDiff * 2);
            int postludeMsPosition3 = postludeMsPosition + (int)(posDiff * 3);

            furies1.AdjustPostludePan(postludeMsPosition, postludeMsPosition1, postludeMsPosition2, postludeMsPosition3);
            furies2.AdjustPostludePan(postludeMsPosition, postludeMsPosition1, postludeMsPosition2, postludeMsPosition3);
            furies3.AdjustPostludePan(postludeMsPosition, postludeMsPosition1, postludeMsPosition2, postludeMsPosition3);
            furies4.AdjustPostludePan(postludeMsPosition, postludeMsPosition1, postludeMsPosition2, postludeMsPosition3);
        }

        private void SetFuriesFinalePitches(Furies1 furies1, Furies2 furies2, Furies3 furies3, Furies4 furies4, 
            Dictionary<string, int> msPositions)
        {
            Dictionary<int, int> msPosTranspositionDict = furies4.SetFinalMelody();

            furies1.TransposeToDict(msPosTranspositionDict);
            furies2.TransposeToDict(msPosTranspositionDict);
            furies3.TransposeToDict(msPosTranspositionDict);
        }

    }
}