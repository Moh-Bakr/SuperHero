namespace SuperHero.BAL;

public class ResponseResult<T> : IResponseResult<T>
{
   public T Data { get; set; }
   public IEnumerable<string> Errors { get; set; }

   public bool Succeeded => Errors == null || !Errors.Any();

   public ResponseResult(T data, IEnumerable<string> errors = null)
   {
      Data = data;
      Errors = errors;
   }

   public static ResponseResult<T> Success(T data)
   {
      return new ResponseResult<T>(data);
   }

   public static ResponseResult<T> Fail(IEnumerable<string> errors)
   {
      return new ResponseResult<T>(default, errors);
   }
}
