﻿#region Copyright & License Information
/*
 * Copyright 2007-2011 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made 
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using System.Linq;
using OpenRA.Traits;

namespace OpenRA.Mods.RA
{
	public class ProximityCaptorInfo : ITraitInfo
	{
		public readonly string[] Types = {};
		public object Create(ActorInitializer init) { return new ProximityCaptor(this); }
	}

	public class ProximityCaptor
	{
		public readonly ProximityCaptorInfo Info;

		public string[] Types;

		[Sync]
		public int Hash { get { return string.Join(",", Types).GetHashCode(); } }

		public ProximityCaptor(ProximityCaptorInfo info)
		{
			Info = info;

			Types = info.Types.Select(t => t.ToLowerInvariant()).OrderBy(t => t).ToArray();
		}

		public bool HasAny(string[] typesList)
		{
			return typesList.Select(t => t.ToLowerInvariant()).Any(flag => Types.Contains(flag));
		}

		public bool HasAll(string[] typesList)
		{
			return typesList.Select(t => t.ToLowerInvariant()).All(flag => Types.Contains(flag));
		}
	}
}