using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    public StateAction[] Actions;
    public Transition[] Transitions;
    public Color gizmoColor = Color.blue;

    public void UpdateState(StateController controller)
    {
        ExecuteActions(controller);
        CheckForTransitions(controller);
    }

    private void ExecuteActions(StateController controller)
    {
        foreach(var action in Actions)
        {
            action.Act(controller);
        }
    }

    private void CheckForTransitions(StateController controller)
    {
        foreach(var transition in Transitions)
        {
            bool decision = transition.Decision.Decide(controller);
            if (decision)
            {
                controller.TransitionToState(transition.TrueState);
            }
            else
            {
                controller.TransitionToState(transition.falseState);
            }
        }
    }
}
