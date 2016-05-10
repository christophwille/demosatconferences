/*
C# Onetime Pad
Copyright (C) 2001 Christoph Wille
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:
1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
3. The name of the author may not be used to endorse or promote products
   derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.IO;
using System.Security.Cryptography;

class Onetimepad
{
	public bool Generate(string strFilename, long nSize)
	{
		if (File.Exists(strFilename))
		{
			throw new ArgumentException("OTP file must not exist");
		}
		
		FileStream theStream = File.Create(strFilename);

		int nGenerateAtOnce = 1000;
		int nWriteNow = nGenerateAtOnce;
		byte[] abStrongRBytes = new Byte[nGenerateAtOnce];
		RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
		
		for (long nStart=0; nStart <= nSize; nStart += nGenerateAtOnce)
			{
				rng.GetBytes(abStrongRBytes);
				if ((nStart + nGenerateAtOnce) > nSize) nWriteNow = Convert.ToInt32(nSize - nStart);
				theStream.Write(abStrongRBytes, 0, nWriteNow);
			}
		theStream.Close();
		return true;
	}
	
	// nTimes intentionally not yet implemented
	public void WipeFile(string strFilename, int nTimes)
	{
		if (! File.Exists(strFilename))
		{
			throw new ArgumentException("The file does not exist");
		}
		
		FileStream fsFile2Wipe = File.OpenWrite(strFilename);
		long nBytesInFile = fsFile2Wipe.Length;
		
		int nBufferSize = 1000, nWritten = 0;
		int nWriteNow = nBufferSize;
		byte[] abBuffer = new Byte[nBufferSize];
		for (int i=0; i < nBufferSize; i++) abBuffer[i]=0;
		
		for (nWritten = 0; nWritten <= nBytesInFile; nWritten += nBufferSize)
		{
			if ((nWritten + nBufferSize) > nBytesInFile) nWriteNow = Convert.ToInt32(nBytesInFile - nWritten);
			fsFile2Wipe.Write(abBuffer, 0, nWriteNow);
		}
		fsFile2Wipe.Close();
	}
	
	public long XorFileWithPad(string strInputFile, string strDestinationFile, string strPad, long nPadStartPos)
	{
		if (! File.Exists(strPad))
		{
			throw new ArgumentException("OTP file does not exist");
		}	
		
		if (! File.Exists(strInputFile))
		{
			throw new ArgumentException("Input file does not exist");
		}	
		
		if (File.Exists(strDestinationFile))
		{
			throw new ArgumentException("Destination file must not exist");
		}		
		
		FileInfo infoPad = new FileInfo(strPad);
		FileInfo infoInputFile = new FileInfo(strInputFile);
		long nInputFileLength = infoInputFile.Length;
		long nPadLength = infoPad.Length;
		if ((nPadLength - nPadStartPos) < nInputFileLength)
		{
			throw new ArgumentException("Pad is not long enough to Xor file!");		
		}
		
		FileStream fsOutput = File.Create(strDestinationFile);
		FileStream fsPad = File.OpenRead(strPad);
		FileStream fsInput = File.OpenRead(strInputFile);
		
		int nBufferSize = 1000, nInputSize, nPadSize, nXor;
		byte[] abInput = new Byte[nBufferSize];
		byte[] abPad = new Byte[nBufferSize];
		byte[] abOutput = new Byte[nBufferSize];
		
		while (0 != (nInputSize = fsInput.Read(abInput, 0, nBufferSize)))
		{
			nPadSize = fsPad.Read(abPad, 0, nBufferSize);
			for (nXor = 0; nXor < nInputSize; nXor++) 
				abOutput[nXor] = Convert.ToByte(abInput[nXor] ^ abPad[nXor]);
			fsOutput.Write(abOutput, 0, nInputSize);
		}
		fsOutput.Close();
		fsInput.Close();
		fsPad.Close();
		
		// this method returns the last byte used of the onetime pad
		// never re-use *any* portion of a onetime pad!!
		return (nPadStartPos + nInputFileLength);
	}
}
