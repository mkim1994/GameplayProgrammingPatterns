using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public abstract class ServiceLocator {
	//TaskManager
	private TaskManager _taskmanager;
	public TaskManager TaskManager{
		get{
			Debug.Assert (_taskmanager != null);
			return _taskmanager;
		}
		set { _taskmanager = value; }

	}

}*/

public static class Services {
	public static TaskManager TaskManager{get;set;}

	public static SceneManager<TransitionData> Scenes{get; set;}

}