#pragma once
#include <emcursorresult.h>
#include "emsbsase.h"
#include <string>

using namespace System;

namespace EaseMobLib {
	public ref class EMCursorResult : public EMBase {
	public:
		EMCursorResult(array<EMBase^>^ result, String^ cursor) {
			mResult = result;
			mCursor = cursor;
		}
		property String^ cursor {
			String^ get() {
				return mCursor;
			}
		}

		property array<EMBase^>^ result {
			array<EMBase^>^ get() {
				return mResult;
			}
		}
	private:
		array<EMBase^>^ mResult;
		String^ mCursor;
	};

	public ref class EMPageResult : public EMBase {
	public:
		EMPageResult(array<EMBase^>^ result, int count) {
			mResult = result;
			mCount = count;
		}
		property int count {
			int get() {
				return mCount;
			}
		}

		property array<EMBase^>^ result {
			array<EMBase^>^ get() {
				return mResult;
			}
		}
	private:
		array<EMBase^>^ mResult;
		int mCount;
	};
}