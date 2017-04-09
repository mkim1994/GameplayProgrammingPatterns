using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree{

	//reports success or failure when it's updated. thus, bool.
	//takes a context variable so that leaf nodes know what to operate on.
	public abstract class Node<T>{
		public abstract bool Update(T context);
	}

	//tree is just a special node that contains the "root." The root is where the execution of the tree starts.
	//trees are just regular nodes, so they can just be added to other trees as subtrees.
	public class Tree<T> : Node<T>{
		private readonly Node<T> _root;

		public Tree(Node<T> root){
			_root = root;
		}

		public override bool Update(T context){
			return _root.Update(context);
		}

	}

	/* NODE TYPES */

	//action nodes; the leaves. Where the tree reaches a decision of what to do/performs some aspect of the behavior
	//game-specific actions, so there will most likely be subclasses for these
	public class Do<T> : Node<T>{
		public delegate bool NodeAction(T context);
		private readonly NodeAction _action;
		public Do(NodeAction action){
			_action = action;
		}
		public override bool Update(T context){
			return _action (context);
		}
	}

	//condition; leaf nodes that test something
	public class Condition<T> : Node<T>{
		private readonly Predicate<T> _condition;
		public Condition(Predicate<T> condition){
			_condition = condition;
		}
		public override bool Update(T context){
			return _condition (context);
		}
	}

	//decision nodes
	//inner node: has a set of child or branch nodes. these are nodes that define structure/logic
	//these nodes don't actually "do" anything
	public abstract class BranchNode<T> : Node<T>{
		protected Node<T>[] Children { get; private set; }
		protected BranchNode(params Node<T>[] children){
			Children = children;
		}
	}

	//selector
	//succeeds when ONE of its children succeeds
	//fails when ALL of its children fails
	public class Selector<T> : BranchNode<T>{
		public Selector(params Node<T>[] children) : base(children) {
		}

		public override bool Update(T context){
			foreach (var child in Children) {
				if (child.Update (context))
					return true;
			}
			return false;
		}
	}

	//sequence
	//succeeds when ALL of its children succeeds
	//fails when ONE of its children fails
	//used for things like checklists (e.g. if there's food nearby, and if i'm hungry, then eat the food)
	public class Sequence<T> : BranchNode<T>{
		public Sequence(params Node<T>[] children) : base(children){
		}
		public override bool Update(T context){
			foreach (var child in Children) {
				if (!child.Update (context))
					return false;
			}
			return true;
		}
	}

	//decorator
	//acts as a modifier for another node
	//base class that holds a reference to the modified or decorated node
	public abstract class Decorator<T> : Node<T>{
		protected Node<T> Child { get; private set;}
		protected Decorator(Node<T> child){
			Child = child;
		}
	}

	//common example of a decorator node: Not or Negate node
	//inverts the result of another node. 
	//e.g. if you have a node that checks if there's any friends nearby, you may need to run some logic when there are NO friends nearby.
	//Not node allows you to do without having to create a different node
	public class Not<T> : Decorator<T>{
		public Not(Node<T> child) : base(child){
		}
		public override bool Update(T context){
			return !Child.Update(context);
		}
	}
}