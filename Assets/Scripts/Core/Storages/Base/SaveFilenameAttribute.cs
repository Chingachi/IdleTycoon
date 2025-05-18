using System;
namespace Core.Storages.Base
{
  [AttributeUsage(AttributeTargets.Class, Inherited = false)]
  public class SaveFilenameAttribute : Attribute
  {

    public SaveFilenameAttribute (string filename)
    {
      Filename = filename;
    }

    public string Filename { get; }
  }
}