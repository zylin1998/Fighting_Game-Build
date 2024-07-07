using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using Loyufei;

namespace FightingGame
{
    public class DropRectAdjust : MonoBehaviour
    {
        private void Awake()
        {
            var scrollRect = GetComponent<ScrollRect>();
            
            if (scrollRect) { Adjust(scrollRect); }
        }

        protected virtual void Adjust(ScrollRect scrollRect)
        {
            var content = scrollRect.content;
            var transform = (RectTransform)scrollRect.transform;

            foreach (RectTransform childRect in content)
            {
                if (childRect == content || !childRect.gameObject.activeSelf) { continue; }
                
                childRect
                    .GetComponentsInChildren<Selectable>()
                    .ForEach(s =>
                    {
                        s.OnSelectAsObservable().Subscribe((data) =>
                        {
                            float overflow = (content.rect.height - transform.rect.height) / 2f;

                            float upBorder = -overflow - childRect.offsetMax.y;
                            float downBorder = -content.rect.height + overflow - childRect.offsetMin.y;

                            /*if (upBorder > content.anchoredPosition.y)
                                content.DOAnchorPos(new Vector2(content.anchoredPosition.x, upBorder), 0.1f);
                            else if (downBorder < content.anchoredPosition.y)
                                content.DOAnchorPos(new Vector2(content.anchoredPosition.x, upBorder), 0.1f);*/
                        });
                    });
            }
        }
    }
}
