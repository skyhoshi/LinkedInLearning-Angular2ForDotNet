using System.Collections.Generic;

namespace SkyhoshiLinkedInLibrary.Processors.Interfaces
{
    public interface IProcessor<T>
    {
        List<T> List();
        List<T> Parse();
    }
}