using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest Dialogue")]
public class QuestDialogue : ScriptableObject
{
    [SerializeField] Dialogue _problem;
    [SerializeField] Dialogue _waiting;
    [SerializeField] Dialogue _resolution;

    [TextArea(1, 2)] [SerializeField] string _questProposal;
    [SerializeField] string _accept;
    [SerializeField] string _acceptResponse;
    [SerializeField] string _decline;
    [SerializeField] string _declineResponse;

    public Dialogue Problem => _problem;
    public Dialogue Waiting => _waiting;
    public Dialogue Resolved => _resolution;

    public string Proposal => _questProposal;
    public string Accept => _accept;
    public string AcceptResponse => _acceptResponse;
    public string Decline => _decline;
    public string DeclineResponse => _declineResponse;
}