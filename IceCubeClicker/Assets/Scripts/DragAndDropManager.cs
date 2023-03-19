using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropManipulator : PointerManipulator
{
    public DragAndDropManipulator(VisualElement target, ScrollView root)
    {
        this.target = target;
        this.root = root;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
        target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
        target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
        target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
        target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
    }

    private StyleLength targetStartLeft { get; set; }
    private StyleLength targetStartTop { get; set; }

    private Vector3 pointerStartPosition { get; set; }

    private bool enabled { get; set; }

    private ScrollView root { get; }

    private void PointerDownHandler(PointerDownEvent evt)
    {
        targetStartLeft = target.style.left;
        targetStartTop = target.style.top;
        pointerStartPosition = evt.position;
        target.CapturePointer(evt.pointerId);
        enabled = true;
    }

    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            Vector3 pointerDelta = evt.position - pointerStartPosition;

            target.transform.position = new Vector2(
                Mathf.Clamp(targetStartLeft.value.value + pointerDelta.x, 0, target.panel.visualTree.worldBound.width),
                Mathf.Clamp(targetStartTop.value.value + pointerDelta.y, 0, target.panel.visualTree.worldBound.height));
        }
    }

    private void PointerUpHandler(PointerUpEvent evt)
    {
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            target.ReleasePointer(evt.pointerId);
        }
    }

    private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
    {
        if (enabled)
        {
            ScrollView slotsContainer = root.Q<ScrollView>("drillPartInventory");
            UQueryBuilder<VisualElement> allSlots =
                slotsContainer.Query<VisualElement>(className: "drillInventorySlot");
            UQueryBuilder<VisualElement> overlappingSlots =
                allSlots.Where(OverlapsTarget);
            VisualElement closestOverlappingSlot =
                FindClosestSlot(overlappingSlots);
            StyleLength closestLeft = 0;
            StyleLength closestTop = 0;
            Vector3 closestPos = Vector3.zero;
            if (closestOverlappingSlot != null)
            {
                closestLeft = closestOverlappingSlot.style.left;
                closestTop = closestOverlappingSlot.style.top;
            }
            target.style.left =
                closestOverlappingSlot != null ?
                closestLeft :
                targetStartLeft;
            target.style.top =
                closestOverlappingSlot != null ?
                closestTop :
                targetStartTop;
            enabled = false;
        }
    }

    private bool OverlapsTarget(VisualElement slot)
    {
        return target.worldBound.Overlaps(slot.worldBound);
    }

    private VisualElement FindClosestSlot(UQueryBuilder<VisualElement> slots)
    {
        List<VisualElement> slotsList = slots.ToList();
        float bestDistanceSq = float.MaxValue;
        VisualElement closest = null;
        foreach (VisualElement slot in slotsList)
        {
            Vector2 displacement = new(slot.style.left.value.value - target.style.left.value.value, slot.style.top.value.value - target.style.left.value.value);
            float distanceSq = displacement.sqrMagnitude;
            if (distanceSq < bestDistanceSq)
            {
                bestDistanceSq = distanceSq;
                closest = slot;
            }
        }
        return closest;
    }
}
