
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Delegates the call to OnTrigger2D for this object to another object.
/// </summary>
public class OnTrigger2DDelegator : MonoBehaviour
{
    private Collider2D caller;

    private void Awake()
    {
        caller = GetComponent<Collider2D>();
    }

    [Tooltip("Which function should be called when trigger was entered.")]
    public UnityEvent<OnTriggerDelegation> Enter;

    [Tooltip("Which function should be called when trigger was exited.")]
    public UnityEvent<OnTriggerDelegation> Exit;

    void OnTriggerEnter2D(Collider2D other) => Enter.Invoke(new OnTriggerDelegation(caller, other));
    void OnTriggerExit2D(Collider2D other) => Exit.Invoke(new OnTriggerDelegation(caller, other));

    void OnValidate()
    {
        Collider2D attached = GetComponent<Collider2D>();

        try
        {
            if (!attached.isTrigger)
            {
                Debug.LogError("Collider2D is not set to be a trigger!");
            }
        }
        catch(MissingComponentException e)
        {
            Debug.LogError("Tried to add an OnTrigger2DDelegator to an object without a Collider2D");
        }
    }
}


/// <summary>
/// Stores which collider triggered this call and which collider belongs to the other object.
/// </summary>
public struct OnTriggerDelegation {

    /// <summary>
    /// Creates an OnTriggerDelegation struct.
    /// Stores which collider triggered this call and which collider belongs to the other object.
    /// </summary>
    /// <param name="caller">The trigger collider which triggered the call.</param>
    /// <param name="other">The collider which belongs to the other object.</param>
    public OnTriggerDelegation(Collider2D caller, Collider2D other)
    {
        Caller = caller;
        Other = other;
    }

    /// <summary>
    /// The trigger collider which triggered the call.
    /// </summary>
    public Collider2D Caller { get; private set; }

    /// <summary>
    /// The other collider.
    /// </summary>
    public Collider2D Other { get; private set; }
}
