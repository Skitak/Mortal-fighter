using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<T, U>  {

	public T first;
	public U second;

	public Tuple(T first,U second){
		this.first = first;
		this.second = second;
	}

}
