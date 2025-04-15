public class Load : State
{
    public override void Begin()
    {
        End();
    }

    public override void End()
    {
        StatesMachine.GoToState<MainMenu>();
    }
}
