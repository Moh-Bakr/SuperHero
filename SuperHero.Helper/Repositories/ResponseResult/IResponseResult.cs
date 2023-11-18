namespace SuperHero.BAL;

public interface IResponseResult<T>
{
   T Data { get; }
   IEnumerable<string> Errors { get; }
   bool Succeeded { get; }
}
