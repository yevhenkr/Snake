using UnityEngine;
using System.Collections;

public class BurgerAnimation : MonoBehaviour
{
	#region Variables

	public Canvas m_Canvas;
	
	public GUIAnimFREE m_CenterButtons;
	
	public GUIAnimFREE m_Button2;

	public GUIAnimFREE m_Bar2;
	
	bool m_Bar2_IsOn = false;
	
	void Awake ()
	{
		if(enabled)
		{
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}
	void Start ()
	{
		StartCoroutine(MoveInTitleGameObjects());
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
	}
	
	IEnumerator MoveInTitleGameObjects()
	{
		yield return new WaitForSeconds(1.0f);
		StartCoroutine(MoveInPrimaryButtons());
	}
	
	// MoveIn all primary buttons
	IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1.0f);

		m_CenterButtons.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		
		StartCoroutine(EnableAllDemoButtons());
	}
	
	public void HideAllGUIs()
	{
		m_CenterButtons.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			if(m_Bar2_IsOn==true)
			m_Bar2.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		
	}
	IEnumerator EnableAllDemoButtons()
	{
		yield return new WaitForSeconds(1.0f);
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}
	
	IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		GUIAnimSystemFREE.Instance.EnableButton(GO.transform, false);
		
		yield return new WaitForSeconds(DisableTime);
		
		GUIAnimSystemFREE.Instance.EnableButton(GO.transform, true);
	}
	
	public void OnButton_2()
	{
		ToggleBar2();
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
	}
	
	void ToggleBar2()
	{
		m_Bar2_IsOn = !m_Bar2_IsOn;
		if(m_Bar2_IsOn==true)
		{// m_Bar2 moves in
			m_Bar2.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{// m_Bar2 moves out
			m_Bar2.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}
	#endregion // Toggle Button
}
