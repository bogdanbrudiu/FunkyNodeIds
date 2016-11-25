using Ewah;
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace FunkyNodeIds.Bitmap
{
    [Serializable]
    class CompressedBitmap : IBitmap
    {
        private EwahCompressedBitArray _bitmap = new EwahCompressedBitArray();


        public static IBitmap BitmapOf(params int[] setbits)
        {
            CompressedBitmap bitmap = new CompressedBitmap();
            foreach (int k in setbits)
                bitmap._bitmap.Set(k);
            return bitmap;
        }

        #region Methods
        public bool Intersects(IBitmap bitmap)
        {
            return _bitmap.Intersects(((CompressedBitmap)bitmap)._bitmap);
        }

        public IBitmap Or(IBitmap bitmap)
        {
            _bitmap = _bitmap.Or(((CompressedBitmap)bitmap)._bitmap);
            return this;
        }

        public bool Set(int i)
        {
            return _bitmap.Set(i);
        }

        public int Size()
        {
            return _bitmap.SizeInBytes;
        } 
        #endregion


        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bitmap.GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("data", _bitmap);
        }
    }
}
