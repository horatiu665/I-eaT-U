using UnityEngine;

namespace ProBuilder2.Common
{
	public struct pb_IntVec3 : System.IEquatable<pb_IntVec3>
	{
		public Vector3 vec;

		public float x { get { return vec.x; } }
		public float y { get { return vec.y; } }
		public float z { get { return vec.z; } }

		const float resolution = 100f;

		public pb_IntVec3(Vector3 vector)
		{
			this.vec = vector;
		}
		
		public bool Equals(pb_IntVec3 p)
		{
			return  round(x) == round(p.x) &&
					round(y) == round(p.y) &&
					round(z) == round(p.z);
		}

		public bool Equals(Vector3 p)
		{
			return  round(x) == round(p.x) &&
					round(y) == round(p.y) &&
					round(z) == round(p.z);
		}

		public override bool Equals(System.Object b)
		{
			return 	(b is pb_IntVec3 && ( this.Equals((pb_IntVec3)b) )) ||
					(b is Vector3 && this.Equals((Vector3)b));
		}

		public override int GetHashCode()
		{
			return round(x) ^ round(y) ^ round(z);
		}

		private int round(float v)
		{
			return (int) (v * resolution);
		}

		public static implicit operator Vector3(pb_IntVec3 p)
		{
			return p.vec;
		}

		public static implicit operator pb_IntVec3(Vector3 p)
		{
			return new pb_IntVec3(p);
		}
	}
}