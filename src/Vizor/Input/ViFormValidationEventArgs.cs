﻿namespace Vizor.Input;

public class ViFormValidationEventArgs
{
	public ViFormValidationEventArgs(bool submit)
	{
		Submit = submit;
	}

	public bool Submit { get; }
}
