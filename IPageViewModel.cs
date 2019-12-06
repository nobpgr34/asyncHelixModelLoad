using System;

namespace HelixProject
{
    public interface IPageViewModel
    {
        string Name { get; }

        Action workWithBoundingBox { get; set; }
    }
}
