FunkyNodeIds
=============

There are sets of node ids which contain partly-contiguous ranges of node ids.
The first part of the exercise is to(in a language of your choice) model the
ranges in a space efficient manner.
An example set:
a/1, a/2, a/3, a/4, a/128, a/129, b/65, b/66, c/1, c/10, c/42
Secondly, given the model already developed, write an algorithm that will add
two sets, merging any overlapping ranges.
For example
Set A (same as example for part 1):
a/1, a/2, a/3, a/4, a/128, a/129, b/65, b/66, c/1, c/10, c/42
Set B:
a/1, a/2, a/3, a/4 a/5, a/126, a/127, b/100, c/2, c/3, d/1
Set A + Set B should contain:
a/1, a/2, a/3, a/4, a/5, a/126, a/127, a/128, a/129, b/65, b/66, b/100, c/1,
c/2, c/3, c/10, c/42, d/1

Proposed solution for set modeling
^.*/ (the non numeric part eg. a ) is the key of the dictionary
/.*$ (the numeric part eg. 1) is the bit in Bitmap

SortedDictionary<string, IBitmap> O(log n) insert/retrieval of key
IBitmap O(n)for setting n bits

IBitmap -  2 implementation 
	SimpleBitmap  
		Straightforward impelementation of Bitmap with max size of 32Bytes (number max value 256)
	CompressedBitmap
		https://github.com/lemire/csharpewah for Compressed bitmaps in C#
		

For Merging ranges Or operation on IBitmap does the job so only 
check if the SortedDictionary contains the key 
	and do the Or
	or Add the missing key with its bitset


Output:

CompressedBitmap
a/1, a/2, a/3, a/4, a/128, a/129, b/65, b/66, c/1, c/10, c/42
memory usage: 3594 bytes
set usage: 64 bytes

a/1, a/2, a/3, a/4, a/5, a/126, a/127, b/100, c/2, c/3, d/1
memory usage: 3673 bytes
set usage: 72 bytes

a/1, a/2, a/3, a/4, a/5, a/126, a/127, a/128, a/129, b/65, b/66, b/100, c/1, c/2
, c/3, c/10, c/42, d/1
memory usage: 3681 bytes
set usage: 80 bytes

SimpleBitmap
a/1, a/2, a/3, a/4, a/128, a/129, b/65, b/66, c/1, c/10, c/42
memory usage: 3392 bytes
set usage: 96 bytes

a/1, a/2, a/3, a/4, a/5, a/126, a/127, b/100, c/2, c/3, d/1
memory usage: 3469 bytes
set usage: 128 bytes

a/1, a/2, a/3, a/4, a/5, a/126, a/127, a/128, a/129, b/65, b/66, b/100, c/1, c/2
, c/3, c/10, c/42, d/1
memory usage: 3469 bytes
set usage: 128 bytes

CompressedBitmap & SimpleBitmap small
a/1
memory usage: 3404 bytes
set usage: 16 bytes

a/1
memory usage: 3238 bytes
set usage: 32 bytes

CompressedBitmap & SimpleBitmap big
a/1, a/2, a/3, a/4, a/5, a/6, a/7, a/8, a/9, a/10, a/12, a/13, a/14, a/15, a/16,
 a/17, a/18, a/19, a/20, a/21, a/22, a/23, a/24, a/25, a/26, a/27, a/28, a/29
memory usage: 3404 bytes
set usage: 16 bytes

a/1, a/2, a/3, a/4, a/5, a/6, a/7, a/8, a/9, a/10, a/12, a/13, a/14, a/15, a/16,
 a/17, a/18, a/19, a/20, a/21, a/22, a/23, a/24, a/25, a/26, a/27, a/28, a/29
memory usage: 3238 bytes
set usage: 32 bytes




