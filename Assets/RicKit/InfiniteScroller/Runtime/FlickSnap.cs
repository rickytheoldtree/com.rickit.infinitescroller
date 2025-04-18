﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RicKit.InfiniteScroller
{
    [RequireComponent(typeof(InfiniteScroller))]
    public class FlickSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private InfiniteScroller scroller;
        public InfiniteScroller.TweenType snapTweenType;
        public float snapTweenTime;
        public event Action<InfiniteScrollerCellView> OnSnapComplete;
        private bool isDragging;

        private Vector2 dragStartPosition = Vector2.zero;
        private int currentIndex;

        private void Awake()
        {
            scroller = GetComponent<InfiniteScroller>();
        }

        public void OnBeginDrag(PointerEventData data)
        {
            if (scroller.IsTweening)
            {
                scroller.InterruptTween();
            }

            isDragging = true;

            dragStartPosition = data.position;

            var cellViews = scroller.GetComponentsInChildren<InfiniteScrollerCellView>();

            var minDistance = float.MaxValue;
            foreach (var cellView in cellViews)
            {
                if (!cellView.active) continue;

                var distance = Vector3.Distance(cellView.transform.position, scroller.transform.position);

                if (distance > minDistance) continue;
                minDistance = distance;
                currentIndex = cellView.dataIndex;
            }
        }

        public void OnEndDrag(PointerEventData data)
        {
            if (!isDragging) return;
            isDragging = false;
            var delta = data.position - dragStartPosition;
            var focusDelta = scroller.scrollDirection switch
            {
                InfiniteScroller.ScrollDirectionEnum.Horizontal => delta.x,
                InfiniteScroller.ScrollDirectionEnum.Vertical => delta.y,
                _ => 0f
            };
            var jumpToIndex = focusDelta switch
            {
                >= 0 => currentIndex - 1,
                < 0 => currentIndex + 1,
                _ => currentIndex
            };
            
            var maxIndex = scroller.Delegate.GetNumberOfCells(scroller) - 1;
            if (jumpToIndex > maxIndex || jumpToIndex < 0) return;
            jumpToIndex = Mathf.Clamp(jumpToIndex, 0, maxIndex);
            scroller.JumpToDataIndex(jumpToIndex, tweenType: snapTweenType, tweenTime: snapTweenTime,
                jumpComplete:
                () =>
                {
                    var cellView = scroller.GetCellViewAtDataIndex(jumpToIndex);
                    if (cellView) OnSnapComplete?.Invoke(cellView);
                });
        }
    }
}