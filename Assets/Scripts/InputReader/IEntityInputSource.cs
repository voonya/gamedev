namespace Player
{
    public interface IEntityInputSource
    {
        float HorizontalDirection { get;  }

        bool Jump { get; }
        bool Attack { get; }

        void ResetOneTimeActions();
    }
}