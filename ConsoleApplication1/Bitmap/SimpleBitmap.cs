using System;
using System.Collections;
using System.Runtime.Serialization;

namespace FunkyNodeIds.Bitmap
{
    [Serializable]
    public class SimpleBitmap : IBitmap
    {
        private const int Size=32;
        private const int SizeInBits = Size * 8;
        private byte[] _bitmap = new byte[Size];



        public static IBitmap BitmapOf(params int[] setbits)
        {
            SimpleBitmap bitmap = new SimpleBitmap();
            foreach (int k in setbits)
                bitmap.Set(k);
            return bitmap;
        }


        #region Methods
        public bool Intersects(IBitmap bitmap)
        {
            for (int i = 0; i < Size; i++)
            {
                if ((byte)(((SimpleBitmap)bitmap)._bitmap[i] & _bitmap[i]) != 0)
                    return true;
            }
            return false;
        }

        public IBitmap Or(IBitmap bitmap)
        {
            for (int i = 0; i < Size; i++)
            {
                _bitmap[i] = (byte)(((SimpleBitmap)bitmap)._bitmap[i] | _bitmap[i]);
            }
            return this;
        }

        public bool Set(int index)
        {
            if (index > SizeInBits)
            {
                return false;
            }
            int byteIndex = index / 8;
            int bitIndex = index % 8;
            byte mask = (byte)(1 << bitIndex);

            _bitmap[byteIndex] = (byte)(_bitmap[byteIndex] | mask);
            return true;
        }

        int IBitmap.Size()
        {
            return Size;
        }
        #endregion


        public IEnumerator GetEnumerator()
        {

            for (int w = 0; w < Size; w++)
            {
                for (int i = 0; i < 8; i++) {
                    if ((_bitmap[w] & (1 << i)) != 0) {
                        yield return w * 8 + i;
                    }
                }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("data", _bitmap);
        }

    }
}
