namespace decomplex
{
    public interface IHandler<TData>
    {
        bool IsFor(TData data);
        void Handle(TData data);
    }
}