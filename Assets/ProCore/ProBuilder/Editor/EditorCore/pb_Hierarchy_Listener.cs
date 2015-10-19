﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using ProBuilder2.EditorCommon;
using System.Linq;
using ProBuilder2.Common;

namespace ProBuilder2.EditorCommon
{

	[InitializeOnLoad]
	/**
	 * Static delegates listen for hierarchy changes (duplication, delete, copy/paste) and rebuild the mesh components
	 * of pb_Objects if necessary.
	 */
	public class pb_Hierarchy_Listener : Editor
	{
		static pb_Hierarchy_Listener()
		{
			// When a prefab is updated, this is raised.  For some reason it's
			// called twice?
			EditorApplication.hierarchyWindowChanged += HierarchyWindowChanged;

			// prefabInstanceUpdated is not called when dragging out of Project view,
			// or when creating a prefab or reverting.  OnHierarchyChange captures those.
			PrefabUtility.prefabInstanceUpdated -= PrefabInstanceUpdated;
			PrefabUtility.prefabInstanceUpdated += PrefabInstanceUpdated;
		}
		
		~pb_Hierarchy_Listener()
		{
			PrefabUtility.prefabInstanceUpdated -= PrefabInstanceUpdated;
			EditorApplication.hierarchyWindowChanged -= HierarchyWindowChanged;
		}

		static void PrefabInstanceUpdated(GameObject go)
		{
			if(EditorApplication.isPlayingOrWillChangePlaymode)
				return;

			foreach(pb_Object pb in go.GetComponentsInChildren<pb_Object>())
			{
				pb.ToMesh();
				pb.Refresh();
				pb.Optimize();
			}
		}

		/**
		 * Used to catch prefab modifications that otherwise wouldn't be registered on the usual 'Awake' verify.
		 *	- Dragging prefabs out of Project
		 *	- 'Apply' prefab changes
		 */
		static void HierarchyWindowChanged()
		{
			bool prefabReverted = false;

			if(!EditorApplication.isPlaying)
			{
				// on duplication, or copy paste, this rebuilds the mesh structures of the new objects
				foreach(pb_Object pb in Selection.transforms.GetComponents<pb_Object>())
				{
					if( pb_Editor_Utility.VerifyMesh(pb) != MeshRebuildReason.None )
					{
						prefabReverted = true;
					}
				}

				if(prefabReverted)
				{
					if(pb_Editor.instance != null)
						pb_Editor.instance.UpdateSelection();
				}
			}
		}
	}
}