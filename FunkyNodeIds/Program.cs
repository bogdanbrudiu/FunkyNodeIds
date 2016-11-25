
using FunkyNodeIds.Bitmap;
using System;
using static FunkyNodeIds.MySet;
/// <summary>
/// There are sets of node ids which contain partly-contiguous ranges of node ids.
///The first part of the exercise is to(in a language of your choice) model the
///ranges in a space efficient manner.
///An example set:
///a/1, a/2, a/3, a/4, a/128, a/129, b/65, b/66, c/1, c/10, c/42
///Secondly, given the model already developed, write an algorithm that will add
///two sets, merging any overlapping ranges.
///For example
///Set A (same as example for part 1):
///a/1, a/2, a/3, a/4, a/128, a/129, b/65, b/66, c/1, c/10, c/42
///Set B:
///a/1, a/2, a/3, a/4 a/5, a/126, a/127, b/100, c/2, c/3, d/1
///Set A + Set B should contain:
///a/1, a/2, a/3, a/4, a/5, a/126, a/127, a/128, a/129, b/65, b/66, b/100, c/1,
///c/2, c/3, c/10, c/42, d/1
/// </summary>
namespace FunkyNodeIds
{
    public class Program
    {
        private const string setA = "a/1, a/2, a/3, a/4, a/128, a/129, b/65, b/66, c/1, c/10, c/42";
        private const string setB = "a/1, a/2, a/3, a/4, a/5, a/126, a/127, b/100, c/2, c/3, d/1";
        private const string setC = "a/1";
        private const string setD = "a/1, a/2, a/3, a/4, a/5, a/6, a/7, a/8, a/9, a/10, a/12, a/13, a/14, a/15, a/16, a/17, a/18, a/19, a/20, a/21, a/22, a/23, a/24, a/25, a/26, a/27, a/28, a/29";



        public static void Main(string[] args)
        {
            Console.WriteLine("CompressedBitmap");
            MySet a = ParseSet(setA, CompressedBitmap.BitmapOf);
            MySet b = ParseSet(setB, CompressedBitmap.BitmapOf);
            a.PrintSet();
            b.PrintSet();
            MySet c = MySet.Merge(a, b, CompressedBitmap.BitmapOf);
            c.PrintSet();

            Console.WriteLine("SimpleBitmap");
            MySet aa = ParseSet(setA, SimpleBitmap.BitmapOf);
            MySet bb = ParseSet(setB, SimpleBitmap.BitmapOf);
            aa.PrintSet();
            bb.PrintSet();
            MySet cc = MySet.Merge(aa, bb, SimpleBitmap.BitmapOf);
            cc.PrintSet();

            Console.WriteLine("CompressedBitmap & SimpleBitmap small");
            MySet s1 = ParseSet(setC, CompressedBitmap.BitmapOf);
            MySet s2 = ParseSet(setC, SimpleBitmap.BitmapOf);
            s1.PrintSet();
            s2.PrintSet();

            Console.WriteLine("CompressedBitmap & SimpleBitmap big");
            MySet ss1 = ParseSet(setD, CompressedBitmap.BitmapOf);
            MySet ss2 = ParseSet(setD, SimpleBitmap.BitmapOf);
            ss1.PrintSet();
            ss2.PrintSet();
            Console.ReadLine();
        }

       

        private static MySet ParseSet(string input, BitmapFactory bitmapFactory) {
            MySet result = new MySet(bitmapFactory);
            foreach (string pair in input.Replace(" ", "").Split(',')) {
                if (!string.IsNullOrEmpty(pair)) { 
                    Node node = Node.Parse(pair);
                    result.Add(node);
                }
            }
            return result;
        }
    }
}
