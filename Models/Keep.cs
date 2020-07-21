namespace Keepr.Models
{
  public class Keep
  {
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public bool IsPrivate { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }
    public int Keeps { get; set; }

    public Keep()
    {

    }
    public Keep(string userId, string name, string description, string img, bool isPrivate, int views, int shares, int keeps)
    {
      UserId = userId;
      Name = name;
      Description = description;
      Img = img;
      IsPrivate = isPrivate;
      Views = views;
      Shares = shares;
      Keeps = keeps;
    }
  }
  public class VaultKeepViewModel : Keep
  {
    public int VaultKeepId { get; set; }
  }
}