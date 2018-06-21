using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<T, U>  {

	public T fisrt;
	public U second;

	public Tuple(T first,U second){
		this.fisrt = first;
		this.second = second;
	}

}
