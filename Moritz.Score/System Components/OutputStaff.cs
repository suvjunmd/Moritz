using System.Collections.Generic;
using System.Diagnostics;

namespace Moritz.Score
{
    public class OutputStaff : Staff
    {
        public OutputStaff(SvgSystem svgSystem, string staffName, int numberOfStafflines, float gap)
            : base(svgSystem, staffName, numberOfStafflines, gap)
        {
        }

        /// <summary>
        /// Writes out the stafflines and noteObjects of an OutputStaff.
        /// </summary>
        public override void WriteSVG(SvgWriter w, int pageNumber, int systemNumber, int staffNumber, PageFormat pageFormat)
        {
            w.SvgStartGroup("outputStaff", "p" + pageNumber.ToString() + "_sys" + systemNumber.ToString() + "_staff" + staffNumber.ToString());

            base.WriteSVG(w, pageNumber, systemNumber, staffNumber, pageFormat);

            w.SvgEndGroup(); // outputStaff
        }
    }
}
