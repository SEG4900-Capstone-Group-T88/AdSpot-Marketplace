namespace AdSpot.Api.Mutations.Inputs;

public class SubmitDeliverableInput
{
    public int OrderId { get; set; }
    public string Deliverable { get; set; }
}
