// project created on 9/22/2001 at 1:04 PM
using System;

class MainClass
{
	public static void Main(string[] args)
	{
		Onetimepad otp = new Onetimepad();
		otp.Generate("test.bin", 3571);
		otp.XorFileWithPad("main.cs", "main.cs.xor", "test.bin", 0);
		otp.XorFileWithPad("main.cs.xor", "main.cs.orig", "test.bin", 0);
	}
}
