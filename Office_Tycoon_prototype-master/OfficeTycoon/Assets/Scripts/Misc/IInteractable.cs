using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
	List<cakeslice.Outline> GetOutlines();
    void OnInteraction(GameAgent source);
}
