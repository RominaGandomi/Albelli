using System;


namespace Albelli.Business.Models
{
    public abstract class ProductTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }

        public virtual double CalculateBin(int count)
        {
            return count * this.Width;
        }
    }

    public class Mug : ProductTypeModel
    {
        public override double CalculateBin(int count)
        {
            return (Math.Ceiling(count / 4.0) * this.Width);
        }
    }

    public class Cards : ProductTypeModel
    {
        public override double CalculateBin(int count)
        {
            return base.CalculateBin(count);
        }
    }

    public class PhotoBook : ProductTypeModel
    {
        public override double CalculateBin(int count)
        {
            return base.CalculateBin(count);
        }
    }

    public class Calendar : ProductTypeModel
    {
        public override double CalculateBin(int count)
        {
            return base.CalculateBin(count);
        }
    }

    public class Canvas : ProductTypeModel
    {
        public override double CalculateBin(int count)
        {
            return base.CalculateBin(count);
        }
    }
}
