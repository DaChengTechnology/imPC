#pragma once
#include <emcallback.h>
#include "emsbsase.h"
#include "EMError.h"

using namespace System;

namespace EaseMobLib {
	
	public ref class EMCallback:EMBase
	{
	public:
		EMCallback();
		~EMCallback();
		delegate void D_SuccessCallback();
		delegate void D_ProgressCallback(int);
		delegate void D_FailCallback(EMError^);
	public:
		D_SuccessCallback^ onSuccessCallback;
		D_ProgressCallback^ onProgressCallback;
		D_FailCallback^ onFailCallback;
		easemob::EMCallbackPtr* getCallback();
	private:
		easemob::EMCallbackObserverHandle* observer;
	};
}

