﻿using System;

namespace ilrLearnerEntry
{
    ///  http://stackoverflow.com/questions/7587110/basic-user-input-string-validation
    ///  Not used in the end. Remove in Future.
    static class InputValiator
	{
		static Boolean IsValueAnInt(String str)
		{
			foreach (char ch in str)
			{
				if (!Char.IsDigit(ch))
					return false;
			}

			return true;
		}
	}
}
