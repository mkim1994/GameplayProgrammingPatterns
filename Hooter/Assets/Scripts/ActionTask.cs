using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionTask : Task {

	private readonly Action _action;

	public ActionTask(Action action){

		_action = action;
	}

	protected override void Init(){
		SetStatus (TaskStatus.Success);
		_action ();
	}
}
