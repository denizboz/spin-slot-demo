namespace Core {
    public interface ISequenceGenerator<T> {
        T[] Generate(T end, int count);
    }
}