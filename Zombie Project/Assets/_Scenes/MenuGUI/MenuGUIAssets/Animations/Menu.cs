using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Animator m_animator;
    private CanvasGroup m_canvasGroup;

    public bool IsOpen
    {
        get
        {
            return m_animator.GetBool("IsOpen");
        }
        set
        {
            m_animator.SetBool("IsOpen", value);
        }
    }
    public void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_canvasGroup = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    public void Update()
    {
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            m_canvasGroup.blocksRaycasts = m_canvasGroup.interactable = false;
        }
        else
        {
            m_canvasGroup.blocksRaycasts = m_canvasGroup.interactable = true;
        }
    }
}
