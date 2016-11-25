
using System.Collections;
using System.Runtime.Serialization;

namespace FunkyNodeIds.Bitmap
{
    public interface IBitmap: IEnumerable, ISerializable
    {
        /// <summary>
        /// Intersects 2 bitmaps
        /// </summary>
        /// <param name="bitmap">Second bitmat to use for intersection</param>
        /// <returns>True if there are comun  numbers in the set and false if not</returns>
        bool Intersects(IBitmap bitmap);
        /// <summary>
        /// Or operation on sets
        /// </summary>
        /// <param name="bitmap">Second bitmat to use for Or operation</param>
        /// <returns>New Bitmap containing sets union</returns>
        IBitmap Or(IBitmap bitmap);
        /// <summary>
        /// Set specific bit in bitset
        /// </summary>
        /// <param name="i">bit to set</param>
        /// <returns>True if bit is set false if bit is out of range</returns>
        bool Set(int i);
        /// <summary>
        /// Gets the size
        /// </summary>
        /// <returns>int representing nr of bytes</returns>
        int Size();
    }
}
