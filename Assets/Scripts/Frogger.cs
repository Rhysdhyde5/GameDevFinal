using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FroggerInput
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public void UpdateInput()
    {
        up = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        down = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        left = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        right = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }
}

public class UpCommand : Command
{
    public UpCommand(IEntity entity) : base(entity)
    {
    }

    public override void Execute()
    {
        Vector3 destination = entity.transform.position + Vector3.up;
        
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        if (platform != null)
        {
            entity.transform.SetParent(platform.transform);
        }
        else
        {
            entity.transform.SetParent(null);
        }

        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        if (obstacle != null && platform == null)
        {
            entity.transform.position = destination;
            entity.Death();
            return;
        }

        entity.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        entity.transform.position += Vector3.up;
        entity.AdvanceRow();
    }
}

public class DownCommand : Command
{
    public DownCommand(IEntity entity) : base(entity)
    {
    }

    public override void Execute()
    {
        Vector3 destination = entity.transform.position + Vector3.down;
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        
        if (platform != null) {
            entity.transform.SetParent(platform.transform);
        } else {
            entity.transform.SetParent(null);
        }
        
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        
        if (obstacle != null && platform == null)
        {
            entity.transform.position = destination;
            entity.Death();
            return;
        }
        
        entity.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        entity.transform.position += Vector3.down;
        return;
    }
}

public class LeftCommand : Command
{
    public LeftCommand(IEntity entity) : base(entity)
    {
    }

    public override void Execute()
    {
        Vector3 destination = entity.transform.position + Vector3.left;
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        
        if (barrier != null) {
            return;
        }
        
        if (obstacle != null && platform == null)
        {
            entity.transform.position = destination;
            entity.Death();
            return;
        }
        entity.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        entity.transform.position += Vector3.left;
        return;
    }
}

public class RightCommand : Command
{
    public RightCommand(IEntity entity) : base(entity)
    {
    }

    public override void Execute()
    {
        Vector3 destination = entity.transform.position + Vector3.right;
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        
        if (barrier != null) {
            return;
        }
        
        if (obstacle != null && platform == null)
        {
            entity.transform.position = destination;
            entity.Death();
            return;
        }
        entity.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        entity.transform.position += Vector3.right;
        return;
    }
}

public class PlayerIdle : IPlayerState
{
    public void Enter(Frogger frogger)
    {
        //Debug.Log("Idle");
        frogger.ChangeToIdleDelay();
        return;
    }

    public IPlayerState Tick(Frogger frogger, FroggerInput input)
    {
        if (input.up) return new PlayerUp();
        if (input.down) return new PlayerDown();
        if (input.left) return new PlayerLeft();
        if (input.right) return new PlayerRight();
        return null;
    }

    public void Exit(Frogger frogger)
    {
        return;
    }
}

public class PlayerUp : IPlayerState
{
    private Command upCommand;

    public void Enter(Frogger frogger)
    {
        //Debug.Log("up");
        frogger.ChangeToLeap(); 
        upCommand = new UpCommand(frogger);
        upCommand.Execute();
    }

    public IPlayerState Tick(Frogger frogger, FroggerInput input)
    {
        if (input.up) return null;
        if (input.down) return new PlayerDown();
        if (input.left) return new PlayerLeft();
        if (input.right) return new PlayerRight();
        return new PlayerIdle();
    }

    public void Exit(Frogger frogger)
    {
        return;
    }
}
public class PlayerDown : IPlayerState
{
    private Command downCommand;
    public void Enter(Frogger frogger)
    {
        //Debug.Log("down");
        frogger.ChangeToLeap(); 
        downCommand = new DownCommand(frogger);
        downCommand.Execute();
    }
    public IPlayerState Tick(Frogger frogger, FroggerInput input)
    {
        if (input.up) return new PlayerUp();
        if (input.down) return null;
        if (input.left) return new PlayerLeft();
        if (input.right) return new PlayerRight();
        return new PlayerIdle();
    }

    public void Exit(Frogger frogger)
    {
        return;
    }
}


public class PlayerLeft : IPlayerState
{
    private Command leftCommand;
    public void Enter(Frogger frogger)
    {
        //Debug.Log("left");
        frogger.ChangeToLeap(); 
        leftCommand = new LeftCommand(frogger);
        leftCommand.Execute();
    }

    public IPlayerState Tick(Frogger frogger, FroggerInput input)
    {
        if (input.up) return new PlayerUp();
        if (input.down) return new PlayerDown();
        if (input.left) return null;
        if (input.right) return new PlayerRight();
        return new PlayerIdle();
    }

    public void Exit(Frogger frogger)
    {
        return;
    }
}

public class PlayerRight : IPlayerState
{
    private Command rightCommand;
    public void Enter(Frogger frogger)
    {
        //Debug.Log("right");
        frogger.ChangeToLeap(); 
        rightCommand = new RightCommand(frogger);
        rightCommand.Execute();
    }

    public IPlayerState Tick(Frogger frogger, FroggerInput input)
    {
        if (input.up) return new PlayerUp();
        if (input.down) return new PlayerDown();
        if (input.left) return new PlayerLeft();
        if (input.right) return null;
        return new PlayerIdle();
    }

    public void Exit(Frogger frogger)
    {
        return;
    }
}

public interface IPlayerState
{
    public IPlayerState Tick(Frogger frogger, FroggerInput input);
    public void Enter(Frogger frogger);
    public void Exit(Frogger frogger);
}

public class Frogger : MonoBehaviour, IEntity
{
    private IPlayerState currentState = new PlayerIdle();
    private FroggerInput _input;
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite leapSprite;
    public Sprite deadSprite;
    private Vector3 spawnPosition;
    private float farthestRow;

    public void ChangeToLeap()
    {
        spriteRenderer.sprite = leapSprite;
    }

    public void ChangeToIdleDelay()
    {
        CancelInvoke();
        Invoke(nameof(ChangeToIdle), 0.1f);
    }

    private void ChangeToIdle()
    {
        spriteRenderer.sprite = idleSprite;
    }
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
        _input = new FroggerInput();
    }

    private void Update()
    {
        _input.UpdateInput();
        UpdateState(_input);
    }

    private void UpdateState(FroggerInput input)
    {
        IPlayerState newState = currentState.Tick(this, input);

        if (newState != null)
        {
            currentState.Exit(this);
            currentState = newState;
            newState.Enter(this);
        }
    }
    
    public void Death()
    {
        Debug.Log("death");
        StopAllCoroutines();
        CancelInvoke();
        enabled = false;
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deadSprite;
        transform.SetParent(null);
        FindObjectOfType<GameManager>().Died();

    }

    public void Respawn()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        farthestRow = spawnPosition.y;
        spriteRenderer.sprite = idleSprite;

        gameObject.SetActive(true);
        enabled = true;

    }

    public void AdvanceRow()
    {
        FindObjectOfType<GameManager>().AdvancedRow(transform.position.y);
    }
}

