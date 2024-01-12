using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SimpleInject
{
	public sealed class ProjectContext : AContext
	{
		private const string ProjectContextPrefab = "SimpleInject/ProjectContext";
		
		private static ProjectContext _instance;
		
		public override ContextScope Scope => ContextScope.Project;

		public static ProjectContext Instance
		{
			get
			{
				if (_instance == null) 
					Instantiate();

				return _instance;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			
			DontDestroyOnLoad(this);
		}

		private static void Instantiate()
		{
			var prefab = Resources.Load<ProjectContext>(ProjectContextPrefab);
			if (prefab == null)
				throw new Exception("Prefab 'ProjectContext' not found in Resources folder!");

			_instance = Object.Instantiate(prefab, null, false);
			_instance.name = "~Project Context~";
		}
	}
}