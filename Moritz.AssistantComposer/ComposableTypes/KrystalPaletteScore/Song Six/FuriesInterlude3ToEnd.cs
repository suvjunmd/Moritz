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
            furies3.AdjustAlignments(furies1, furies2, furies4, clytemnestra);
            furies3.AdjustVelocities(msPositions);

            AdjustPostludePans(furies1, furies2, furies3, msPositions["postlude"]);
            SetFuriesFinalePitches(furies1, furies2, furies3, furies4, msPositions);
        }

        private void AdjustPostludePans(Furies1 furies1, Furies2 furies2, Furies3 furies3, int postludeMsPosition)
        {
            double posDiff = ((double)(furies1.EndMsPosition - postludeMsPosition)) / 4;
            int postludeMsPosition1 = postludeMsPosition + (int)posDiff;
            int postludeMsPosition2 = postludeMsPosition + (int)(posDiff * 2);
            int postludeMsPosition3 = postludeMsPosition + (int)(posDiff * 3);

            furies1.AdjustPostludePan(postludeMsPosition, postludeMsPosition1, postludeMsPosition2, postludeMsPosition3);
            furies2.AdjustPostludePan(postludeMsPosition, postludeMsPosition1, postludeMsPosition2, postludeMsPosition3);
            furies3.AdjustPostludePan(postludeMsPosition, postludeMsPosition1, postludeMsPosition2, postludeMsPosition3);
            // Furies 4 pans dont change
        }

        private void SetFuriesFinalePitches(VoiceDef furies1, VoiceDef furies2, VoiceDef furies3, VoiceDef furies4, 
            Dictionary<string, int> msPositions)
        {
            PermutationKrystal pk = new PermutationKrystal("C://Moritz/krystals/krystals/pk4(12)-2.krys");
            ExpansionKrystal xk = new ExpansionKrystal("C://Moritz/krystals/krystals/xk3(12.12.1)-1.krys");
            ExpansionKrystal pInputToxk = new ExpansionKrystal("C://Moritz/krystals/krystals/xk2(1.12.10)-1.krys");

            List<List<int>> pkValues = pk.GetValues(4);
            List<List<int>> xkValues = xk.GetValues(3);
            List<List<int>> pInputToxkValues = pInputToxk.GetValues(2);            
        }

    }
}
