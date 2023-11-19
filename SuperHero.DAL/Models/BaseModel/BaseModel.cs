namespace SuperHero.DAL;

public class BaseModel
{
   public DateTime? CreatedDate { get; set; }
   public DateTime? ModifiedDate { get; set; }

   protected BaseModel()
   {
      CreatedDate = DateTime.UtcNow;
      ModifiedDate = DateTime.UtcNow;
   }

   public int Id { get; set; }
}
