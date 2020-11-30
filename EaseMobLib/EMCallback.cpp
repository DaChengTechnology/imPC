#include "pch.h"
#include <gcroot.h>
#include "EMCallBack.h"
#include "EMUtils.h"

namespace EaseMobLib {
	static std::string TAG = "_EMCallbackImpl";

	class _EMCallbackImpl : public easemob::EMCallback {
	public:
		_EMCallbackImpl(EaseMobLib::EMCallback^ obj, const easemob::EMCallbackObserverHandle& observer) :
			easemob::EMCallback(observer,
				// onSuccess
				[this]()->bool {
					pCallback->onSuccessCallback->Invoke();
					return true;
				},
				// onError
					[this](const easemob::EMErrorPtr err)->bool {
					EMError^ csError = gcnew EMError(err->mErrorCode, getCSString(err->mDescription));
					pCallback->onFailCallback->Invoke(csError);
					return true;
				},
					// progress
					[this](int progress) {
					pCallback->onProgressCallback->Invoke(progress);
				}
					)
		{
			pCallback = obj;
		}

	private:
		easemob::EMCallbackObserverHandle observer;
		gcroot<EaseMobLib::EMCallback^> pCallback;
	};

	EMCallback::EMCallback() {
		observer = new easemob::EMCallbackObserverHandle;
		easemob::EMCallbackPtr ptr(new _EMCallbackImpl(this, *observer));
		this->setNativeHandler<easemob::EMCallbackPtr>(&ptr);
	}

	EMCallback::~EMCallback() {
		delete observer;
		nativeFinalize<easemob::EMCallbackPtr>();
	}


	easemob::EMCallbackPtr* EMCallback::getCallback() {
		return new easemob::EMCallbackPtr(getNative<easemob::EMCallbackPtr>());
	}
}