﻿using System.Collections.Generic;
using System.Diagnostics;

namespace Moritz.Spec
{
	public class SeqDef
	{
		/// <param name="trkRefs"></param>
		/// <param name="inputControls">Can be null</param>
		public SeqDef(List<TrkRef> trkRefs, InputControls inputControls)
		{
			Debug.Assert(trkRefs != null && trkRefs.Count > 0);
			_inputControls = inputControls;
			_trkRefs = trkRefs;
		}

		internal void WriteSvg(Xml.SvgWriter w)
		{
			w.WriteStartElement("seq");
			foreach(TrkRef trkRef in _trkRefs)
			{
				trkRef.WriteSvg(w);
			}
			if(_inputControls != null)
			{
				_inputControls.WriteSvg(w);
			}
			w.WriteEndElement(); // seq
		}

		public List<TrkRef> TrkRefs { get{ return _trkRefs;} }
		private List<TrkRef> _trkRefs = new List<TrkRef>();

		private InputControls _inputControls;

	}
}
