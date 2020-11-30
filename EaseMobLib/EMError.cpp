#include "pch.h"
#include <vcclr.h>
#include "EMError.h"
#include "EMUtils.h"

namespace EaseMobLib {
	void EMError::description::set(String^ desc) {
		pin_ptr<const wchar_t> wch = PtrToStringChars(desc);
		self()->mDescription = wstring_to_utf8(wch);
	}
}