using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler,
    IRuntimeInitializable
{
    private const float MinMovementForTrigger = 10f;

    [SerializeField] private UIInputDetector _inputDetector;

    private Player _player;
    private Vector2 _middlePosition;
    private bool _isPointerDown;
    private Vector2 _initialPosition;

    public void Initialize()
    {
        _initialPosition = transform.position;
        UpdateMiddlePosition();
        
        _player = FindObjectOfType<Player>();
        
        _inputDetector.PointerDown +=InputDetector_OnPointerDown;
        _inputDetector.PointerUp +=InputDetector_OnPointerUp;
    }

    private void OnDestroy()
    {
        _inputDetector.PointerDown -=InputDetector_OnPointerDown;
        _inputDetector.PointerUp -=InputDetector_OnPointerUp;
    }

    private void UpdateMiddlePosition()
    {
        _middlePosition = transform.position;
    }

    private void InputDetector_OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        transform.position = eventData.position;

        UpdateMiddlePosition();
    }
    
    private void InputDetector_OnPointerUp(PointerEventData eventData)
    {
        OnPointerUp(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!_isPointerDown)
            return;

        var yDifference = _middlePosition.y - eventData.position.y;
        var moveOnY = Mathf.Abs(yDifference) > MinMovementForTrigger;
        
        var xDifference = _middlePosition.x - eventData.position.x;
        var moveOnX = Mathf.Abs(xDifference) > MinMovementForTrigger;

        if (!moveOnX && !moveOnY)
        {
            _player.StopMoving();
            return;
        }

        var normalizedVector = new Vector3(xDifference, 0, yDifference);
        _player.Move(normalizedVector);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
        _player.StopMoving();

        transform.position = _initialPosition;
        UpdateMiddlePosition();
    }
}