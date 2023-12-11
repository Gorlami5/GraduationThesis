namespace ReservationApp.Results
{
    public interface IDataResult<out T>
    {
        public T Data { get; }
    }
}
