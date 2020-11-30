#pragma once
#include <emcursorresult.h>
#include "emsbsase.h"

using namespace System;

namespace EaseMobLib {
	public ref class EMGroupInfo : EMBase {
	public:
		EMGroupInfo(String^ groupId, String^ groupName) {
			mGroupId = groupId;
			mGroupName = groupName;
		}

		String^ getGroupId() {
			return mGroupId;
		}

		String^ getGroupName() {
			return mGroupName;
		}

	private:
		String^ mGroupId;
		String^ mGroupName;
	};
}